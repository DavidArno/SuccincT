using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public class UnionT1T2T3ComplexMatchersTests
    {
        private enum Colors { Red, Yellow, Green, Blue }

        [Test]
        public void UnionWithT1_MatchesComplexCase1Correctly()
        {
            var union = new Union<int, string, Colors>(3);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case3().Do(x => 2)
                              .Case1().Do(x => x)
                              .Case2().Do(x => 4).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_MatchesComplexCase2Correctly()
        {
            var union = new Union<int, string, Colors>("t");
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case2().Do(x => 5).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_MatchesComplexCase3Correctly()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case2().Do(x => 5).Result();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT1_MatchesOfOrStyleCase1Correctly()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>()
                              .Case3().Do(x => 1)
                              .Case1().Of(1).Or(2).Do(x => 2)
                              .Case1().Do(x => 3)
                              .Case2().Do(x => 4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_MatchesOfOrStyleCase2Correctly()
        {
            var union = new Union<int, string, Colors>("x");
            var result = union.Match<int>()
                              .Case3().Do(x => 1)
                              .Case2().Of("y").Or("x").Do(x => 2)
                              .Case2().Do(x => 3)
                              .Case1().Do(x => 4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_MatchesOfOrStyleCase3Correctly()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            var result = union.Match<int>()
                              .Case2().Do(x => 1)
                              .Case3().Of(Colors.Red).Or(Colors.Blue).Do(x => 2)
                              .Case3().Do(x => 3)
                              .Case1().Do(x => 4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1_HandlesMultipleOfOrStyleCase1MatchesCorrectly()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>()
                              .Case1().Of(1).Or(0).Do(x => 1)
                              .Case2().Do(x => 2)
                              .Case3().Do(x => 3)
                              .Case1().Of(3).Or(2).Do(x => 4)
                              .Case1().Do(x => 5).Result();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT2_HandlesMultipleOfOrStyleCase2MatchesCorrectly()
        {
            var union = new Union<int, string, Colors>("c");
            var result = union.Match<int>()
                              .Case2().Of("a").Or("b").Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("c").Or("d").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case1().Do(x => 5).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_HandlesMultipleOfOrStyleCase3MatchesCorrectly()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            var result = union.Match<int>()
                              .Case3().Of(Colors.Yellow).Or(Colors.Blue).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case3().Of(Colors.Green).Or(Colors.Red).Do(x => 3)
                              .Case2().Do(x => 4)
                              .Case1().Do(x => 5).Result();
            Assert.AreEqual(3, result);
        }
    }
}