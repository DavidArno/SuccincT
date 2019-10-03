using NUnit.Framework;
using SuccincT.Parsers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.Variant;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2ComplexMatchersTests
    {
        [Test]
        public void UnionWithT1_MatchesComplexCase1Correctly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Do(x => 3).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1_MatchesComplexCase1CorrectlyWithExec()
        {
            var union = new Union<int, string>(2);
            var result = 0;
            union.Match().Case1().Of(1).Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case2().Do(x => result = 3).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_MatchesComplexCase2Correctly()
        {
            var union = new Union<int, string>("t");
            var result = union.Match<int>()
                              .Case1().Of(1).Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("t").Do(x => 3)
                              .Case2().Do(x => 4).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_MatchesComplexCase2CorrectlyWithExec()
        {
            var union = new Union<int, string>("t");
            var result = 0;
            union.Match().Case1().Of(1).Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case2().Of("t").Do(x => result = 3)
                 .Case2().Do(x => result = 4).Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1_MatchesOfOrStyleCase1Correctly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>()
                              .Case1().Of(1).Or(2).Do(x => 1)
                              .Case1().Do(x => 3)
                              .Case2().Do(x => 4).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_MatchesOfOrStyleCase1CorrectlyWithExec()
        {
            var union = new Union<int, string>(2);
            var result = 0;
            union.Match().Case1().Of(1).Or(2).Do(x => result = 1)
                 .Case1().Do(x => result = 3)
                 .Case2().Do(x => result = 4).Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_MatchesOfOrStyleCase2Correctly()
        {
            var union = new Union<int, string>("x");
            var result = union.Match<int>()
                              .Case2().Of("y").Or("x").Do(x => 1)
                              .Case2().Do(x => 3)
                              .Case1().Do(x => 4).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_MatchesOfOrStyleCase2CorrectlyWithExec()
        {
            var union = new Union<int, string>("x");
            var result = 0;
            union.Match().Case2().Of("y").Or("x").Do(x => result = 1)
                 .Case2().Do(x => result = 3)
                 .Case1().Do(x => result = 4).Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_HandlesMultipleOfOrStyleCase1MatchesCorrectly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>()
                              .Case1().Of(1).Or(0).Do(x => 1)
                              .Case2().Do(x => 4)
                              .Case1().Of(3).Or(2).Do(x => 2)
                              .Case1().Do(x => 3).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1_HandlesMultipleOfOrStyleCase1MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string>(2);
            var result = 0;
            union.Match().Case1().Of(1).Or(0).Do(x => result = 1)
                 .Case2().Do(x => result = 4)
                 .Case1().Of(3).Or(2).Do(x => result = 2)
                 .Case1().Do(x => result = 3).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_HandlesMultipleOfOrStyleCase2MatchesCorrectly()
        {
            var union = new Union<int, string>("c");
            var result = union.Match<int>()
                              .Case2().Of("a").Or("b").Do(x => 1)
                              .Case1().Do(x => 2)
                              .Case2().Of("c").Or("d").Do(x => 3)
                              .Case1().Do(x => 4).Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_HandlesMultipleOfOrStyleCase2MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string>("c");
            var result = 0;
            union.Match().Case2().Of("a").Or("b").Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case2().Of("c").Or("d").Do(x => result = 3)
                 .Case1().Do(x => result = 4).Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string>(2);
            var result = union switch
            {
                (Case1, 2, _) => 1,
                (Case1, var x, _) => x,
                (_, _, _) => 3
            };

            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_CanBeUsedWithCS8PatternMatching()
        {
            var union = new Union<int, string>("2");
            var result = union switch
            {
                (Case1, 2, _) => 1,
                (Case1, var x, _) => x,
                (Case2, _, var x) when x == "2" => x.TryParseInt().Value, 
                (_, _, _) => 3
            };

            AreEqual(2, result);
        }
    }
}