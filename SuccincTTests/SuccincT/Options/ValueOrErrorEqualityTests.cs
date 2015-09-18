using NUnit.Framework;
using SuccincT.Options;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class ValueOrErrorEqualityTests
    {
        [Test]
        public void SameValues_AreEqualAndHaveSameHashCode()
        {
            var a = ValueOrError.WithValue("1234");
            var b = ValueOrError.WithValue("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void SameErrors_AreEqualAndHaveSameHashCode()
        {
            var a = ValueOrError.WithError("1234");
            var b = ValueOrError.WithError("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void SameStringInOneValueOneError_AreNotEqual()
        {
            var a = ValueOrError.WithValue("1234");
            var b = ValueOrError.WithError("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void DifferentValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = ValueOrError.WithValue("1234");
            var b = ValueOrError.WithValue("12345");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentErrors_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = ValueOrError.WithError("1234");
            var b = ValueOrError.WithError("12345");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingValueWithNull_ResultsInNotEqual()
        {
            var a = ValueOrError.WithValue("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingErrorWithNull_ResultsInNotEqual()
        {
            var a = ValueOrError.WithError("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }
    }
}