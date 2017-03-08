using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Options;

namespace SuccincT.JSON
{
    public class SuccessConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Success<>);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            var type = objectType.GetGenericArguments()[0];

            var jsonObject = JObject.Load(reader);
            var isFailure = jsonObject["isFailure"].ToObject<bool>(serializer);

            if (isFailure)
            {
                var failure = jsonObject["failure"].ToObject(type, serializer);
                var rawMethod = typeof(Success).GetMethod("CreateFailure");
                var typedMethod = rawMethod.MakeGenericMethod(type);
                return typedMethod.Invoke(null, new[] { failure });
            }

            var rawSuccessType = typeof(Success<>);
            var successType = rawSuccessType.MakeGenericType(type);
            return Activator.CreateInstance(successType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var successType = value.GetType();
            var isFailureProperty = successType.GetProperty("IsFailure");
            var isFailure = (bool)isFailureProperty.GetValue(value, null);

            writer.WriteStartObject();
            writer.WritePropertyName("isFailure");
            serializer.Serialize(writer, isFailure);

            if (isFailure)
            {
                writer.WritePropertyName("failure");
                var failureProperty = successType.GetProperty("Failure");
                var failure = failureProperty.GetValue(value, null);
                serializer.Serialize(writer, failure);
            }

            writer.WriteEndObject();
        }
    }
}
