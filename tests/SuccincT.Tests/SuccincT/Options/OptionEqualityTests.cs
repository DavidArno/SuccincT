﻿using NUnit.Framework;
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
        public void ComparingNoneWithNull_ResultsInEqual()
        {
            var a = Option<string>.None();
            IsTrue(a.Equals(null));
            IsTrue(a == null);
            IsTrue(null == a);
            IsFalse(null != a);
            IsFalse(a != null);
        }

        [Test]
        public void ComparingSomeOptionWithItsValue_ResultsInEqual()
        {
            var a = Option<int>.Some(1);
            IsTrue(a == 1);
            IsTrue(1 == a);
            IsFalse(a != 1);
            IsFalse(1 != a);
        }
    }
}