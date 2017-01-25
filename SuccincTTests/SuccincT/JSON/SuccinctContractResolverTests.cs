using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.JSON;
using SuccincT.Options;
using SuccincT.Unions;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.None;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public class SuccinctContractResolverTests
    {
        [Test]
        public void ContractResolver_CanConvertOptionToJsonAndBack()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var option1 = Option<string>.Some("a");
            var option2 = Option<string>.None();
            var list = new List<Option<string>> { option1, option2 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Option<string>>>(json, settings);

            AreEqual(2, newList.Count);
            IsTrue(newList[0].HasValue);
            IsFalse(newList[1].HasValue);
            AreEqual("a", newList[0].Value);
        }

        [Test]
        public void ContractResolver_CanConvertMaybeToJsonAndBack()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var maybe1 = Maybe<string>.Some("a");
            var maybe2 = Maybe<string>.None();
            var list = new List<Maybe<string>> { maybe1, maybe2 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Maybe<string>>>(json, settings);

            AreEqual(2, newList.Count);
            IsTrue(newList[0].HasValue);
            IsFalse(newList[1].HasValue);
            AreEqual("a", newList[0].Value);
        }

        [Test]
        public void ContractResolver_CanConvertUnion2ToJsonAndBack()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var union1 = new Union<int, string>(1);
            var union2 = new Union<int, string>("a");
            var list = new List<Union<int, string>> { union1, union2 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Union<int, string>>>(json, settings);

            AreEqual(2, newList.Count);
            AreEqual(Variant.Case1, newList[0].Case);
            AreEqual(Variant.Case2, newList[1].Case);
            AreEqual(1, newList[0].Case1);
            AreEqual("a", newList[1].Case2);
        }

        [Test]
        public void ContractResolver_CanConvertUnion3ToJsonAndBack()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var union1 = new Union<int, double, string>(1);
            var union2 = new Union<int, double, string>(2.0);
            var union3 = new Union<int, double, string>("3");
            var list = new List<Union<int, double, string>> { union1, union2, union3 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Union<int, double, string>>>(json, settings);

            AreEqual(3, newList.Count);
            AreEqual(Variant.Case1, newList[0].Case);
            AreEqual(Variant.Case2, newList[1].Case);
            AreEqual(Variant.Case3, newList[2].Case);
            AreEqual(1, newList[0].Case1);
            AreEqual(2.0, newList[1].Case2);
            AreEqual("3", newList[2].Case3);
        }

        [Test]
        public void ContractResolver_CanConvertUnion4ToJsonAndBack()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var union1 = new Union<int, double, string, Variant>(1);
            var union2 = new Union<int, double, string, Variant>(2.0);
            var union3 = new Union<int, double, string, Variant>("3");
            var union4 = new Union<int, double, string, Variant>(Variant.Case3);
            var list = new List<Union<int, double, string, Variant>> { union1, union2, union3, union4 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Union<int, double, string, Variant>>>(json, settings);

            AreEqual(4, newList.Count);
            AreEqual(Variant.Case1, newList[0].Case);
            AreEqual(Variant.Case2, newList[1].Case);
            AreEqual(Variant.Case3, newList[2].Case);
            AreEqual(Variant.Case4, newList[3].Case);
            AreEqual(1, newList[0].Case1);
            AreEqual(2.0, newList[1].Case2);
            AreEqual("3", newList[2].Case3);
            AreEqual(Variant.Case3, newList[3].Case4);
        }

        [Test]
        public void ConvertingValueToJsonAndBack_PreservesOptionState()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var value = ValueOrError.WithValue("a");
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<ValueOrError>(json, settings);

            IsTrue(newValue.HasValue);
            AreEqual("a", newValue.Value);
        }
        [Test]
        public void ConvertingNoneToJsonAndBack_CreatesANone()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };
            var value = none;
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<None>(json, settings);
            AreEqual(value, newValue);
        }

        [Test]
        public void ConvertingUnitToJsonAndBack_CreatesAUnit()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };
            var value = unit;
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<Unit>(json, settings);
            AreEqual(value, newValue);
        }

        [Test]
        public void TheContractResolver_EnablesSerializationSupportForMultipleTypes()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

            var value = new TestCollection
            {
                Value1 = Option<int>.Some(1),
                Value2 = none,
                Value3 = new Union<int, string>("a")
            };
            var json = SerializeObject(value, settings);
            var newValue = DeserializeObject<TestCollection>(json, settings);
            AreEqual(value.Value1, newValue.Value1);
            AreEqual(value.Value2, newValue.Value2);
            AreEqual(value.Value3, newValue.Value3);
        }

        private class TestCollection
        {
            public Option<int> Value1 { get; set; }
            public None Value2 { get; set; }
            public Union<int, string> Value3 { get; set; }
        }
    }
}
