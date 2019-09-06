using NUnit.Framework;
using SuccincT.Unions;
using System;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class UnionT1T2EqualityTests
    {
        [Test]
        public static void SameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string>(2);
            var b = new Union<int, string>(2);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string>(1);
            var b = new Union<int, string>(2);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string>("1234");
            var b = new Union<int, string>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string>("a");
            var b = new Union<int, string>("b");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void DifferentValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string>(0);
            var b = new Union<int, string>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void ComparingT1OrT2ValueWithNull_ResultsInNotEqualAndExceptionOnImplcitCastOfNull()
        {
            var union1 = new Union<int, string>(null);
            var union2 = new Union<string, int>(null);
            Multiple(
                () => {
                    IsFalse(union1.Equals(null));
                    IsFalse(union2.Equals(null));

                    Throws<InvalidCastException>(() => _ = union1 != null);
                    Throws<InvalidCastException>(() => _ = null != union1);
                    Throws<InvalidCastException>(() => _ = union2 != null);
                    Throws<InvalidCastException>(() => _ = null != union2);
                });
        }

        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT1Type()
        {
            Union<int, string> union = 1;
            AreEqual(1, union.Case1);
        }

        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT2Type()
        {
            Union<int, string> union = "string";
            AreEqual("string", union.Case2);
        }
    }
}