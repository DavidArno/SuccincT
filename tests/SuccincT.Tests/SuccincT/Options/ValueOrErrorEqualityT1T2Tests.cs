using System;
using NUnit.Framework;
using SuccincT.Options;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public static class ValueOrErrorEqualityT1T2Tests
    {
        [Test]
        public static void SameValues_AreEqualAndHaveSameHashCode()
        {
            var a = WithValue("1234");
            var b = WithValue("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void SameErrors_AreEqualAndHaveSameHashCode()
        {
            var error = new Exception("1234");
            var a = WithError(error);
            var b = WithError(error);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void SameStringInOneValueOneError_AreNotEqual()
        {
            var error = new Exception("1234");
            var a = WithValue("1234");
            var b = WithError(error);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public static void DifferentValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = WithValue("1234");
            var b = WithValue("12345");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void DifferentErrors_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = WithError(new Exception("1234"));
            var b = WithError(new Exception("1234"));
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            IsFalse(a == b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public static void ComparingValueWithNull_ResultsInNotEqual()
        {
            var a = WithValue("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
            IsFalse(a == null);
            IsFalse(null == a);
        }

        [Test]
        public static void ComparingErrorWithNull_ResultsInNotEqual()
        {
            var a = WithError(new Exception("1234"));
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
            IsFalse(a == null);
            IsFalse(null == a);
        }

        [Test]
        public static void ComparingNullErrorWithAnotherNullError_ResultsInEqual()
        {
            var a = WithError(null);
            var b = WithError(null);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            IsFalse(a != b);
        }

        [Test]
        public static void ComparingNullValueWithAnotherNullValue_ResultsInEqual()
        {
            var a = WithValue(null);
            var b = WithValue(null);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            IsFalse(a != b);
        }

        [Test]
        public static void ComparingNullValueWithNullError_ResultsInNotEqual()
        {
            var a = WithValue(null);
            var b = WithError(null);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
            IsTrue(a != b);
        }

        private static ValueOrError<string, Exception> WithValue(string s)
            => ValueOrError<string, Exception>.WithValue(s);

        private static ValueOrError<string, Exception> WithError(Exception e)
            => ValueOrError<string, Exception>.WithError(e);
    }
}