using System;
using NUnit.Framework;
using SuccincT.FunctionalComposition;

namespace SuccincTTests.FunctionalComposition
{
    [TestFixture]
    public class FunctionalTests
    {
        [Test]
        public void TwoParamFunctionIsComposable()
        {
            Func<int, int, int> testFunction = (p1, p2) => p1 + p2;
            var expected = testFunction(2, 3);
            var composedFunction = Functional.Compose(testFunction, 2);
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposable()
        {
            Func<int, int, int, int> testFunction = (p1, p2, p3) => (p1 + p2) * p3;
            var expected = testFunction(1, 2, 3);
            var partiallyComposedFunction = Functional.Compose(testFunction, 1);
            var completelyComposedFunction = Functional.Compose(partiallyComposedFunction, 2);
            var actual = completelyComposedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionCanBeComposedUsingTwoParams()
        {
            Func<int, int, int, int> testFunction = (p1, p2, p3) => (p1 + p2) * p3;
            var expected = testFunction(1, 2, 3);
            var composedFunction = Functional.Compose(testFunction, 1, 2);
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }
    }
}
