﻿using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public static class UnionIgnoreElseTests
    {
        private enum Colors { Green, Blue }

        private enum Animals { Sheep }

        [Test]
        public static void UnionOfTwoWithT1_UsesIgnoreElseIfNoCase1MatchWithExec()
        {
            var union = new Union<int, string>(1);
            var result = 0;
            union.Match().Case2().Do(x => result = 1).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfTwoWithT2_UsesIgnoreElseIfNoCase2MatchWithExec()
        {
            var union = new Union<int, string>("fred");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void EitherWithTLeft_UsesIgnoreElseIfNoLeftMatchWithExec()
        {
            var either = new Either<int, string>(1);
            var result = 0;
            either.Match().Right().Do(x => result = 1).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void EitherWithTRight_UsesIgnoreElseIfNoCase2MatchWithExec()
        {
            var either = new Either<int, string>("fred");
            var result = 0;
            either.Match().Left().Do(x => result = 1).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfThreeWithT1_UsesIgnoreElseIfNoCase1MatchWithExec()
        {
            var union = new Union<int, string, Colors>(1);
            var result = 0;
            union.Match().Case2().Do(x => result = 1).Case3().Do(x => result = 2).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfThreeWithT2_UsesIgnoreElseIfNoCase2MatchWithExec()
        {
            var union = new Union<int, string, Colors>("fred");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case3().Do(x => result = 2).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfThreeWithT3_UsesIgnoreElseIfNoCase3MatchWithExec()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case2().Do(x => result = 2).IgnoreElse().Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfFourWithT1_UsesIgnoreElseIfNoCase1MatchWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(1);
            var result = 0;
            union.Match()
                 .Case2()
                 .Do(x => result = 1)
                 .Case3()
                 .Do(x => result = 2)
                 .Case4()
                 .Do(x => result = 3)
                 .IgnoreElse()
                 .Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfFourWithT2_UsesIgnoreElseIfNoCase2MatchWithExec()
        {
            var union = new Union<int, string, Colors, Animals>("fred");
            var result = 0;
            union.Match()
                 .Case1()
                 .Do(x => result = 1)
                 .Case3()
                 .Do(x => result = 2)
                 .Case4()
                 .Do(x => result = 3)
                 .IgnoreElse()
                 .Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfFourWithT3_UsesIgnoreElseIfNoCase3MatchWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = 0;
            union.Match()
                 .Case1()
                 .Do(x => result = 1)
                 .Case2()
                 .Do(x => result = 2)
                 .Case4()
                 .Do(x => result = 3)
                 .IgnoreElse()
                 .Exec();
            AreEqual(0, result);
        }

        [Test]
        public static void UnionOfFourWithT4_UsesIgnoreElseIfNoCase4MatchWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = 0;
            union.Match()
                 .Case1()
                 .Do(x => result = 1)
                 .Case2()
                 .Do(x => result = 2)
                 .Case3()
                 .Do(x => result = 3)
                 .IgnoreElse()
                 .Exec();
            AreEqual(0, result);
        }
    }
}