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
    public static class UnionOf4ConverterTests
    {
        [Test]
        public static void ConvertingUnionToJsonAndBack_PreservesUnionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf4Converter());
            var union = new Union<int, List<int>, string, List<string>>(new List<int> { 1, 2 });
            var json = SerializeObject(union, settings);
            var newUnion = DeserializeObject<Union<int, List<int>, string, List<string>>>(json, settings);

            AreEqual(2, newUnion.Case2.Count);
            AreEqual(Variant.Case2, newUnion.Case);
            AreEqual(1, newUnion.Case2[0]);
            AreEqual(2, newUnion.Case2[1]);
        }

        [Test]
        public static void ConvertingListOfUnionsToJsonAndBack_PreservesUnionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf4Converter());
            var union1 = new Union<int, string, Variant, List<int>>(1);
            var union2 = new Union<int, string, Variant, List<int>>("a");
            var union3 = new Union<int, string, Variant, List<int>>(Variant.Case4);
            var union4 = new Union<int, string, Variant, List<int>>(new List<int> { 1, 2 });
            var list = new List<Union<int, string, Variant, List<int>>> { union1, union2, union3, union4 };
            var json = SerializeObject(list, settings);
            var newList = DeserializeObject<List<Union<int, string, Variant, List<int>>>>(json, settings);

            AreEqual(4, newList.Count);
            AreEqual(Variant.Case1, newList[0].Case);
            AreEqual(Variant.Case2, newList[1].Case);
            AreEqual(Variant.Case3, newList[2].Case);
            AreEqual(Variant.Case4, newList[3].Case);
            AreEqual(1, newList[0].Case1);
            AreEqual("a", newList[1].Case2);
            AreEqual(Variant.Case4, newList[2].Case3);
            AreEqual(1, newList[3].Case4[0]);
            AreEqual(2, newList[3].Case4[1]);
        }
    }
}
