using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Options;

namespace SuccincT.JSON
{
    public class ValueOrErrorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => 
            objectType == typeof(ValueOrError);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var possibleValue = jsonObject.Properties().TryFirst(p => p.Name == "value");
            var possibleError = jsonObject.Properties().TryFirst(p => p.Name == "error");

            if (possibleValue.HasValue)
            {
                return ValueOrError.WithValue(possibleValue.Value.ToObject<string>());
            }

            if (possibleError.HasValue)
            {
                return ValueOrError.WithError(possibleError.Value.ToObject<string>());
            }

            throw new JsonSerializationException(
                "Cannot deserialize a ValueOrError that contains neither a value or an error");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueOrError = (ValueOrError)value;

            writer.WriteStartObject();

            if (valueOrError.HasValue)
            {
                writer.WritePropertyName("value");
                serializer.Serialize(writer, valueOrError.Value);
            }
            else
            {
                writer.WritePropertyName("error");
                serializer.Serialize(writer, valueOrError.Error);
            }

            writer.WriteEndObject();
        }
    }
}
