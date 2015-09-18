using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class UnionT1T2T3T4EqualityTests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Cat, Dog, Cow }

        [Test]
        public void SameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(2);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(3);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>("1234");
            var b = new Union<int, string, Colors, Animals>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>("abc");
            var b = new Union<int, string, Colors, Animals>("def");
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public void SameT3Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Blue);
            var b = new Union<int, string, Colors, Animals>(Colors.Blue);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT3Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Blue);
            var b = new Union<int, string, Colors, Animals>(Colors.Red);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public void SameT4Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Cat);
            var b = new Union<int, string, Colors, Animals>(Animals.Cat);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT4Values_ArentEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Cat);
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsFalse(a == b);
        }

        [Test]
        public void DifferentT1T2Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            var b = new Union<int, string, Colors, Animals>(Animals.Dog);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>("xyz");
            var b = new Union<int, string, Colors, Animals>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>("xyz");
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT3T4Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Red);
            var b = new Union<int, string, Colors, Animals>(Animals.Cow);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(2);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT3ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Colors.Red);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT4ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors, Animals>(Animals.Dog);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }
    }
}