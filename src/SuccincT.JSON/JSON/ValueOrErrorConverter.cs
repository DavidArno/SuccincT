using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Options;

namespace SuccincT.JSON
{
    public class ValueOrErrorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType.IsGenericType() && objectType.GetGenericTypeDefinition() == typeof(ValueOrError<,>);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            var stringType = typeof(string);
            var staticValueOrErrorType = typeof(ValueOrError);

            var jsonObject = JObject.Load(reader);

            var valueTypeJsonOption = jsonObject["valueType"].ToOption();
            var errorTypeJsonOption = jsonObject["errorType"].ToOption();

            var valueJsonOption = jsonObject["value"].ToOption();
            var errorJsonOption = jsonObject["error"].ToOption();

            var valueType = valueTypeJsonOption.Match<Type>()
                .Some().Do(vt =>
                {
                    var typeNameOption = (vt.ToObject(stringType, serializer) as string).ToOption();
                    if (typeNameOption.HasValue)
                        return Type.GetType(typeNameOption.Value);
                    return stringType;
                })
                .None().Do(stringType)
                .Result();

            var errorType = errorTypeJsonOption.Match<Type>()
                .Some().Do(vt =>
                {
                    var typeNameOption = (vt.ToObject(stringType, serializer) as string).ToOption();
                    if (typeNameOption.HasValue)
                        return Type.GetType(typeNameOption.Value);
                    return stringType;
                })
                .None().Do(stringType)
                .Result();

            if (valueJsonOption.HasValue)
            {
                var value = valueJsonOption.Value.ToObject(valueType, serializer);

                var staticMethod = staticValueOrErrorType.GetMethod("WithValue", new[] { valueType });
                if (staticMethod.IsGenericMethod)
                {
                    var genericMethod = staticMethod.MakeGenericMethod(valueType, errorType);
                    return genericMethod.Invoke(null, new[] { value });
                }
                else
                {
                    return staticMethod.Invoke(null, new[] { value });
                }
            }

            if (errorJsonOption.HasValue)
            {
                var error = errorJsonOption.Value.ToObject(errorType, serializer);

                var staticMethod = staticValueOrErrorType.GetMethod("WithError", new[] { errorType });
                if (staticMethod.IsGenericMethod)
                {
                    var genericMethod = staticMethod.MakeGenericMethod(valueType, errorType);
                    return genericMethod.Invoke(null, new[] { error });
                }
                else
                {
                    return staticMethod.Invoke(null, new[] { error });
                }
            }

            throw new JsonSerializationException(
                "Cannot deserialize a ValueOrError that contains neither a value or an error");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            var valueOrErrorType = value.GetType();

            var hasValue = (bool)valueOrErrorType.GetProperty("HasValue").GetValue(value, null);

            if (hasValue)
            {
                var valueValue = valueOrErrorType.GetProperty("Value").GetValue(value);

                writer.WritePropertyName("value");
                serializer.Serialize(writer, valueValue);
            }
            else
            {
                var errorValue = valueOrErrorType.GetProperty("Error").GetValue(value);

                writer.WritePropertyName("error");
                serializer.Serialize(writer, errorValue);
            }

            var valueOrErrorGenericTypes = valueOrErrorType.GetGenericArguments();

            var valueTypeOption = valueOrErrorGenericTypes.TryElementAt(0);
            if (valueTypeOption.HasValue)
            {
                writer.WritePropertyName("valueType");
                serializer.Serialize(writer, valueTypeOption.Value.FullName);
            }

            var errorTypeOption = valueOrErrorGenericTypes.TryElementAt(1);
            if (errorTypeOption.HasValue)
            {
                writer.WritePropertyName("errorType");
                serializer.Serialize(writer, errorTypeOption.Value.FullName);
            }
            
            writer.WriteEndObject();
        }
    }
}
