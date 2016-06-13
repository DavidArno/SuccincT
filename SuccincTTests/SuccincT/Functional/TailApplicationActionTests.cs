using System;
using NUnit.Framework;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public sealed class TailApplicationActionTests
    {
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

        private int _twoParamMethodWithOptionalBoolResult;
        private void TwoParamMethodWithOptionalBool(int p1, bool p2 = false) =>
            _twoParamMethodWithOptionalBoolResult = p1 * (p2 ? 1 : 0);

        [Test]
        public void TwoParamMethodWithOptionalBool_IsTailComposable()
        {
            _twoParamMethodWithOptionalBoolResult = -1;
            var testAction = Action<int, bool>(TwoParamMethodWithOptionalBool);
            testAction(2, true);
            var expected = _twoParamMethodWithOptionalBoolResult;
            _twoParamMethodWithOptionalBoolResult = -1;

            var appliedAction = testAction.TailApply(true);
            appliedAction(2);
            Assert.AreEqual(expected, _twoParamMethodWithOptionalBoolResult);
        }

        private int _threeParamMethodWithOptionalBoolResult;
        private void ThreeParamMethodWithOptionalBool(int p1, int p2, bool p3 = false) =>
            _threeParamMethodWithOptionalBoolResult = (p1 + p2) * (p3 ? 1 : 0);

        [Test]
        public void ThreeParamActionWithOptionalBool_IsTailComposable()
        {
            _threeParamMethodWithOptionalBoolResult = -1;
            var testAction = Action<int, int, bool>(ThreeParamMethodWithOptionalBool);
            testAction(1, 2, true);
            var expected = _threeParamMethodWithOptionalBoolResult;
            _threeParamMethodWithOptionalBoolResult = -1;

            var appliedAction = testAction.TailApply(true).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, _threeParamMethodWithOptionalBoolResult);
        }

        private int _fourParamMethodWithOptionalBoolResult;
        private void FourParamMethodWithOptionalBool(int p1, int p2, int p3, bool p4 = false) =>
            _threeParamMethodWithOptionalBoolResult = (p1 + p2 + p3) * (p4 ? 1 : 0);

        [Test]
        public void FourParamActionWithOptionalBool_IsTailComposable()
        {
            _fourParamMethodWithOptionalBoolResult = -1;
            var testAction = Action<int, int, int, bool>(FourParamMethodWithOptionalBool);
            testAction(1, 2, 3, true);
            var expected = _fourParamMethodWithOptionalBoolResult;
            _fourParamMethodWithOptionalBoolResult = -1;

            var appliedAction = testAction.TailApply(true).TailApply(3).TailApply(2);
            appliedAction(1);
            Assert.AreEqual(expected, _fourParamMethodWithOptionalBoolResult);
        }
    }
}