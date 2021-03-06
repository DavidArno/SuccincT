﻿using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using SuccincT.JSON;
using SuccincT.Unions;
using static Newtonsoft.Json.JsonConvert;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.JSON
{
    [TestFixture]
    public static class UnionOf2ConverterTests
    {
        [Test]
        public static void WhenProvidedWithNoCase_DeserializeFailsWithJsonException()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf2Converter());

            _ = Throws<JsonException>(() => {
                _ = DeserializeObject<Union<List<int>, string> > ("{k:1, value:2}", settings);
            });
        }

        [Test]
        public static void WhenProvidedWithNoValue_DeserializeFailsWithJsonException()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf2Converter());

            _ = Throws<JsonException>(() => {
                _ = DeserializeObject<Union<List<int>, string>>("{case:\"Case1\", k:2}", settings);
            });
        }

        [Test]
        public static void ConvertingUnionToJsonAndBack_PreservesUnionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf2Converter());
            var union = new Union<List<int>, string>(new List<int> { 1, 2 });
            var json = SerializeObject(union, settings);
            var newUnion = DeserializeObject<Union<List<int>, string>>(json, settings);

            AreEqual(2, newUnion.Case1.Count);
            AreEqual(Variant.Case1, newUnion.Case);
            AreEqual(1, newUnion.Case1[0]);
            AreEqual(2, newUnion.Case1[1]);
        }

        [Test]
        public static void ConvertingListOfUnionsToJsonAndBack_PreservesUnionState()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UnionOf2Converter());
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
    }
}
