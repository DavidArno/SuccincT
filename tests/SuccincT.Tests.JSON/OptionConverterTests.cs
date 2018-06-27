using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.JSON;
using SuccincT.Options;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class OptionConverterTests
    {
        [Test]
        public void ConvertingSomeToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var option = Option<int>.Some(1);
            var json = SerializeObject(option, settings);
            var newOption = DeserializeObject<Option<int>>(json, settings);

            IsTrue(newOption.HasValue);
            AreEqual(1, newOption.Value);
        }

        [Test]
        public void ConvertingNoneToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var option = Option<int>.None();
            var json = SerializeObject(option, settings);
            var newOption = DeserializeObject<Option<int>>(json, settings);

            IsFalse(newOption.HasValue);
        }

        [Test]
        public void ConvertingJsonToOption_ProducesNoneIfConverterNotUsed()
        {
            var result = DeserializeObject<Option<int>>("{\"hasValue\":true,\"value\":1}");
            AreEqual(Option<int>.None(), result);
        }
    }
}