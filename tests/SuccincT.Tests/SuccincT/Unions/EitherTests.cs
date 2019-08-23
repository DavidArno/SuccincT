using System;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class EitherTests
    {
        [Test]
        public static void CreatingEitherLeft_LetsLeftBeReadAndNotRight()
        {
            var left = new Either<int, string>(1);
            AreEqual(1, left.Left);
            IsTrue(left.IsLeft);
            IsTrue(left.TryLeft.HasValue);
            AreEqual(1, left.TryLeft.Value);
            Throws<InvalidOperationException>(() => _ = left.Right);
            IsFalse(left.TryRight.HasValue);
        }

        [Test]
        public static void CreatingEitherRight_LetsRightBeReadAndNotLeft()
        {
            var right = new Either<int, string>("2");
            AreEqual("2", right.Right);
            IsFalse(right.IsLeft);
            IsTrue(right.TryRight.HasValue);
            AreEqual("2", right.TryRight.Value);
            Throws<InvalidOperationException>(() => _ = right.Left);
            IsFalse(right.TryLeft.HasValue);
        }

        [Test]
        public static void TwoLeftEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>(1);
            AreEqual(either1, either2);
            IsTrue(either1 == either2);
        }

        [Test]
        public static void TwoRightEithersWithSameValue_AreEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<int, string>("2");
            AreEqual(either1, either2);
            IsTrue(either1 == either2);
        }

        [Test]
        public static void LeftAndRightEithersWithSameValue_AreNotEqual()
        {
            var either1 = new Either<int, string>("2");
            var either2 = new Either<string,int>("2");
            AreNotEqual(either1, either2);
        }

        [Test]
        public static void TwoEithersWithDifferentValues_AreNotEqual()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<int, string>("2");
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public static void TwoEithersLeftWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>(null);
            var either2 = new Either<int, string>("");
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public static void TwoEithersRightWithNull_AreNotEqual()
        {
            var either1 = new Either<int, string>("xxx");
            var either2 = new Either<int, string>(null);
            AreNotEqual(either1, either2);
            IsTrue(either1 != either2);
        }

        [Test]
        public static void LeftEitherHashCode_MatchesLeftValuesHashCode()
        {
            var either = new Either<int, string>(1);
            AreEqual(either.GetHashCode(), 1.GetHashCode());
        }

        [Test]
        public static void RightEitherHashCode_MatchesRightValuesHashCode()
        {
            var either = new Either<int, string>("yellow");
            AreEqual(either.GetHashCode(), "yellow".GetHashCode());
        }

        [Test]
        public static void NullRightEitherHashCode_GivesZeroHashCode()
        {
            var either = new Either<int, string>(null);
            AreEqual(either.GetHashCode(), 0);
        }
    }
}
