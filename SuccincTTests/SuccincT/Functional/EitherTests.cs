using System;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class EitherTests
    {
        [Test]
        public void CreatingEitherLeft_LetsLeftBeReadAndNotRight()
        {
            var left = new Either<int, string>(1);
            AreEqual(1, left.Left);
            IsTrue(left.IsLeft);
            IsTrue(left.TryLeft.HasValue);
            AreEqual(1, left.TryLeft.Value);
            Throws<InvalidOperationException>(() => Unit.Ignore(left.Right));
            IsFalse(left.TryRight.HasValue);
        }

        [Test]
        public void CreatingEitherRight_LetsRightBeReadAndNotLeft()
        {
            var left = new Either<int, string>("2");
            AreEqual("2", left.Right);
            IsFalse(left.IsLeft);
            IsTrue(left.TryRight.HasValue);
            AreEqual("2", left.TryRight.Value);
            Throws<InvalidOperationException>(() => Unit.Ignore(left.Left));
            IsFalse(left.TryLeft.HasValue);
        }

        [Test]
        public void TwoLeftEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>(1);
            AreEqual(either1, either2);
            IsTrue(either1 == either2);
        }

        [Test]
        public void TwoRightEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<int, string>("2");
            AreEqual(either1, either2);
            IsTrue(either1 == either2);
        }

        [Test]
        public void LeftAndRightEithersWithSameValue_AreNotEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<string,int>("2");
            AreNotEqual(either1, either2);
        }

        [Test]
        public void TwoEithersWithDifferentValues_AreNotEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>("2");
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public void TwoEithersLeftWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>(null);
            var either2 = new Either<int, string>("");
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public void TwoEithersRightWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>("xxx");
            var either2 = new Either<int, string>(null);
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public void LeftEitherHashCode_MatchesLeftValuesHashCode()
        {
            var either = new Either<int, string>(1);
            AreEqual(either.GetHashCode(), 1.GetHashCode());
        }

        [Test]
        public void RightEitherHashCode_MatchesLeftValuesHashCode()
        {
            var either = new Either<int, string>("yellow");
            AreEqual(either.GetHashCode(), "yellow".GetHashCode());
        }
    }
}
