using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4BasicMatchElseExecTests
    {
        private enum Colors { Green, Blue }

        private enum Animals { Dog, Sheep }

        [Test]
        public void UnionWithT1_UsesElseIfNoCase1Match()
        {
            var union = new Union<int, string, Colors, Animals>(5);
            var result = false;
            union.Match().Case2().Do(_ => result = false).Case3().Do(_ => result = false)
                         .Case4().Do(_ => result = false).Else(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2Match()
        {
            var union = new Union<int, string, Colors, Animals>("fred");
            var result = false;
            union.Match().Case1().Do(_ => result = false).Case3().Do(_ => result = false)
                         .Case4().Do(_ => result = false).Else(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT3_UsesElseIfNoCase3Match()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = false;
            union.Match().Case1().Do(_ => result = false).Case2().Do(_ => result = false)
                         .Case4().Do(_ => result = false).Else(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT4_UsesElseIfNoCase4Match()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            var result = false;
            union.Match().Case1().Do(_ => result = false).Case2().Do(_ => result = false)
                         .Case3().Do(_ => result = false).Else(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT1_UsesCase1MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = 0;
            union.Match().Case1().Do(x => result = x).Else(_ => result = 1).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesCase2MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>("x");
            var result = 0;
            union.Match().Case2().Do(_ => result = 1).Else(_ => result = 2).Exec();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT3_UsesCase3MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = 0;
            union.Match().Case3().Do(_ => result = 1).Else(_ => result = 3).Exec();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT4_UsesCase4MatchOverElse()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = 0;
            union.Match().Case4().Do(_ => result = 1).Else(_ => result = 3).Exec();
            Assert.AreEqual(1, result);
        }
    }
}