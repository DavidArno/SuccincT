using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

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
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT1Values_AreNotEqual()
        {
            var a = new Union<int, string>(1);
            var b = new Union<int, string>(2);
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void SameT2Values_AreEqualAndHaveSameHashCode()
        {
            var a = new Union<int, string>("1234");
            var b = new Union<int, string>("1234");
            IsTrue(a.Equals(b));
            IsTrue(a == b);
            AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void DifferentT2Values_AreNotEqual()
        {
            var a = new Union<int, string>("a");
            var b = new Union<int, string>("b");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
        }

        [Test]
        public void DifferentValues_AreNotEqualAndHaveDifferentHashCodes()
        {
            var a = new Union<int, string>(0);
            var b = new Union<int, string>("1234");
            IsFalse(a.Equals(b));
            IsTrue(a != b);
            AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ComparingT1ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string>(1);
            IsFalse(a.Equals(null));
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT2ValueWithNull_ResultsInNotEqual()
        {
            var a = new Union<int, string>("1");
            IsFalse(a.Equals(null));
            IsFalse(a == null);
            IsFalse(null == a);
            IsTrue(a != null);
            IsTrue(null != a);
        }

        [Test]
        public void ComparingT2NullValueWithNull_ResultsInEqual()
        {
            var a = new Union<int, string>(null);
            IsTrue(a.Equals(null));
            IsTrue(a == null);
            IsTrue(null == a);
            IsFalse(a != null);
            IsFalse(null != a);
        }

        [Test]
        public void UnionCanBeCorrectlyCreatedFromT1Type()
        {
            Union<int, string> union = 1;
            AreEqual(1, union.Case1);
        }

        [Test]
        public void UnionCanBeCorrectlyCreatedFromT2Type()
        {
            Union<int, string> union = "string";
            AreEqual("string", union.Case2);
        }
    }
}