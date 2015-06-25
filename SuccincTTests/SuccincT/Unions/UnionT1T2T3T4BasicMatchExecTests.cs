using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4BasicMatchExecTests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Cat, Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2)
                         .Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case1().Of(2).Do(_ => result = 1).Case1().Do(_ => result = 2).Case2().Do(_ => result = 3)
                         .Case3().Do(_ => result = 4).Case4().Do(_ => result = 5).Exec();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(3);
            var result = -1;
            union.Match().Case1().Of(2).Do(_ => result = 0).Case1().Where(x => x == 3).Do(_ => result = 1)
                         .Case2().Do(_ => result = 2).Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2)
                         .Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("1");
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case2().Of("1").Do(_ => result = 2)
                         .Case2().Do(_ => result = 1).Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case3().Do(_ => result = 3).Case4().Do(_ => result = 4)
                         .Case2().Where(x => x == "2").Do(_ => result = 2)
                         .Case2().Of("1").Do(_ => result = 1).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2)
                         .Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case3().Of(Colors.Green).Do(_ => result = 2)
                         .Case3().Do(_ => result = 1).Case2().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case2().Do(_ => result = 3).Case4().Do(_ => result = 4)
                         .Case3().Where(x => x == Colors.Red).Do(_ => result = 2)
                         .Case3().Of(Colors.Green).Do(_ => result = 1).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2)
                         .Case3().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT4_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case4().Of(Animals.Sheep).Do(_ => result = 2)
                         .Case3().Do(_ => result = 1).Case2().Do(_ => result = 3).Case4().Do(_ => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = -1;
            union.Match().Case1().Do(_ => result = 0).Case2().Do(_ => result = 1).Case3().Do(_ => result = 2)
                         .Case4().Where(x => x == Animals.Sheep).Do(_ => result = 3)
                         .Case4().Of(Animals.Dog).Do(_ => result = 4).Exec();
            Assert.AreEqual(3, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT1AndNoCase1Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case2().Do(_ => result = 1).Case3().Do(_ => result = 2).Case4().Do(_ => result = 3).Exec();
            Assert.AreEqual(0, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>("la la");
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case3().Do(_ => result = 2).Case4().Do(_ => result = 3).Exec();
            Assert.AreEqual(0, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT3AndNoCase3Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2).Case4().Do(_ => result = 3).Exec();
            Assert.AreEqual(0, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT4AndNoCase4Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            var result = 0;
            union.Match().Case1().Do(_ => result = 1).Case2().Do(_ => result = 2).Case3().Do(_ => result = 3).Exec();
            Assert.AreEqual(0, result);
        }
    }
}