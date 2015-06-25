using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4ComplexMatchersExecTests
    {
        private enum Colors { Red, Yellow, Green, Blue }

        private enum Animals { Cat, Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_MatchesComplexCase1CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case1().Of(1).Do(x => result = 1)
                 .Case3().Do(x => result = 2)
                 .Case1().Of(2).Do(x => result = 3)
                 .Case1().Do(x => result = 4)
                 .Case4().Do(x => result = 6)
                 .Case2().Do(x => result = 5).Exec();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_MatchesComplexCase2CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>("t");
            var result = 0;
            union.Match().Case1().Of(1).Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case3().Do(x => result = 3)
                 .Case2().Of("t").Do(x => result = 4)
                 .Case4().Do(x => result = 6)
                 .Case2().Do(x => result = 5).Exec();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT3_MatchesComplexCase3CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = 0;
            union.Match().Case3().Of(Colors.Red).Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case3().Do(x => result = 3)
                 .Case2().Of("t").Do(x => result = 4)
                 .Case4().Do(x => result = 6)
                 .Case2().Do(x => result = 5).Exec();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT4_MatchesComplexCase4CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            var result = 0;
            union.Match().Case4().Of(Animals.Cow).Do(x => result = 1)
                 .Case1().Do(x => result = 2)
                 .Case4().Do(x => result = 3)
                 .Case2().Of("t").Do(x => result = 4)
                 .Case3().Do(x => result = 6)
                 .Case2().Do(x => result = 5).Exec();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1_MatchesOfOrStyleCase1CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case3().Do(x => result = 1)
                         .Case1().Of(1).Or(2).Do(x => result = 2)
                         .Case1().Do(x => result = 3)
                         .Case4().Do(x => result = 5)
                         .Case2().Do(x => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_MatchesOfOrStyleCase2CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>("x");
            var result = 0;
            union.Match().Case3().Do(x => result = 1)
                         .Case2().Of("y").Or("x").Do(x => result = 2)
                         .Case4().Do(x => result = 5)
                         .Case2().Do(x => result = 3)
                         .Case1().Do(x => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_MatchesOfOrStyleCase3CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = 0;
            union.Match().Case2().Do(x => result = 1)
                         .Case3().Of(Colors.Green).Or(Colors.Blue).Do(x => result = 2)
                         .Case3().Do(x => result = 3)
                         .Case4().Do(x => result = 5)
                         .Case1().Do(x => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_MatchesOfOrStyleCase4CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            var result = 0;
            union.Match().Case2().Do(x => result = 1)
                         .Case4().Of(Animals.Cow).Or(Animals.Dog).Do(x => result = 2)
                         .Case4().Do(x => result = 3)
                         .Case3().Do(x => result = 5)
                         .Case1().Do(x => result = 4).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1_HandlesMultipleOfOrStyleCase1MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case1().Of(1).Or(0).Do(x => result = 1)
                         .Case2().Do(x => result = 2)
                         .Case1().Of(3).Or(2).Do(x => result = 3)
                         .Case3().Do(x => result = 4)
                         .Case4().Do(x => result = 6)
                         .Case1().Do(x => result = 5).Exec();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2_HandlesMultipleOfOrStyleCase2MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>("c");
            var result = 0;
            union.Match().Case2().Of("a").Or("b").Do(x => result = 1)
                         .Case1().Do(x => result = 2)
                         .Case3().Do(x => result = 3)
                         .Case4().Do(x => result = 6)
                         .Case2().Of("c").Or("d").Do(x => result = 4)
                         .Case1().Do(x => result = 5).Exec();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT3_HandlesMultipleOfOrStyleCase3MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = 0;
            union.Match().Case3().Of(Colors.Yellow).Or(Colors.Blue).Do(x => result = 1)
                         .Case1().Do(x => result = 2)
                         .Case2().Do(x => result = 3)
                         .Case4().Do(x => result = 6)
                         .Case3().Of(Colors.Green).Or(Colors.Red).Do(x => result = 4)
                         .Case1().Do(x => result = 5).Exec();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT4_HandlesMultipleOfOrStyleCase4MatchesCorrectlyWithExec()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = 0;
            union.Match().Case4().Of(Animals.Cat).Or(Animals.Cow).Do(x => result = 1)
                         .Case1().Do(x => result = 2)
                         .Case2().Do(x => result = 3)
                         .Case3().Do(x => result = 6)
                         .Case4().Of(Animals.Sheep).Or(Animals.Dog).Do(x => result = 4)
                         .Case1().Do(x => result = 5).Exec();
            Assert.AreEqual(4, result);
        }
    }
}