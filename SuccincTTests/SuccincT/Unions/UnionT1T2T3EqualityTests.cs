using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class UnionT1T2T3EqualityTests
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void SameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>(2);
            var b = new Union<int, string, Colors>(2);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>(1);
            var b = new Union<int, string, Colors>(2);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>("1234");
            var b = new Union<int, string, Colors>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>("abc");
            var b = new Union<int, string, Colors>("def");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void SameT3Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string, Colors>(Colors.Blue);
            var b = new Union<int, string, Colors>(Colors.Blue);
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT3Values_ArentEqual()
        {
            var a = new Union<int, string, Colors>(Colors.Blue);
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void DifferentT1T2Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>(2);
            var b = new Union<int, string, Colors>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>(0);
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2T3Values_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string, Colors>("xyz");
            var b = new Union<int, string, Colors>(Colors.Green);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>(2);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>("1234");
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT3ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string, Colors>(Colors.Red);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }
    }
}