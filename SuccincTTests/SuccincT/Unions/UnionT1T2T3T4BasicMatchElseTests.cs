using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4BasicMatchElseTests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_UsesElseIfNoCase1Match()
        {
            var union = new Union<int, string, Colors, Animals>(5);
            var result = union.Match<bool>()
                              .Case2().Do(x => false).Case3().Do(false).Case4().Do(false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2Match()
        {
            var union = new Union<int, string, Colors, Animals>("fred");
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case3().Do(x => false).Case4().Do(false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT3_UsesElseIfNoCase3Match()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case2().Do(x => false).Case4().Do(false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT4_UsesElseIfNoCase4Match()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case2().Do(x => false).Case3().Do(false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT1_UsesElseExpressionIfNoCase1Match()
        {
            var union = new Union<int, string, Colors, Animals>(5);
            var result = union.Match<bool>()
                              .Case2().Do(x => false).Case3().Do(x => false).Case4().Do(false).Else(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_UsesElseExpressionIfNoCase2Match()
        {
            var union = new Union<int, string, Colors, Animals>("fred");
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case3().Do(x => false).Case4().Do(false).Else(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT3_UsesElseExpressionIfNoCase3Match()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case2().Do(x => false).Case4().Do(false).Else(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT4_UsesElseExpressionIfNoCase3Match()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            var result = union.Match<bool>()
                              .Case1().Do(x => false).Case2().Do(x => false).Case3().Do(false).Else(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT1_UsesCase1MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>().Case1().Do(x => x).Else(x => 1).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesCase2MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>("x");
            var result = union.Match<int>().Case2().Do(x => 1).Else(x => 2).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT3_UsesCase3MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = union.Match<int>().Case3().Do(x => 1).Else(x => 3).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT4_UsesCase4MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>().Case4().Do(x => 1).Else(x => 3).Result();
            Assert.AreEqual(1, result);
        }
    }
}