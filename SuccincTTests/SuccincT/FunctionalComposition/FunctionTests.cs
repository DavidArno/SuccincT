using System;
using NUnit.Framework;
using SuccincT.FunctionalComposition;

namespace SuccincTTests.SuccincT.FunctionalComposition
{
    [TestFixture]
    public sealed class FunctionTests
    {
        [Test]
        public void TwoParamFunctionIsComposable()
        {
            Func<int, int, int> testFunction = (p1, p2) => p1 + p2;
            var expected = testFunction(2, 3);
            var composedFunction = testFunction.Compose(2);
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithOneParam()
        {
            Func<int, int, int, int> testFunction = (p1, p2, p3) => (p1 + p2) * p3;
            var expected = testFunction(1, 2, 3);
            var partiallyComposedFunction = testFunction.Compose(1);
            var completelyComposedFunction = partiallyComposedFunction.Compose(2);
            var actual = completelyComposedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithTwoParams()
        {
            Func<int, int, int, int> testFunction = (p1, p2, p3) => (p1 + p2) * p3;
            var expected = testFunction(1, 2, 3);
            var completelyComposedFunction = testFunction.Compose(1, 2);
            var actual = completelyComposedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithOneParam()
        {
            Func<int, int, int, int, int> testFunction = (p1, p2, p3, p4) => (p1 + p2) * (p3 + p4);
            var expected = testFunction(1, 2, 3, 4);
            var partiallyComposedFunction1 = testFunction.Compose(1);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Compose(2);
            var completelyComposedFunction = partiallyComposedFunction2.Compose(3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithTwoParams()
        {
            Func<int, int, int, int, int> testFunction = (p1, p2, p3, p4) => (p1 + p2) * (p3 + p4);
            var expected = testFunction(1, 2, 3, 4);
            var partiallyComposedFunction = testFunction.Compose(1, 2);
            var completelyComposedFunction = partiallyComposedFunction.Compose(3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithThreeParams()
        {
            Func<int, int, int, int, int> testFunction = (p1, p2, p3, p4) => (p1 + p2) * (p3 + p4);
            var expected = testFunction(1, 2, 3, 4);
            var completelyComposedFunction = testFunction.Compose(1, 2, 3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithOneParam()
        {
            Func<int, int, int, int, int, int> testFunction = (p1, p2, p3, p4, p5) => (p1 + p2) * (p3 + p4) + p5;
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction1 = testFunction.Compose(1);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Compose(2);
            var partiallyComposedFunction3 = partiallyComposedFunction2.Compose(3);
            var completelyComposedFunction = partiallyComposedFunction3.Compose(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithTwoParams()
        {
            Func<int, int, int, int, int, int> testFunction = (p1, p2, p3, p4, p5) => (p1 + p2) * (p3 + p4) + p5;
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction1 = testFunction.Compose(1, 2);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Compose(3);
            var completelyComposedFunction = partiallyComposedFunction2.Compose(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithThreeParams()
        {
            Func<int, int, int, int, int, int> testFunction = (p1, p2, p3, p4, p5) => (p1 + p2) * (p3 + p4) + p5;
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction = testFunction.Compose(1, 2, 3);
            var completelyComposedFunction = partiallyComposedFunction.Compose(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithFourParams()
        {
            Func<int, int, int, int, int, int> testFunction = (p1, p2, p3, p4, p5) => (p1 + p2) * (p3 + p4) + p5;
            var expected = testFunction(1, 2, 3, 4, 5);
            var completelyComposedFunction = testFunction.Compose(1, 2, 3, 4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }
    }
}