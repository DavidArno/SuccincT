using NUnit.Framework;
using static NUnit.Framework.Assert;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class AnyTests
    {
        [Test]
        public void AnyTwoAnys_AreEqual()
        {
            var x = any;
            var y = any;
            var z = __;

            Multiple(() =>
            {
                AreEqual(x, y);
                AreEqual(x, z);
                IsTrue(x == y);
                IsTrue(y == z);
                IsFalse(y != x);
                IsFalse(z != x);
            });
        }

        [Test]
        public void HashCodeForAny_IsZero() => AreEqual(0, any.GetHashCode());

        [Test]
        public void ToStringForAny_IsAsterisk() => AreEqual("*", __.ToString());
    }
}
