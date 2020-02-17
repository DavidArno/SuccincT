using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.JSON;
using SuccincT.Unions;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class EitherConverterTests
    {
        [Test]
        public void WhenProvidedWithNoIsLeft_DeserializeFailsWithJsonException()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EitherConverter());

            _ = Throws<JsonException>(() => {
                _ = DeserializeObject<Either<List<int>, string>>("{k:1, value:2}", settings);
            });
        }

        [Test]
        public void WhenProvidedWithNoValue_DeserializeFailsWithJsonException()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EitherConverter());

            _ = Throws<JsonException>(() => {
                _ = DeserializeObject<Either<List<int>, string>>("{isLeft:1, k:2}", settings);
            });
        }

        [Test]
        public void ConvertingLeftEitherToJsonAndBack_PreservesEitherState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EitherConverter());
            var either = new Either<List<int>, string>(new List<int> { 1, 2 });
            var json = SerializeObject(either, settings);
            var newEither = DeserializeObject<Either<List<int>, string>>(json, settings);

            AreEqual(2, newEither.Left.Count);
            IsTrue(newEither.IsLeft);
            AreEqual(1, newEither.Left[0]);
            AreEqual(2, newEither.Left[1]);
        }

        [Test]
        public void ConvertingRightEitherToJsonAndBack_PreservesEitherState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EitherConverter());
            var either = new Either<List<int>, string>("abc");
            var json = SerializeObject(either, settings);
            var newEither = DeserializeObject<Either<List<int>, string>>(json, settings);

            IsFalse(newEither.IsLeft);
            AreEqual("abc", newEither.Right);
        }

        [Test]
        public void ConvertingListOfEithersToJsonAndBack_PreservesUEitherState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new EitherConverter());
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>("a");
            var list = new List<Either<int, string>> { either1, either2 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Either<int, string>>>(json, settings);

            AreEqual(2, newList.Count);
            IsTrue(newList[0].IsLeft);
            IsFalse(newList[1].IsLeft);
            AreEqual(1, newList[0].Left);
            AreEqual("a", newList[1].Right);
        }
    }
}
