using NUnit.Framework;
using SuccincT.Unions;
using System.Diagnostics.CodeAnalysis;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.None;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class NoneTests
    {
        [Test]
        public static void NoneToString_GivesMeanfulResult()
        {
            var value = none;
            AreEqual("!none!", value.ToString());
        }

        [Test]
        public static void NonesEquality_NoneEqualsNoneOnly()
        {
            var value1 = none;
            var value2 = new None();
            var value3 = default(None);
            var value4 = 0;

            Multiple(
                () => {
                    AreEqual(0, value1.GetHashCode());
                    AreEqual(0, value2.GetHashCode()); 
                    AreEqual(0, value3.GetHashCode());
                    IsTrue(value1.Equals(value2));
                    IsTrue(value3.Equals(value2));
                    IsTrue(value2.Equals(value1));
                    IsTrue(value1 == value2);
                    IsTrue(value3 == value1);
                    IsFalse(value2 != value1);
                    IsFalse(value1.Equals(value4));
                });
        }
    }
}
