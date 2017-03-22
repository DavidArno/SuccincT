using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.JSON;
using SuccincT.Options;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class SuccessConverterTests
    {
        [Test]
        public void ConvertingFailureToJsonAndBack_PreservesSuccessState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SuccessConverter());
            var failure = Success.CreateFailure("a");
            var json = SerializeObject(failure, settings);
            var newFailure = DeserializeObject<Success<string>>(json, settings);

            IsTrue(newFailure.IsFailure);
            AreEqual("a", newFailure.Failure);
        }

        [Test]
        public void ConvertingSuccessToJsonAndBack_PreservesSuccessState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SuccessConverter());
            var success = new Success<int>();
            var json = SerializeObject(success, settings);
            var newSuccess = DeserializeObject<Success<int>>(json, settings);

            IsFalse(newSuccess.IsFailure);
        }
    }
}