using Newtonsoft.Json;
using SuccincT.Unions;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.PatternMatchers.GeneralMatcher;
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
            var unionType = rawUnionType.MakeGenericType(type1, type2);

            var jsonObject = JObject.Load(reader);
            var variant = jsonObject["case"].ToObject<Variant>(serializer);
            var valueJson = jsonObject["value"];
            var value = variant.Match().To<object>()
                               .With(Case1).Do(_ => valueJson.ToObject(type1, serializer))
                               .With(Case2).Do(_ => valueJson.ToObject(type2, serializer))
                               .Result();

            return Activator.CreateInstance(unionType, value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var unionType = value.GetType();
            var caseProperty = unionType.GetProperty("Case");
            var variant = (Variant)caseProperty.GetValue(value, null);
            var variantValue = variant.Match().To<object>()
                                      .With(Case1).Do(_ => unionType.GetProperty("Case1").GetValue(value, null))
                                      .With(Case2).Do(_ => unionType.GetProperty("Case2").GetValue(value, null))
                                      .Result();

            writer.WriteStartObject();
            writer.WritePropertyName("case");
            serializer.Serialize(writer, variant);
            writer.WritePropertyName("value");
            serializer.Serialize(writer, variantValue);
            writer.WriteEndObject();
        }
    }
}
