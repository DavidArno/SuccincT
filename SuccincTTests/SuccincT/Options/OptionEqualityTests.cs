using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using SuccincT.Options;
using static NUnit.Framework.Assert;

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
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TwoNones_AreEqualAndHaveSameHashCode()
        {
            var a = Option<string>.None();
            var b = Option<string>.None();
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void SomeValueAndNone_AreNotEqual()
        {
            var a = Option<string>.Some("1234");
            var b = Option<string>.None();
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void DifferentSomeValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = Option<string>.Some("1234");
            var b = Option<string>.Some("12345");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingSomeValueWithNull_ResultsInNotEqual()
        {
            var a = Option<string>.Some("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingNoneWithNull_ResultsInNotEqual()
        {
            var a = Option<string>.None();
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test, SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void OptionAndMaybe_AreEqualWithoutCasting()
        {
            var a = Option<string>.Some("1234");
            var b = Maybe<string>.Some("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a, b);
        }

        [Test, SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void EmptyOptionAndMaybe_AreEqualWithoutCasting()
        {
            var a = Option<string>.None();
            var b = Maybe<string>.None();
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            IsTrue(b == a);
            AreEqual(a, b);
        }

        [Test, SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void DifferentOptionAndMaybe_AreNotEqualWithoutCasting()
        {
            var a = Option<string>.Some("1234");
            var b = Maybe<string>.Some("5678");
            IsFalse(a.Equals(b));
            IsFalse(b.Equals(a));
            IsFalse(a == b);
            IsFalse(b == a);
            AreNotEqual(a, b);
            IsTrue(a != b);
            IsTrue(b != a);
        }

        [Test]
        public void EmptyOptionsOfSameType_AreReferentiallyEqual()
        {
            var a = Option<string>.None();
            var b = Option<string>.None();
            AreSame(a, b);
        }
    }
}