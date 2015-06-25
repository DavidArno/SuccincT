using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    internal class UnionCreatorTests
    {
        private enum Reds { Red }

        private enum Greens { Green }

        [Test]
        public void UnionCreatorT1T2_CanCreateT1AndT2Unions()
        {
            var creator = Union<int, string>.Creator();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            Assert.AreEqual(1, t1Value.Case1);
            Assert.AreEqual("2", t2Value.Case2);
        }

        [Test]
        public void UnionCreatorT1T2T3_CanCreateT1T2AndT3Unions()
        {
            var creator = Union<int, string, Reds>.Creator();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            var t3Value = creator.Create(Reds.Red);
            Assert.AreEqual(1, t1Value.Case1);
            Assert.AreEqual("2", t2Value.Case2);
            Assert.AreEqual(Reds.Red, t3Value.Case3);
        }

        [Test]
        public void UnionCreatorT1T2T3T4_CanCreateT1T2T3AndT4Unions()
        {
            var creator = Union<int, string, Reds, Greens>.Creator();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            var t3Value = creator.Create(Reds.Red);
            var t4Value = creator.Create(Greens.Green);
            Assert.AreEqual(1, t1Value.Case1);
            Assert.AreEqual("2", t2Value.Case2);
            Assert.AreEqual(Reds.Red, t3Value.Case3);
            Assert.AreEqual(Greens.Green, t4Value.Case4);
        }
    }
}