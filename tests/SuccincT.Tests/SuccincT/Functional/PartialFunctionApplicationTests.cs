﻿using System;
using NUnit.Framework;
using SuccincT.Functional;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public sealed class PartialFunctionApplicationTests
    {
        [Test]
        public void TwoParamFunctionIsComposable()
        {
            var testFunction = TypedLambdas.Lambda<int>((p1, p2) => p1 + p2);
            var expected = testFunction(2, 3);
            var composedFunction = testFunction.Apply(2);
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithOneParam()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3) => (p1 + p2) * p3);
            var expected = testFunction(1, 2, 3);
            var partiallyComposedFunction = testFunction.Apply(1);
            var completelyComposedFunction = partiallyComposedFunction.Apply(2);
            var actual = completelyComposedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithTwoParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3) => (p1 + p2) * p3);
            var expected = testFunction(1, 2, 3);
            var completelyComposedFunction = testFunction.Apply(1, 2);
            var actual = completelyComposedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithOneParam()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4) => (p1 + p2) * (p3 + p4));
            var expected = testFunction(1, 2, 3, 4);
            var partiallyComposedFunction1 = testFunction.Apply(1);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(2);
            var completelyComposedFunction = partiallyComposedFunction2.Apply(3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithTwoParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4) => (p1 + p2) * (p3 + p4));
            var expected = testFunction(1, 2, 3, 4);
            var partiallyComposedFunction = testFunction.Apply(1, 2);
            var completelyComposedFunction = partiallyComposedFunction.Apply(3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FourParamFunctionIsComposableWithThreeParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4) => (p1 + p2) * (p3 + p4));
            var expected = testFunction(1, 2, 3, 4);
            var completelyComposedFunction = testFunction.Apply(1, 2, 3);
            var actual = completelyComposedFunction(4);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithOneParam()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5);
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction1 = testFunction.Apply(1);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(2);
            var partiallyComposedFunction3 = partiallyComposedFunction2.Apply(3);
            var completelyComposedFunction = partiallyComposedFunction3.Apply(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithTwoParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5);
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction1 = testFunction.Apply(1, 2);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(3);
            var completelyComposedFunction = partiallyComposedFunction2.Apply(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithThreeParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5);
            var expected = testFunction(1, 2, 3, 4, 5);
            var partiallyComposedFunction = testFunction.Apply(1, 2, 3);
            var completelyComposedFunction = partiallyComposedFunction.Apply(4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamFunctionIsComposableWithFourParams()
        {
            var testFunction = TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5);
            var expected = testFunction(1, 2, 3, 4, 5);
            var completelyComposedFunction = testFunction.Apply(1, 2, 3, 4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FiveParamMethodIsComposableWithFourParams()
        {
            var expected = TestMethod(1, 2, 3, 4, 5);
            Func<int, int, int, int, int, int> testFunction = TestMethod;
            var completelyComposedFunction = testFunction.Apply(1, 2, 3, 4);
            var actual = completelyComposedFunction(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsComposableWithOneParam()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var partiallyComposedFunction1 = testFunction.Apply(1);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(2);
            var partiallyComposedFunction3 = partiallyComposedFunction2.Apply(3);
            var partiallyComposedFunction4 = partiallyComposedFunction3.Apply(4);
            var completelyComposedFunction = partiallyComposedFunction4.Apply(5);
            var actual = completelyComposedFunction(6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsComposableWithTwoParams()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var partiallyComposedFunction1 = testFunction.Apply(1, 2);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(3);
            var partiallyComposedFunction3 = partiallyComposedFunction2.Apply(4);
            var completelyComposedFunction = partiallyComposedFunction3.Apply(5);
            var actual = completelyComposedFunction(6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsComposableWithThreeParams()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var partiallyComposedFunction1 = testFunction.Apply(1, 2, 3);
            var partiallyComposedFunction2 = partiallyComposedFunction1.Apply(4);
            var completelyComposedFunction = partiallyComposedFunction2.Apply(5);
            var actual = completelyComposedFunction(6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsComposableWithFourParams()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var partiallyComposedFunction = testFunction.Apply(1, 2, 3, 4);
            var completelyComposedFunction = partiallyComposedFunction.Apply(5);
            var actual = completelyComposedFunction(6);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SixParamFunctionIsComposableWithFiveParams()
        {
            var testFunction =
                TypedLambdas.Lambda((int p1, int p2, int p3, int p4, int p5, int p6) => (p1 + p2) * (p3 + p4) * (p5 + p6));
            var expected = testFunction(1, 2, 3, 4, 5, 6);
            var completelyComposedFunction = testFunction.Apply(1, 2, 3, 4, 5);
            var actual = completelyComposedFunction(6);
            Assert.AreEqual(expected, actual);
        }

        private static int TestMethod(int p1, int p2, int p3, int p4, int p5) => (p1 + p2) * (p3 + p4) + p5;
    }
}