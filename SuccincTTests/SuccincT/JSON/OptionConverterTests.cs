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
        public void ConvertingMaybeSomeToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var maybe = Maybe<int>.Some(1);
            var json = SerializeObject(maybe, settings);
            var newMaybe = DeserializeObject<Maybe<int>>(json, settings);

            IsTrue(newMaybe.HasValue);
            AreEqual(1, newMaybe.Value);
        }

        [Test]
        public void SerializedMaybe_CanBeDeserializedAsOption()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var maybe = Maybe<int>.Some(1);
            var json = SerializeObject(maybe, settings);
            var option = DeserializeObject<Option<int>>(json, settings);

            IsTrue(option.HasValue);
            AreEqual(1, option.Value);
        }

        [Test]
        public void SerializedOption_CanBeDeserializedAsMaybe()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var option = Option<int>.Some(1);
            var json = SerializeObject(option, settings);
            var maybe = DeserializeObject<Maybe<int>>(json, settings);

            IsTrue(maybe.HasValue);
            AreEqual(1, maybe.Value);
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
        public void ConvertingMaybeNoneToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OptionConverter());
            var maybe = Maybe<int>.None();
            var json = SerializeObject(maybe, settings);
            var newMaybe = DeserializeObject<Maybe<int>>(json, settings);

            IsFalse(newMaybe.HasValue);
        }

        [Test]
        public void ConvertingJsonToOption_FailsCleanlyIfSuccinctConverterNotUsed()
        {
            Throws<JsonSerializationException>(() => DeserializeObject<Option<int>>("{\"hasValue\":true,\"value\":1}"));
        }
    }
}