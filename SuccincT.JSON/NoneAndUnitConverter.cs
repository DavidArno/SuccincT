using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using SuccincT.Unions;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.None;

namespace SuccincT.JSON
{
    public class NoneAndUnitConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(None);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            JObject.Load(reader);
            return objectType.Name == "None" ? (object)none : unit;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
        }
    }
}
