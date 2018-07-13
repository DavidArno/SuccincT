using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.JSON;
using SuccincT.Options;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class ValueOrErrorConverterTests
    {
        [Test]
        public void ConvertingValueToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ValueOrErrorConverter());
            var value = ValueOrError.WithValue("a");
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<ValueOrError>(json, settings);

            IsTrue(newValue.HasValue);
            AreEqual("a", newValue.Value);
        }

        [Test]
        public void ConvertingErrorToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ValueOrErrorConverter());
            var value = ValueOrError.WithError("b");
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<ValueOrError>(json, settings);

            IsFalse(newValue.HasValue);
            AreEqual("b", newValue.Error);
        }

        [Test]
        public void ConvertingJsonToValueOrError_ProducesCleanDefaultValue()
        {
            var result = DeserializeObject<ValueOrError>("{\"value\":\"a\"}");
            IsFalse(result.HasValue);
            AreEqual(null, result.Error);
        }

        [Test]
        public void ConvertingInvalidJsonToValueOrError_FailsCleanlyIfSuccinctConverterUsed()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ValueOrErrorConverter());
            Throws<JsonSerializationException>(() => DeserializeObject<ValueOrError>("{}", settings));
        }
    }
}