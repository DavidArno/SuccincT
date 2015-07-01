using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class UnionT1T2EqualityTests
    {
        [Test]
        public void SameT1Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string>(2);
            var b = new Union<int, string>(2);
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1Values_ArentEqual()
        {
            var a = new Union<int, string>(1);
            var b = new Union<int, string>(2);
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        [Test]
        public void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string>("1234");
            var b = new Union<int, string>("1234");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2Values_ArentEqual()
        {
            var a = new Union<int, string>("a");
            var b = new Union<int, string>("b");
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }

        [Test]
        public void DifferentValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string>(0);
            var b = new Union<int, string>("1234");
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string>(1);
            Assert.IsFalse(a.Equals(null));
            Assert.IsTrue(a != null);
            Assert.IsTrue(null != a);
        }

        [Test]
        public void ComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string>(null);
            Assert.IsFalse(a.Equals(null));
            Assert.IsTrue(a != null);
            Assert.IsTrue(null != a);
        }
    }
}