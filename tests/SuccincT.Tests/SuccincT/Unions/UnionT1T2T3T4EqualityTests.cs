using NUnit.Framework;
using SuccincT.Unions;
using System;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class UnionT1T2T3T4EqualityTests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Cat, Dog, Cow }

        [Test]
        public static void SameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(2);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(3);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public static void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>("1234");
            var b = new Union<int, string, Colors, Animals>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>("abc");
            var b = new Union<int, string, Colors, Animals>("def");
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public static void SameT3Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Blue);
            var b = new Union<int, string, Colors, Animals>(Colors.Blue);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT3Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Blue);
            var b = new Union<int, string, Colors, Animals>(Colors.Red);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public static void SameT4Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Cat);
            var b = new Union<int, string, Colors, Animals>(Animals.Cat);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT4Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Cat);
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public static void DifferentT1T2Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT1T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT1T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(Animals.Dog);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT2T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>("xyz");
            var b = new Union<int, string, Colors, Animals>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT2T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>("xyz");
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentT3T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Red);
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void ComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void ComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>("1234");
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void ComparingT3ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Red);
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void ComparingT4ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Dog);
            IsFalse(a.Equals(null));
        }

        [Test]
        public static void voidComparingT1T2T3OrT4ValueWithNull_ResultsInNotEqualAndExceptionOnImplcitCastOfNull()
        {
            var union1 = new Union<string, Colors, int, Animals>(null);
            var union2 = new Union<int, string, Colors, Animals>(null);
            var union3 = new Union<Colors, int, string, Animals>(null);
            var union4 = new Union<Colors, int, Animals, string>(null);

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

                    _ = Throws<InvalidCastException>(() => _ = union4 != null);
                    _ = Throws<InvalidCastException>(() => _ = null != union4);
                });
        }

        [Test]
        public static void T1HashCode_IsBasedOnT1Value()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            AreEqual(a.GetHashCode(), 2.GetHashCode());
        }

        [Test]
        public static void T2HashCode_IsBasedOnT2Value()
        {
            var a = new Union<int, string, Colors, Animals>("party");
            AreEqual(a.GetHashCode(), "party".GetHashCode());
        }

        [Test]
        public static void T3HashCode_IsBasedOnT3Value()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Blue);
            AreEqual(a.GetHashCode(), Colors.Blue.GetHashCode());
        }

        [Test]
        public static void T4HashCode_IsBasedOnT4Value()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Cow);
            AreEqual(a.GetHashCode(), Animals.Cow.GetHashCode());
        }
        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT1Type()
        {
            Union<int, string, Colors, Animals> union = 1;
            AreEqual(1, union.Case1);
        }

        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT2Type()
        {
            Union<int, string, Colors, Animals> union = "string";
            AreEqual("string", union.Case2);
        }

        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT3Type()
        {
            Union<int, string, Colors, Animals> union = Colors.Blue;
            AreEqual(Colors.Blue, union.Case3);
        }

        [Test]
        public static void UnionCanBeCorrectlyCreatedFromT4Type()
        {
            Union<int, string, Colors, Animals> union = Animals.Cow;
            AreEqual(Animals.Cow, union.Case4);
        }
    }
}