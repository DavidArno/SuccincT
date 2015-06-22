using System;
using NUnit.Framework;
using SuccincT.FunctionalComposition;

namespace SuccincTTests.SuccincT.FunctionalComposition
{
    [TestFixture]
    public sealed class TailComposingTests
    {
        [Test]
        public void TwoParamFunctionIsTailComposable()
        {
            Func<int, int, int> testFunction = (p1, p2) => p1 + p2;
            var expected = testFunction(2, 3);
            var composedFunction = testFunction.TailCompose(3);
            var actual = composedFunction(2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsTailComposable()
        {
            Func<int, int, int, int> testFunction = (p1, p2, p3) => p1 + p2 + p3;
            var expected = testFunction(2, 3, 4);
            var composedFunction = testFunction.TailCompose(4);
            var actual = composedFunction(2, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int> testFunction = (p1, p2, p3, p4) => (p1 + p2) * (p3 + p4);
            var expected = testFunction(1, 2, 3, 4);
            var composedFunction = testFunction.TailCompose(4);
            var actual = composedFunction(1, 2, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int> testFunction = (p1, p2, p3, p4, p5) => (p1 + p2) * (p3 + p4) * p5;
            var expected = testFunction(1, 2, 3, 4, 5);
            var composedFunction = testFunction.TailCompose(5);
            var actual = composedFunction(1, 2, 3, 4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int, int> testFunction =
                (p1, p2, p3, p4, p5, p6) => (p1 + p2) * (p3 + p4) * (p5 + p6);
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var composedFunction = testFunction.TailCompose(6);
            var actual = composedFunction(1, 2, 3, 4, 5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SevenParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int, int, int> testFunction =
                (p1, p2, p3, p4, p5, p6, p7) => (p1 + p2) * (p3 + p4) * (p5 + p6) * p7;
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7);
            var composedFunction = testFunction.TailCompose(7);
            var actual = composedFunction(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EightParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int, int, int, int> testFunction =
                (p1, p2, p3, p4, p5, p6, p7, p8) => (p1 + p2) * (p3 + p4) * (p5 + p6) * (p7 + p8);
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8);
            var composedFunction = testFunction.TailCompose(8);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NineParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int, int, int, int, int> testFunction =
                (p1, p2, p3, p4, p5, p6, p7, p8, p9) => (p1 + p2) * (p3 + p4) * (p5 + p6) * (p7 + p8) * p9;
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var composedFunction = testFunction.TailCompose(9);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TenParamFunctionIsTailComposable()
        {
            Func<int, int, int, int, int, int, int, int, int, int, int> testFunction =
                (p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) => (p1 + p2) * (p3 + p4) * (p5 + p6) * (p7 + p8) * (p9 + p10);
            var expected = testFunction(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            var composedFunction = testFunction.TailCompose(10);
            var actual = composedFunction(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(expected, actual);
        }
    }
}