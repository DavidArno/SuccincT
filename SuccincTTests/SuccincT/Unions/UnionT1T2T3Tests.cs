using NUnit.Framework;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public sealed class UnionT1T2T3Tests
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void UnionWithT1_HasVariantCase1()
        {
            var union = new Union<int, string, Colors>(2);
            Assert.AreEqual(Variant.Case1, union.Case);
        }

        [Test]
        public void UnionWithT2_HasVariantCase2()
        {
            var union = new Union<int, string, Colors>("test");
            Assert.AreEqual(Variant.Case2, union.Case);
        }

        [Test]
        public void UnionWithT3_HasVariantCase3()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            Assert.AreEqual(Variant.Case3, union.Case);
        }

        [Test]
        public void UnionWithT1_HasCase1Value()
        {
            var union = new Union<int, string, Colors>(2);
            Assert.AreEqual(2, union.Case1);
        }

        [Test]
        public void UnionWithT2_HasCase2Value()
        {
            var union = new Union<int, string, Colors>("Test");
            Assert.AreEqual("Test", union.Case2);
        }

        [Test]
        public void UnionWithT3_HasCase3Value()
        {
            var union = new Union<int, string, Colors>(Colors.Green);
            Assert.AreEqual(Colors.Green, union.Case3);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors>(2);
            Assert.AreEqual("", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase3ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string, Colors>(2);
            Assert.AreEqual(Colors.Red, union.Case3);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors>("Test");
            Assert.AreEqual(1, union.Case1);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase3ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string, Colors>("Test");
            Assert.AreEqual(Colors.Blue, union.Case3);
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

        [Test]
        public void UnionWithT1_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>().Case1().Of(2).Do(0).Case1().Do(1).Case2().Do(2).Case3().Do(3).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void UnionWithT1_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors>(3);
            var result = union.Match<int>()
                              .Case1().Of(2).Do(0).Case1().Where(x => x == 3).Do(1).Case2().Do(2).Case3().Do(3).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors>("2");
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors>("1");
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Of("1").Do(2).Case2().Do(1).Case3().Do(3).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors>("2");
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Do(3).Case2().Where(x => x == "2").Do(2)
                              .Case2().Of("1").Do(1).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Case3().Do(3).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void UnionWithT3_CaseOfExpressionSupported()
        {
            var union = new Union<int, string, Colors>(Colors.Green);
            var result = union.Match<int>()
                              .Case1().Do(0).Case3().Of(Colors.Green).Do(2).Case3().Do(1).Case2().Do(3).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Do(3)
                              .Case3().Where(x => x == Colors.Red).Do(2)
                              .Case3().Of(Colors.Green).Do(1).Result();
            Assert.AreEqual(2, result);
        }
    }
}