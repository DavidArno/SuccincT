using NUnit.Framework;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.Union;

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
            var creator = UnionCreator<int, string>();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            AreEqual(1, t1Value.Case1);
            AreEqual("2", t2Value.Case2);
        }

        [Test]
        public void UnionCreatorT1T2T3_CanCreateT1T2AndT3Unions()
        {
            var creator = UnionCreator<int, string, Reds>();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            var t3Value = creator.Create(Reds.Red);
            AreEqual(1, t1Value.Case1);
            AreEqual("2", t2Value.Case2);
            AreEqual(Reds.Red, t3Value.Case3);
        }

        [Test]
        public void UnionCreatorT1T2T3T4_CanCreateT1T2T3AndT4Unions()
        {
            var creator = UnionCreator<int, string, Reds, Greens>();
            var t1Value = creator.Create(1);
            var t2Value = creator.Create("2");
            var t3Value = creator.Create(Reds.Red);
            var t4Value = creator.Create(Greens.Green);
            AreEqual(1, t1Value.Case1);
            AreEqual("2", t2Value.Case2);
            AreEqual(Reds.Red, t3Value.Case3);
            AreEqual(Greens.Green, t4Value.Case4);
        }
    }
}