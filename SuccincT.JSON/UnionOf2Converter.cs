using Newtonsoft.Json;
using SuccincT.Unions;
using System;
using Newtonsoft.Json.Linq;
using static SuccincT.Unions.Variant;

namespace SuccincT.JSON
{
    // don't forget to document stuff from http://stackoverflow.com/questions/19510532/registering-a-custom-jsonconverter-globally-in-json-net
    public class UnionOf2Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => 
            objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Union<,>);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            var type1 = objectType.GetGenericArguments()[0];
            var type2 = objectType.GetGenericArguments()[1];
            var rawUnionType = typeof(Union<,>);
            var unionType = rawUnionType.MakeGenericType(new[] {type1, type2});


            var jsonObject = JObject.Load(reader);
            var variant = jsonObject["case"].ToObject<Variant>(serializer);
            var value = variant == Case1 
                ? jsonObject["value"].ToObject(type1, serializer)
                : jsonObject["value"].ToObject(type2, serializer);

            var union = Activator.CreateInstance(unionType, value);

            return union;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var unionType = value.GetType();
            var caseProperty = unionType.GetProperty("Case");
            var variant = (Variant)caseProperty.GetValue(value, null);
            var variantValue = variant == Case1
                ? unionType.GetProperty("Case1").GetValue(value, null)
                : unionType.GetProperty("Case2").GetValue(value, null);
            writer.WriteStartObject();
            writer.WritePropertyName("case");
            serializer.Serialize(writer, variant);
            writer.WritePropertyName("value");
            serializer.Serialize(writer, variantValue);
            writer.WriteEndObject();
        }
    }
}
