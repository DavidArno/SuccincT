using Newtonsoft.Json;
using SuccincT.Unions;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.PatternMatchers.GeneralMatcher;
using static SuccincT.Unions.Variant;

namespace SuccincT.JSON
{
    public class UnionOf3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => 
            objectType.IsGenericType() && objectType.GetGenericTypeDefinition() == typeof(Union<,,>);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object? existingValue,
                                        JsonSerializer serializer)
        {
            var type1 = objectType.GenericTypeArguments[0];
            var type2 = objectType.GenericTypeArguments[1];
            var type3 = objectType.GenericTypeArguments[2];
            var rawUnionType = typeof(Union<,,>);
            var unionType = rawUnionType.MakeGenericType(type1, type2, type3);

            var jsonObject = JObject.Load(reader);
            var rawVariant = jsonObject["case"] ?? throw new JsonException("No 'case' found for \"Union/3\" value.");
            var variant = rawVariant.ToObject<Variant>(serializer);

            var valueJson = jsonObject["value"] ?? throw new JsonException("No 'value' found for \"Union/3\" value.");

            var value = variant.Match().To<object?>()
                               .With(Case1).Do(_ => valueJson.ToObject(type1, serializer))
                               .With(Case2).Do(_ => valueJson.ToObject(type2, serializer))
                               .With(Case3).Do(_ => valueJson.ToObject(type3, serializer))
                               .Result();

            return Activator.CreateInstance(unionType, value)!;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var unionType = value!.GetType();
            var caseProperty = unionType.GetProperty("Case");
            var variant = (Variant)caseProperty?.GetValue(value, null)!;
            var variantValue = variant.Match().To<object>()
                                      .With(Case1).Do(_ => unionType?.GetProperty("Case1")?.GetValue(value, null)!)
                                      .With(Case2).Do(_ => unionType?.GetProperty("Case2")?.GetValue(value, null)!)
                                      .With(Case3).Do(_ => unionType?.GetProperty("Case3")?.GetValue(value, null)!)
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
