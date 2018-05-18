using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Options;
using System.Reflection;

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

            var valueType = RetrieveTypeOrDefault(serializer, valueTypeJsonOption, stringType);
            var errorType = RetrieveTypeOrDefault(serializer, errorTypeJsonOption, stringType);

            if (valueJsonOption.HasValue)
            {
                return InstantiateValueFromMethod(nameof(ValueOrError.WithValue), serializer, staticValueOrErrorType, valueJsonOption, valueType, errorType);
            }

            if (errorJsonOption.HasValue)
            {
                return InstantiateErrorFromMethod(nameof(ValueOrError.WithError), serializer, staticValueOrErrorType, errorJsonOption, valueType, errorType);
            }

            throw new JsonSerializationException(
                "Cannot deserialize a ValueOrError that contains neither a value or an error");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            var valueOrErrorType = value.GetType();

            var hasValue = (bool)valueOrErrorType.GetProperty("HasValue").GetValue(value);

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

        private static Type RetrieveTypeOrDefault(JsonSerializer serializer, Option<JToken> jsonTokenOption, Type defaultType)
        {
            return jsonTokenOption.Match<Type>()
                .Some().Do(jsonToken =>
                {
                    var typeNameOption = (jsonToken.ToObject(typeof(string), serializer) as string).ToOption();
                    if (typeNameOption.HasValue)
                        return Type.GetType(typeNameOption.Value);
                    return defaultType;
                })
                .None().Do(defaultType)
                .Result();
        }

        private static object InstantiateValueFromMethod(string methodName, JsonSerializer serializer, Type staticValueOrErrorType, Option<JToken> jsonTokenOption, Type valueType, Type errorType)
        {
            var value = jsonTokenOption.Value.ToObject(valueType, serializer);

            var staticMethod = staticValueOrErrorType.GetMethod(methodName, new[] { valueType });
            return InstantiateFromMethod(valueType, errorType, value, staticMethod);
        }

        private static object InstantiateErrorFromMethod(string methodName, JsonSerializer serializer, Type staticValueOrErrorType, Option<JToken> jsonTokenOption, Type valueType, Type errorType)
        {
            var error = jsonTokenOption.Value.ToObject(errorType, serializer);

            var staticMethod = staticValueOrErrorType.GetMethod(methodName, new[] { errorType });
            return InstantiateFromMethod(valueType, errorType, error, staticMethod);
        }

        private static object InstantiateFromMethod(Type valueType, Type errorType, object parameter, MethodInfo staticMethod)
        {
            if (staticMethod.IsGenericMethod)
            {
                var genericMethod = staticMethod.MakeGenericMethod(valueType, errorType);
                return genericMethod.Invoke(null, new[] { parameter });
            }
            else
            {
                return staticMethod.Invoke(null, new[] { parameter });
            }
        }
    }
}
