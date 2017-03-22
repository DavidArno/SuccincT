using NUnit.Framework;
using SuccincT.Functional;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public sealed class TailApplicationTests
    {
        [Test]
        public void TwoParamFunctionIsTailComposable()
        {
            var testFunction = TypedLambdas.Lambda<int>((p1, p2) => p1 + p2);
            var expected = testFunction(2, 3);
            var composedFunction = testFunction.TailApply(3);
            var actual = composedFunction(2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsTailComposable()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3) => (p1 + p2) * p3);
            var expected = testFunction(2, 3, 4);
            var composedFunction = testFunction.TailApply(4);
            var actual = composedFunction(2, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsTailComposable()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4) => (p1 + p2) * (p3 + p4));
            var expected = testFunction(1, 2, 3, 4);
            var composedFunction = testFunction.TailApply(4);
            var actual = composedFunction(1, 2, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsTailComposable()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5);
            var expected = testFunction(1, 2, 3, 4, 5);
            var composedFunction = testFunction.TailApply(5);
            var actual = composedFunction(1, 2, 3, 4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsTailComposable()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var composedFunction = testFunction.TailApply(6);
            var actual = composedFunction(1, 2, 3, 4, 5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SevenParamFunctionIsTailComposable()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6, int p7) => (p1 + p2)*(p3 + p4)*(p5 + p6)*p7);
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7);
            var composedFunction = testFunction.TailApply(7);
            var actual = composedFunction(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EightParamFunctionIsTailComposable()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8) =>
                           (p1 + p2)*(p3 + p4)*(p5 + p6)*(p7 + p8));
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8);
            var composedFunction = testFunction.TailApply(8);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NineParamFunctionIsTailComposable()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9) =>
                           (p1 + p2)*(p3 + p4)*(p5 + p6)*(p7 + p8)*p9);
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var composedFunction = testFunction.TailApply(9);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TenParamFunctionIsTailComposable()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9, int p10) =>
                           (p1 + p2)*(p3 + p4)*(p5 + p6)*(p7 + p8)*(p9 + p10));
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            var composedFunction = testFunction.TailApply(10);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(expected, actual);
        }
    }
}