using NUnit.Framework;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class ActionExtensionsTests
    {
        [Test]
        public void ConvertingActionToToUnitFunc_AllowsActionToBeRun()
        {
            var value = 1;
            var func = Action(() => { value = 2; }).ToUnitFunc();
            var result = func();
            Assert.AreEqual(2, value);
            Assert.AreEqual(unit, result);
        }

        [Test]
        public void ConvertingActionT1ToToUnitFunc_AllowsActionToBeRun()
        {
            var value = 1;
            var func = Action((int x) => { value = x; }).ToUnitFunc();
            var result = func(3);
            Assert.AreEqual(3, value);
            Assert.AreEqual(unit, result);
        }

        [Test]
        public void ConvertingActionT1T2ToToUnitFunc_AllowsActionToBeRun()
        {
            var value = 1;
            var func = Lambda<int>((x, y) => { value = x + y; }).ToUnitFunc();
            var result = func(3, 2);
            Assert.AreEqual(5, value);
            Assert.AreEqual(unit, result);
        }

        [Test]
        public void ConvertingActionT1T2T3ToToUnitFunc_AllowsActionToBeRun()
        {
            var value = 1;
            var func = Lambda<int>((x, y, z) => { value = x * y + z; }).ToUnitFunc();
            var result = func(3, 2, 1);
            Assert.AreEqual(7, value);
            Assert.AreEqual(unit, result);
        }

        [Test]
        public void ConvertingActionT1T2T3T4ToToUnitFunc_AllowsActionToBeRun()
        {
            var value = 1;
            var func = Lambda<int>((w, x, y, z) => { value = (w + x) * (y + z); }).ToUnitFunc();
            var result = func(4, 3, 2, 1);
            Assert.AreEqual(21, value);
            Assert.AreEqual(unit, result);
        }
    }
}
