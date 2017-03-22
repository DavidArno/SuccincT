using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Options;

namespace SuccincT.JSON
{
    public class OptionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType.IsGenericType() &&
            (objectType.GetGenericTypeDefinition() == typeof(Option<>) ||
             objectType.GetGenericTypeDefinition() == typeof(Maybe<>));

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            var type = objectType.GenericTypeArguments[0];
            var rawOptionType = objectType.Name == "Maybe`1" ? typeof(Maybe<>) : typeof(Option<>);
            var optionType = rawOptionType.MakeGenericType(type);

            var jsonObject = JObject.Load(reader);
            var hasValue = jsonObject["hasValue"].ToObject<bool>(serializer);

            var typedMethod = optionType.GetMethod(hasValue ? "Some" : "None");

            if (hasValue)
            {
                var value = jsonObject["value"].ToObject(type, serializer);
                return typedMethod.Invoke(null, new[] {value});
            }
            return typedMethod.Invoke(null, null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var optionType = value.GetType();
            var hasValueProperty = optionType.GetProperty("HasValue");
            var hasValue = (bool)hasValueProperty.GetValue(value, null);

            writer.WriteStartObject();
            writer.WritePropertyName("hasValue");
            serializer.Serialize(writer, hasValue);

            if (hasValue)
            {
                writer.WritePropertyName("value");
                var valueProperty = optionType.GetProperty("Value");
                var optionValue = valueProperty.GetValue(value, null);
                serializer.Serialize(writer, optionValue);
            }

            writer.WriteEndObject();
        }
    }
}
