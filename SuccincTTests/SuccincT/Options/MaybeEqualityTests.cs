using NUnit.Framework;
using SuccincT.Options;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    // ReSharper disable SuspiciousTypeConversion.Global
    [TestFixture]
    public class MaybeEqualityTests
    {
        [Test]
        public void TwoSomeValues_AreEqualAndHaveSameHashCode()
        {
            var a = Maybe<string>.Some("1234");
            var b = Maybe<string>.Some("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void MaybeAndOption_AreEqual()
        {
            var a = Maybe<string>.Some("1234");
            var b = Option<string>.Some("1234");
            IsTrue(a.Equals((Maybe<string>)b));
            IsTrue(a == b);
        }

        [Test]
        public void OptionAndMaybe_AreEqual()
        {
            var a = Option<string>.Some("1234");
            var b = Maybe<string>.Some("1234");
            IsTrue(a.Equals((Option<string>)b));
            IsTrue(a == (Option<string>)b);
        }

        [Test]
        public void TwoNones_AreEqualAndHaveSameHashCode()
        {
            var a = Maybe<string>.None();
            var b = Maybe<string>.None();
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void SomeValueAndNone_AreNotEqual()
        {
            var a = Maybe<string>.Some("1234");
            var b = Maybe<string>.None();
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void DifferentSomeValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = Maybe<string>.Some("1234");
            var b = Maybe<string>.Some("12345");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void OptionAndMaybe_AreEqualWithoutCasting()
        {
            var a = Maybe<string>.Some("1234");
            var b = Option<string>.Some("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a, b);
        }

        [Test]
        public void EmptyOptionAndMaybe_AreEqualWithoutCasting()
        {
            var a = Maybe<string>.None();
            var b = Option<string>.None();
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a, b);
        }

        [Test]
        public void DifferentValuesInOptionAndMaybe_AreNotEqualWithoutCasting()
        {
            var a = Maybe<string>.Some("1234");
            var b = Option<string>.Some("5678");
            IsFalse(a.Equals(b));
            IsFalse(a == b);
            AreNotEqual(a, b);
            IsTrue(a != b);
        }
    }
}