using NUnit.Framework;
using SuccincT.Unions;
using System;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class EitherTests
    {
        [Test]
        public void CreatingEitherLeft_LetsLeftBeReadAndNotRight()
        {
            var left = new Either<int, string>(1);
            Assert.AreEqual(1, left.Left);
            Assert.IsTrue(left.IsLeft);
            Assert.IsTrue(left.TryLeft.HasValue);
            Assert.AreEqual(1, left.TryLeft.Value);
            Assert.Throws<InvalidOperationException>(() => _ = left.Right);
            Assert.IsFalse(left.TryRight.HasValue);
        }

        [Test]
        public void CreatingEitherRight_LetsRightBeReadAndNotLeft()
        {
            var left = new Either<int, string>("2");
            Assert.AreEqual("2", left.Right);
            Assert.IsFalse(left.IsLeft);
            Assert.IsTrue(left.TryRight.HasValue);
            Assert.AreEqual("2", left.TryRight.Value);
            Assert.Throws<InvalidOperationException>(() => _ = left.Left);
            Assert.IsFalse(left.TryLeft.HasValue);
        }

        [Test]
        public void TwoLeftEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>(1);
            Assert.AreEqual(either1, either2);
            Assert.IsTrue(either1 == either2);
        }

        [Test]
        public void TwoRightEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<int, string>("2");
            Assert.AreEqual(either1, either2);
            Assert.IsTrue(either1 == either2);
        }

        [Test]
        public void LeftAndRightEithersWithSameValue_AreNotEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<string,int>("2");
            Assert.AreNotEqual(either1, either2);
        }

        [Test]
        public void TwoEithersWithDifferentValues_AreNotEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>("2");
            Assert.AreNotEqual(either1, either2);
            Assert.IsTrue(either1 != either2);
        }

        [Test]
        public void TwoEithersLeftWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>(null);
            var either2 = new Either<int, string>("");
            Assert.AreNotEqual(either1, either2);
            Assert.IsTrue(either1 != either2);
        }

        [Test]
        public void TwoEithersRightWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>("xxx");
            var either2 = new Either<int, string>(null);
            Assert.AreNotEqual(either1, either2);
            Assert.IsTrue(either1 != either2);
        }

        [Test]
        public void LeftEitherHashCode_MatchesLeftValuesHashCode()
        {
            var either = new Either<int, string>(1);
            Assert.AreEqual(either.GetHashCode(), 1.GetHashCode());
        }

        [Test]
        public void RightEitherHashCode_MatchesRightValuesHashCode()
        {
            var either = new Either<int, string>("yellow");
            Assert.AreEqual(either.GetHashCode(), "yellow".GetHashCode());
        }

        [Test]
        public void NullRightEitherHashCode_GivesZeroHashCode()
        {
            var either = new Either<int, string>(null);
            Assert.AreEqual(either.GetHashCode(), 0);
        }
    }
}
