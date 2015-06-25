using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class OptionEqualityTests
    {
        [Test]
        public void SameSomeValue_AreEqualAndHaveSameHashCode()
        {
            var a = Option<string>.Some("1234");
            var b = Option<string>.Some("1234");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TwoNones_AreEqualAndHaveSameHashCode()
        {
            var a = Option<string>.None();
            var b = Option<string>.None();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void SomeValueAndNone_AreNotEqual()
        {
            var a = Option<string>.Some("1234");
            var b = Option<string>.None();
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        [Test]
        public void DifferentSomeValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = Option<string>.Some("1234");
            var b = Option<string>.Some("12345");
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingSomeValueWithNull_ResultsInNotEqual()
        {
            var a = Option<string>.Some("1234");
            Assert.IsFalse(a.Equals(null));
            Assert.IsTrue(a != null);
            Assert.IsTrue(null != a);
        }

        [Test]
        public void ComparingNoneWithNull_ResultsInNotEqual()
        {
            var a = Option<string>.None();
            Assert.IsFalse(a.Equals(null));
            Assert.IsTrue(a != null);
            Assert.IsTrue(null != a);
        }
    }
}