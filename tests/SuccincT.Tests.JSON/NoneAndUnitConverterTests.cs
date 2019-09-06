using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.JSON;
using SuccincT.Unions;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.None;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class NoneAndUnitConverterTests
    {
        [Test]
        public void ConvertingNoneToJsonAndBack_CreatesANone()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NoneAndUnitConverter());
            var value = none;
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<None>(json, settings);
            AreEqual(value, newValue);
        }

        [Test]
        public void ConvertingUnitToJsonAndBack_CreatesAUnit()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NoneAndUnitConverter());
            var value = unit;
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<Unit>(json, settings);
            AreEqual(value, newValue);
        }

        [Test]
        public void ConvertingJsonUnit_WorksEvenIfIfSuccinctConverterNotUsed()
        {
            // Unit is a struct, so we can't prevent new copies being made so can't prevent the serializer
            // creating them, so the type has to implicitly support serialization.
            var value = unit;
            var json = SerializeObject(value);
            var newValue = DeserializeObject<Unit>(json);
            AreEqual(value, newValue);
        }
    }
}