using NUnit.Framework;
using SuccincT.Unions;
using System;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class UnionT1T2T3EqualityTests
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public static void voidSameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>(2);
            var b = new Union<int, string, Colors>(2);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidDifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>(1);
            var b = new Union<int, string, Colors>(2);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void voidSameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>("1234");
            var b = new Union<int, string, Colors>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidDifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>("abc");
            var b = new Union<int, string, Colors>("def");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void voidSameT3Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>(Colors.Blue);
            var b = new Union<int, string, Colors>(Colors.Blue);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidDifferentT3Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>(Colors.Blue);
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void voidDifferentT1T2Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>(2);
            var b = new Union<int, string, Colors>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidDifferentT1T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>(0);
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidDifferentT2T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>("xyz");
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void voidComparingT1T2OrT3ValueWithNull_ResultsInNotEqualAndExceptionOnImplcitCastOfNull()
        {
            var union1 = new Union<string, Colors>(null);
            var union2 = new Union<int, string, Colors>(null);
            var union3 = new Union<Colors, int, string>(null);

            Multiple(
                () => {
                    IsFalse(union1.Equals(null));
                    IsFalse(union2.Equals(null));
                    IsFalse(union3.Equals(null));

                    _ = Throws<InvalidCastException>(() => _ = union1 != null);
                    _ = Throws<InvalidCastException>(() => _ = null != union1);

                    _ = Throws<InvalidCastException>(() => _ = union2 != null);
                    _ = Throws<InvalidCastException>(() => _ = null != union2);

                    _ = Throws<InvalidCastException>(() => _ = union3 != null);
                    _ = Throws<InvalidCastException>(() => _ = null != union3);
                });
        }

        [Test]
        public static void voidComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>(2);

            IsFalse(a.Equals(null));
        }

        [Test]
        public static void voidComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>("1234");
            
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void voidComparingT3ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>(Colors.Red);
            
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void voidT1HashCode_IsBasedOnT1Value()
        {
            var a = new Union<int, string, Colors>(2);
            
            AreEqual(a.GetHashCode(), 2.GetHashCode());
        }

        [Test]
        public static void voidT2HashCode_IsBasedOnT2Value()
        {
            var a = new Union<int, string, Colors>("cow");
            
            AreEqual(a.GetHashCode(), "cow".GetHashCode());
        }

        [Test]
        public static void voidT3HashCode_IsBasedOnT3Value()
        {
            var a = new Union<int, string, Colors>(Colors.Blue);
            
            AreEqual(a.GetHashCode(), Colors.Blue.GetHashCode());
        }

        [Test]
        public static void VoidUnionCanBeCorrectlyCreatedFromT1Type()
        {
            Union<int, string, Colors> union = 1;
            AreEqual(1, union.Case1);
        }

        [Test]
        public static void VoidUnionCanBeCorrectlyCreatedFromT2Type()
        {
            Union<int, string, Colors> union = "string";
            AreEqual("string", union.Case2);
        }

        [Test]
        public static void VoidUnionCanBeCorrectlyCreatedFromT3Type()
        {
            Union<int, string, Colors> union = Colors.Blue;
            AreEqual(Colors.Blue, union.Case3);
        }
    }
}