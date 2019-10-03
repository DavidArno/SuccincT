using NUnit.Framework;
using SuccincT.Parsers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.Variant;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4ComplexMatchersTests
    {
        private enum Colors { Red, Yellow, Green, Blue }

        private enum Animals { Cat, Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_MatchesComplexCase1Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(3);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case3().Do(x => 2)
                              .Case4().Do(x => 5)
                              .Case1().Do(x => x)
                              .Case2().Do(x => 4).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_MatchesComplexCase2Correctly()
        {
            var union = new Union<int, string, Colors, Animals>("t");
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case4().Do(x => 6)
                              .Case2().Do(x => 5).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_MatchesComplexCase3Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case4().Do(x => 6)
                              .Case2().Do(x => 5).Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT4_MatchesComplexCase4Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case4().Do(6)
                              .Case2().Do(x => 5).Result();
            AreEqual(6, result);
        }

        [Test]
        public void UnionWithT1_MatchesOfOrStyleCase1Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>()
                              .Case3().Do(x => 1)
                              .Case1().Of(1).Or(2).Do(x => 2)
                              .Case1().Do(x => 3)
                              .Case4().Do(5)
                              .Case2().Do(x => 4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_MatchesOfOrStyleCase2Correctly()
        {
            var union = new Union<int, string, Colors, Animals>("x");
            var result = union.Match<int>()
                              .Case3().Do(x => 1)
                              .Case2().Of("y").Or("x").Do(x => 2)
                              .Case2().Do(x => 3)
                              .Case4().Do(5)
                              .Case1().Do(x => 4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_MatchesOfOrStyleCase3Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = union.Match<int>()
                              .Case2().Do(x => 1)
                              .Case3().Of(Colors.Red).Or(Colors.Blue).Do(x => 2)
                              .Case3().Do(x => 3)
                              .Case4().Do(5)
                              .Case1().Do(x => 4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_MatchesOfOrStyleCase4Correctly()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            var result = union.Match<int>()
                              .Case2().Do(x => 1)
                              .Case4().Of(Animals.Cat).Or(Animals.Cow).Do(x => 2)
                              .Case4().Do(3)
                              .Case3().Do(5)
                              .Case1().Do(x => 4).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1_HandlesMultipleOfOrStyleCase1MatchesCorrectly()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>()
                              .Case1().Of(1).Or(0).Do(x => 1)
                              .Case2().Do(x => 2)
                              .Case3().Do(x => 3)
                              .Case4().Do(6)
                              .Case1().Of(3).Or(2).Do(x => 4)
                              .Case1().Do(x => 5).Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT2_HandlesMultipleOfOrStyleCase2MatchesCorrectly()
        {
            var union = new Union<int, string, Colors, Animals>("c");
            var result = union.Match<int>()
                              .Case2().Of("a").Or("b").Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("c").Or("d").Do(x => 3)
                              .Case3().Do(x => 4)
                              .Case4().Do(6)
                              .Case1().Do(x => 5).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_HandlesMultipleOfOrStyleCase3MatchesCorrectly()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = union.Match<int>()
                              .Case3().Of(Colors.Yellow).Or(Colors.Blue).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case3().Of(Colors.Green).Or(Colors.Red).Do(x => 3)
                              .Case2().Do(x => 4)
                              .Case4().Do(6)
                              .Case1().Do(x => 5).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT4_HandlesMultipleOfOrStyleCase4MatchesCorrectly()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>()
                              .Case4().Of(Animals.Cow).Or(Animals.Dog).Do(1)
                              .Case1().Do(x => 2)
                              .Case4().Of(Animals.Cat).Or(Animals.Sheep).Do(3)
                              .Case2().Do(x => 4)
                              .Case3().Do(6)
                              .Case1().Do(x => 5).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union switch {
                (Case1, 2, _, _, _) => 1,
                (Case1, var x, _, _, _) => x,
                (_, _, _, _, _) => 3
            };

            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = union switch {
                (Case1, 2, _, _, _) => 1,
                (Case1, var x, _, _, _) => x,
                (Case2, _, var x, _, _) when x == "2" => x.TryParseInt().Value,
                (_, _, _, _, _) => 3
            };

            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Yellow);
            var result = union switch
            {
                (Case1, _, _, _, _) => 1,
                (Case2, _, _, _, _) => 2,
                (Case3, _, _, Colors.Yellow, _) => 3,
                _ => 4
            };

            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT4_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            var result = union switch
            {
                (Case1, _, _, _, _) => 1,
                (Case2, _, _, _, _) => 2,
                (Case3, _, _, _, _) => 3,
                (Case4, _, _, _, Animals.Dog) => 4,
                (_, _, _, _, _) => 5
            };

            AreEqual(4, result);
        }
    }
}