using System;
using NUnit.Framework;
using SuccincT.Functional;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public sealed class TailApplicationActionTests
    {
        private int _testFunctionResult;

        [Test]
        public void TwoParamAction_IsTailComposable()
        {
            var result = 0;
            Action<int, int> testAction = (p1, p2) => result = p1 + p2;
            testAction(2, 3);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(3);
            appliedAction(2);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThreeParamAction_IsTailComposable()
        {
            var result = 0;
            Action<int, int, int> testAction = (p1, p2, p3) => result = (p1 + p2) * p3;
            testAction(1, 2, 3);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FourParamAction_IsTailComposable()
        {
            var result = 0;
            Action<int, int, int, int> testAction = (p1, p2, p3, p4) => result = (p1 + p2) * p3 * p4;
            testAction(1, 2, 3, 4);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(4).TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FiveParamAction_IsTailComposable()
        {
            var result = 0;
            Action<int, int, int, int, int> testAction = (p1, p2, p3, p4, p5) => result = (p1 + p2) * p3 * p4 + p5;
            testAction(1, 2, 3, 4, 5);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(5).TailApply(4).TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoParamActionWithOptionalBool_IsTailComposable()
        {
            var result = 0;
            ActionWithOptionalParameter<int, bool> testAction = (p1, p2) => result = p1 * (p2 ? 1 : 0);
            testAction(2, true);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(true);
            appliedAction(2);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThreeParamActionWithOptionalBool_IsTailComposable()
        {
            var result = 0;
            ActionWithOptionalParameter<int, int, bool> testAction = (p1, p2, p3) => result = (p1 + p2) * (p3 ? 1 : 0);
            testAction(1, 2, true);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(true).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FourParamActionWithOptionalBool_IsTailComposable()
        {
            var result = 0;
            ActionWithOptionalParameter<int, int, int, bool> testAction =
                (p1, p2, p3, p4) => result = (p1 + p2 + p3) * (p4 ? 1 : 0);
            testAction(1, 2, 3, true);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(true).TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FiveParamActionWithOptionalBool_IsTailComposable()
        {
            var result = 0;
            ActionWithOptionalParameter<int, int, int, int, bool> testAction =
                (p1, p2, p3, p4, p5) => result = (p1 + p2) * (p3 + p4) * (p5 ? 1 : 0);
            testAction(1, 2, 3, 4, true);
            var expected = result;
            result = 0;

            var appliedAction = testAction.TailApply(true).TailApply(4).TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoParamActionWithOptionalTrueUsingTestAction_IsTailComposable()
        {
            _testFunctionResult = 0;
            ActionWithOptionalParameter<int, bool> testAction = TestActionDefaultTrue;
            testAction(2, true);
            var expectedWhenTrue = _testFunctionResult;
            _testFunctionResult = 0;
            testAction(2);
            var expectedWhenFalse = _testFunctionResult;
            _testFunctionResult = 0;

            var appliedActionTrue = testAction.TailApply(true);
            appliedActionTrue(2);
            var actualWhenTrue = _testFunctionResult;
            var appliedActionFalse = testAction.TailApply(false);
            appliedActionFalse(2);
            var actualWhenFalse = _testFunctionResult;

            Assert.AreEqual(expectedWhenTrue, actualWhenTrue);
            Assert.AreEqual(expectedWhenFalse, actualWhenFalse);
        }

        private void TestActionDefaultTrue(int a, bool b = true) => _testFunctionResult = a * (b ? 1 : 0);
    }
}