using NUnit.Framework;
using SuccincT.Unions;

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
            Assert.AreEqual(Variant.Case1, union.Case);
        }

        [Test]
        public void UnionWithT2_HasVariantCase2()
        {
            var union = new Union<int, string, Colors, Animals>("test");
            Assert.AreEqual(Variant.Case2, union.Case);
        }

        [Test]
        public void UnionWithT3_HasVariantCase3()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            Assert.AreEqual(Variant.Case3, union.Case);
        }

        [Test]
        public void UnionWithT4_HasVariantCase4()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cow);
            Assert.AreEqual(Variant.Case4, union.Case);
        }

        [Test]
        public void UnionWithT1_HasCase1Value()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Assert.AreEqual(2, union.Case1);
        }

        [Test]
        public void UnionWithT2_HasCase2Value()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Assert.AreEqual("Test", union.Case2);
        }

        [Test]
        public void UnionWithT3_HasCase3Value()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            Assert.AreEqual(Colors.Green, union.Case3);
        }

        [Test]
        public void UnionWithT4_HasCase4Value()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Cat);
            Assert.AreEqual(Animals.Cat, union.Case4);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Assert.AreEqual("", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase3ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Assert.AreEqual(Colors.Red, union.Case3);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase4ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            Assert.AreEqual(Animals.Dog, union.Case4);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Assert.AreEqual(1, union.Case1);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase3ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Assert.AreEqual(Colors.Blue, union.Case3);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase4ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>("Test");
            Assert.AreEqual(Animals.Dog, union.Case4);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            Assert.AreEqual(1, union.Case1);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            Assert.AreEqual("", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase4ForUnionWithT3_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            Assert.AreEqual(Animals.Dog, union.Case4);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            Assert.AreEqual(1, union.Case1);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            Assert.AreEqual("", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase3ForUnionWithT4_CausesException()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Dog);
            Assert.AreEqual(Colors.Green, union.Case3);
        }

        [Test]
        public void UnionWithT1_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(2);
            var result = union.Match<int>()
                              .Case1().Of(2).Do(0).Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void UnionWithT1_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(3);
            var result = union.Match<int>()
                              .Case1().Of(2).Do(0).Case1().Where(x => x == 3).Do(1)
                              .Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("1");
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Of("1").Do(2).Case2().Do(1).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>("2");
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Do(3).Case4().Do(4)
                              .Case2().Where(x => x == "2").Do(2)
                              .Case2().Of("1").Do(1).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Blue);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Green);
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Of(Colors.Green).Do(2)
                              .Case3().Do(1).Case2().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Colors.Red);
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Do(3).Case4().Do(4)
                              .Case3().Where(x => x == Colors.Red).Do(2)
                              .Case3().Of(Colors.Green).Do(1).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void UnionWithT4_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>()
                              .Case1().Do(0).Case4().Of(Animals.Sheep).Do(2)
                              .Case3().Do(1).Case2().Do(3).Case4().Do(4).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT4_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors, Animals>(Animals.Sheep);
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Do(1).Case3().Do(2)
                              .Case4().Where(x => x == Animals.Sheep).Do(3)
                              .Case4().Of(Animals.Dog).Do(4).Result();
            Assert.AreEqual(3, result);
        }
    }
}