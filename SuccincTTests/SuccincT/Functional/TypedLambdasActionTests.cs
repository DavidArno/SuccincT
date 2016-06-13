using NUnit.Framework;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class TypedLambdasActionTests
    {
        [Test]
        public void NoParamAction_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Action(() => result = 1);
            func();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void OneParamAction_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Action((int x) => result = x * 2);
            func(1);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void TwoParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func = Action((double x, int y) => result = x + y);
            func(1.5, 2);
            Assert.AreEqual(3.5, result);
        }

        [Test]
        public void ThreeParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((double x, int y, double z) =>
                         result = x + y + z);
            func(3, 5, 8);
            Assert.AreEqual(16, result);
        }

        [Test]
        public void FourParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((int w, double x, int y, double z) =>
                         result = w + x + y + z);
            func(3, 5, 6, 8);
            Assert.AreEqual(22, result);
        }

        [Test]
        public void FiveParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((double v, int w, double x, int y, double z) =>
                         result = v + w + x + y + z);
            func(3, 4, 5, 6, 8);
            Assert.AreEqual(26, result);
        }

        [Test]
        public void SixParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((double u, double v, int w, double x, int y, double z) =>
                         result = u + v + w + x + y + z);
            func(1, 3, 4, 5, 6, 8);
            Assert.AreEqual(27, result);
        }

        [Test]
        public void SevenParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((int t, double u, double v, int w, double x, int y, double z) =>
                         result = t + u + v + w + x + y + z);
            func(1, 3, 4, 5, 6, 8, 9);
            Assert.AreEqual(36, result);
        }

        [Test]
        public void EightParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((double s, int t, double u, double v, int w, double x, int y, double z) =>
                         result = s + t + u + v + w + x + y + z);
            func(1, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(43, result);
        }

        [Test]
        public void NineParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((int r, double s, int t, double u, double v, int w, double x, int y, double z) =>
                         result = r + s + t + u + v + w + x + y + z);
            func(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(45, result);
        }

        [Test]
        public void TenParamAction_CanBeTypedAndRun()
        {
            double result = -1;
            var func =
                Action((double q, int r, double s, int t, double u, double v, int w, double x, int y, double z) =>
                         result = q + r + s + t + u + v + w + x + y + z);
            func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(55, result);
        }
    }
}
