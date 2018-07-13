using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public sealed class UnionT1T2T3T4Tests
    {
        private enum Colors { Red, Green, Blue }

        private enum Animals { Cat, Dog, Cow, Sheep }

        [Test]
        public void UnionWithT1_HasVariantCase1()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            AreEqual(Variant.Case1, union.Case);
        }

        [Test]
        public void UnionWithT2_HasVariantCase2()
        {
            var union = new Union<int, string, Colors, Animals>("test");
            AreEqual(Variant.Case2, union.Case);
        }

        [Test]
        public void UnionWithT3_HasVariantCase3()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            AreEqual(Variant.Case3, union.Case);
        }

        [Test]
        public void UnionWithT4_HasVariantCase4()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            AreEqual(Variant.Case4, union.Case);
        }

        [Test]
        public void UnionWithT1_HasCase1Value()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            AreEqual(2, union.Case1);
        }

        [Test]
        public void UnionWithT2_HasCase2Value()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            AreEqual("Test", union.Case2);
        }

        [Test]
        public void UnionWithT3_HasCase3Value()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            AreEqual(Colors.Green, union.Case3);
        }

        [Test]
        public void UnionWithT4_HasCase4Value()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            AreEqual(Animals.Cat, union.Case4);
        }

        [Test]
        public void AccessingCase2ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Throws<InvalidCaseException>(() => Ignore(union.Case2));
        }

        [Test]
        public void AccessingCase2ForUnionWithT1_GivesMeaningfulException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            try
            {
                _ = union.Case2;
                Fail("Expected exception to be thrown");
            }
            catch (InvalidCaseException e)
            {
                AreEqual($"Cannot access union case {Variant.Case2} when case {Variant.Case1} is selected one.",
                    e.Message);
            }
        }

        [Test]
        public void AccessingCase3ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Throws<InvalidCaseException>(() => _ = union.Case3);
        }

        [Test]
        public void AccessingCase4ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Throws<InvalidCaseException>(() => _ = union.Case4);
        }

        [Test]
        public void AccessingCase1ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Throws<InvalidCaseException>(() => _ = union.Case1);
        }

        [Test]
        public void AccessingCase3ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Throws<InvalidCaseException>(() => _ = union.Case3);
        }

        [Test]
        public void AccessingCase4ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Throws<InvalidCaseException>(() => _ = union.Case4);
        }

        [Test]
        public void AccessingCase1ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            Throws<InvalidCaseException>(() => _ = union.Case1);
        }

        [Test]
        public void AccessingCase2ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            Throws<InvalidCaseException>(() => _ = union.Case2);
        }

        [Test]
        public void AccessingCase4ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            Throws<InvalidCaseException>(() => _ = union.Case4);
        }

        [Test]
        public void AccessingCase4ForUnionWithT3_GivesMeaningfulException()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            try
            {
                _ = union.Case4;
                Fail("Expected exception to be thrown");
            }
            catch (InvalidCaseException e)
            {
                AreEqual($"Cannot access union case {Variant.Case4} when case {Variant.Case3} is selected one.",
                    e.Message);
            }
        }

        [Test]
        public void AccessingCase1ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            Throws<InvalidCaseException>(() => _ = union.Case1);
        }

        [Test]
        public void AccessingCase2ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            Throws<InvalidCaseException>(() => Ignore(union.Case2));
        }

        [Test]
        public void AccessingCase3ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            Throws<InvalidCaseException>(() => _ = union.Case3);
        }
    }
}