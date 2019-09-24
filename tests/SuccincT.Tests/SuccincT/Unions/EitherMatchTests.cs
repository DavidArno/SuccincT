using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.EitherState;

namespace SuccincTTests.SuccincT.Unions
{
    public static class EitherMatchTests
    {

        [Test]
        public static void EitherWithTLeftAndNoLeftMatchWithExec_ThrowsException()
        {
            var either = new Either<int, string>(2);
            _ = Throws<NoMatchException>(() => either.Match().Right().Do(x => { }).Exec());
        }

        [Test]
        public static void EitherWithTRight_MatchesBasicRightCorrectly()
        {
            var either = new Either<int, string>("la la");
            var result = either.Match<bool>().Left().Do(x => false).Right().Do(x => true).Result();
            IsTrue(result);
        }

        [Test]
        public static void EitherWithTRight_MatchesBasicRightCorrectlyWithExec()
        {
            var either = new Either<int, string>("la la");
            var result = 0;
            either.Match().Left().Do(x => result = 1).Right().Do(x => result = 2).Exec();
            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRight_UsesElseIfNoRightMatch()
        {
            var either = new Either<int, string>("fred");
            var result = either.Match<bool>().Left().Do(x => false).Else(x => true).Result();
            IsTrue(result);
        }

        [Test]
        public static void EitherWithTRight_UsesElseValueIfNoRightMatch()
        {
            var either = new Either<int, string>("fred");
            var result = either.Match<bool>().Left().Do(x => false).Else(true).Result();
            IsTrue(result);
        }

        [Test]
        public static void EitherWithTLeft_UsesElseIfNoLeftMatchWithExec()
        {
            var either = new Either<int, string>("fred");
            var result = 0;
            either.Match().Left().Do(x => result = 1).Else(x => result = 2).Exec();
            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRightAndNoRightMatch_ThrowsException()
        {
            var either = new Either<int, string>("la la");
            _ = Throws<NoMatchException>(() => either.Match<bool>().Left().Do(x => false).Result());
        }

        [Test]
        public static void EitherWithTRightAndNoRightMatchWithExec_ThrowsException()
        {
            var either = new Either<int, string>("la la");
            _ = Throws<NoMatchException>(() => either.Match().Left().Do(x => { }).Exec());
        }

        [Test]
        public static void EitherWithTLeft_SimpleCaseExpressionSupported()
        {
            var either = new Either<int, string>(2);
            var result = either.Match<int>().Left().Do(1).Right().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTLeft_CaseOfExpressionSupported()
        {
            var either = new Either<int, string>(2);
            var result = either.Match<int>().Left().Of(2).Do(0).Left().Do(1).Right().Do(2).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void EitherWithTLeft_CaseWhereExpressionSupported()
        {
            var either = new Either<int, string>(3);
            var result = either.Match<int>().Left().Of(2).Do(0).Left().Where(x => x == 3).Do(1).Right().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTRight_SimpleCaseExpressionSupported()
        {
            var either = new Either<int, string>("2");
            var result = either.Match<int>().Left().Do(1).Right().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRight_CaseOfExpressionSupported()
        {
            var either = new Either<int, string>("1");
            var result = either.Match<int>().Left().Do(0).Right().Of("1").Do(2).Right().Do(1).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRight_CaseWhereExpressionSupported()
        {
            var either = new Either<int, string>("2");
            var result = either.Match<int>()
                              .Left().Do(0).Right().Where(x => x == "2").Do(2).Right().Of("1").Do(1).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTLeft_MatchesComplexLeftCorrectly()
        {
            var either = new Either<int, string>(2);
            var result = either.Match<int>()
                               .Left().Of(1).Do(x => 1)
                               .Left().Do(x => 2)
                               .Right().Do(x => 3).Result();

            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTLeft_MatchesComplexLeftCorrectlyWithExec()
        {
            var either = new Either<int, string>(2);
            var result = 0;
            either.Match().Left().Of(1).Do(x => result = 1)
                  .Left().Do(x => result = 2)
                  .Right().Do(x => result = 3).Exec();

            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRight_MatchesComplexRightCorrectly()
        {
            var either = new Either<int, string>("t");
            var result = either.Match<int>()
                               .Left().Of(1).Do(x => 1)
                               .Left().Do(x => 2)
                               .Right().Of("t").Do(x => 3)
                               .Right().Do(x => 4).Result();

            AreEqual(3, result);
        }

        [Test]
        public static void EitherWithTRight_MatchesComplexRightCorrectlyWithExec()
        {
            var either = new Either<int, string>("t");
            var result = 0;
            either.Match().Left().Of(1).Do(x => result = 1)
                  .Left().Do(x => result = 2)
                  .Right().Of("t").Do(x => result = 3)
                  .Right().Do(x => result = 4).Exec();

            AreEqual(3, result);
        }

        [Test]
        public static void EitherWithTLeft_MatchesOfOrStyleLeftCorrectly()
        {
            var either = new Either<int, string>(2);
            var result = either.Match<int>()
                               .Left().Of(1).Or(2).Do(x => 1)
                               .Left().Do(x => 3)
                               .Right().Do(x => 4).Result();

            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTLeft_MatchesOfOrStyleLeftCorrectlyWithExec()
        {
            var either = new Either<int, string>(2);
            var result = 0;
            either.Match().Left().Of(1).Or(2).Do(x => result = 1)
                  .Left().Do(x => result = 3)
                  .Right().Do(x => result = 4).Exec();

            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTRight_MatchesOfOrStyleRightCorrectly()
        {
            var either = new Either<int, string>("x");
            var result = either.Match<int>()
                               .Right().Of("y").Or("x").Do(x => 1)
                               .Right().Do(x => 3)
                               .Left().Do(x => 4).Result();

            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTRight_MatchesOfOrStyleRightCorrectlyWithExec()
        {
            var either = new Either<int, string>("x");
            var result = 0;

            either.Match()
                  .Right().Of("y").Or("x").Do(x => result = 1)
                  .Right().Do(x => result = 3)
                  .Left().Do(x => result = 4).Exec();

            AreEqual(1, result);
        }

        [Test]
        public static void EitherWithTLeft_HandlesMultipleOfOrStyleLeftMatchesCorrectly()
        {
            var either = new Either<int, string>(2);
            var result = either.Match<int>()
                               .Left().Of(1).Or(0).Do(x => x)
                               .Right().Do(x => 10)
                               .Left().Of(3).Or(2).Do(x => x * 2)
                               .Left().Do(x => x * 3).Result();

            var result2 = either switch {
                (Left, var x, _) when x == 0 || x == 1 => x,
                (Right, _, _) => 10,
                (Left, var x, _) when x == 2 || x == 3 => x * 2,
                (_, var x, _) => x * 3
            };

            AreEqual(4, result);
            AreEqual(4, result2);
        }

        [Test]
        public static void EitherWithTLeft_HandlesMultipleOfOrStyleLeftMatchesCorrectlyWithExec()
        {
            var either = new Either<int, string>(2);
            var result = 0;
            either.Match()
                  .Left().Of(1).Or(0).Do(x => result = 1)
                  .Right().Do(x => result = 4)
                  .Left().Of(3).Or(2).Do(x => result = 2)
                  .Left().Do(x => result = 3).Exec();

            AreEqual(2, result);
        }

        [Test]
        public static void EitherWithTRight_HandlesMultipleOfOrStyleRightMatchesCorrectly()
        {
            var either = new Either<int, string>("c");
            var result = either.Match<int>()
                               .Right().Of("a").Or("b").Do(x => 1)
                               .Left().Do(x => 2)
                               .Right().Of("c").Or("d").Do(x => 3)
                               .Right().Do(x => 4).Result();

            var result2 = either switch {
                (Right, _, var x) when x == "a" || x == "b" => 1,
                (Left, _, _) => 2,
                (Right, _, var x) when x == "c" || x == "d" => 3,
                (_, _, var x) => 4
            };

            AreEqual(3, result);
            AreEqual(3, result2);
        }

        [Test]
        public static void EitherWithTRight_HandlesMultipleOfOrStyleRightMatchesCorrectlyWithExec()
        {
            var either = new Either<int, string>("c");
            var result = 0;
            either.Match()
                  .Right().Of("a").Or("b").Do(x => result = 1)
                  .Left().Do(x => result = 2)
                  .Right().Of("c").Or("d").Do(x => result = 3)
                  .Right().Do(x => result = 4).Exec();

            AreEqual(3, result);
        }
    }
}