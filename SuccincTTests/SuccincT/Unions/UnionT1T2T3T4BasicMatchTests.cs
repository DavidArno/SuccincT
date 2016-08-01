using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4BasicMatchTests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Cat, Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>()
                              .Case1().Of(2).Do(0).Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(0, result);
        }

        [Test]
        public void UnionWithT1_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(3);
            var result = union.Match<int>()
                              .Case1().Of(2).Do(0).Case1().Where(x => x == 3).Do(1)
                              .Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("1");
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Of("1").Do(2).Case2().Do(1).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Do(3).Case4().Do(4)
                              .Case2().Where(x => x == "2").Do(2)
                              .Case2().Of("1").Do(1).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Of(Colors.Green).Do(2)
                              .Case3().Do(1).Case2().Do(3).Case4().Do(4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Do(3).Case4().Do(4)
                              .Case3().Where(x => x == Colors.Red).Do(2)
                              .Case3().Of(Colors.Green).Do(1).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT4_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>()
                              .Case1().Do(0).Case4().Of(Animals.Sheep).Do(2)
                              .Case3().Do(1).Case2().Do(3).Case4().Do(4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Do(1).Case3().Do(2)
                              .Case4().Where(x => x == Animals.Sheep).Do(3)
                              .Case4().Of(Animals.Dog).Do(4).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1AndNoCase1Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Throws<NoMatchException>(() => union.Match<bool>()
                                                .Case2().Do(x => true)
                                                .Case3().Do(x => true)
                                                .Case4().Do(false)
                                                .Result());
        }

        [Test]
        public void UnionWithT2AndNoCase2Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>("la la");
            Throws<NoMatchException>(() => union.Match<bool>()
                                                .Case1().Do(x => false)
                                                .Case3().Do(x => false)
                                                .Case4().Do(false)
                                                .Result());
        }

        [Test]
        public void UnionWithT3AndNoCase3Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            Throws<NoMatchException>(() => union.Match<bool>()
                                                .Case1().Do(x => false)
                                                .Case2().Do(x => false)
                                                .Case4().Do(false)
                                                .Result());
        }

        [Test]
        public void UnionWithT4AndNoCase4Match_ThrowsException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            Throws<NoMatchException>(() => union.Match<bool>()
                                                .Case1().Do(false)
                                                .Case2().Do(false)
                                                .Case3().Do(false)
                                                .Result());
        }
    }
}