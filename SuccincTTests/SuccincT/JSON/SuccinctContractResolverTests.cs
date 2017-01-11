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
    public class SuccinctContractResolverTests
    {
        [Test]
        public void ContractResolver_CanConvertUnion2ToJsonAndBack()
        {
            var union1 = new Union<int, string>(1);
            var settings = new JsonSerializerSettings
            {
                ContractResolver = SuccinctContractResolver.Instance
            };

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
