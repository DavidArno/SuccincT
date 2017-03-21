using NUnit.Framework;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class TypedLambdasFuncTests
    {
        [Test]
        public void OneParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int x) => x * 2);
            var result = func(1);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void TwoParamFunc_CanBeTypedAndRun()
        {
            var func = Func((double x, int y) => x + y);
            var result = func(1.5, 2);
            Assert.AreEqual(3.5, result);
        }

        [Test]
        public void ThreeParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int x, int y, double z) => $"{x * y + z}");
            var result = func(1, 2, 3);
            Assert.AreEqual("5", result);
        }

        [Test]
        public void FourParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int w, double x, int y, double z) => $"{w * x + y * z}");
            var result = func(1, 2, 3, 4);
            Assert.AreEqual("14", result);
        }

        [Test]
        public void FiveParamFunc_CanBeTypedAndRun()
        {
            var func = Func((double v, int w, double x, int y, double z) => $"{v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5);
            Assert.AreEqual("15", result);
        }

        [Test]
        public void SixParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int u, double v, int w, double x, int y, double z) => $"{u + v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5, 6);
            Assert.AreEqual("21", result);
        }

        [Test]
        public void SevenParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int t, double u, double v, int w, double x, int y, double z) =>
                                $"{t + u + v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("28", result);
        }

        [Test]
        public void EightParamFunc_CanBeTypedAndRun()
        {
            var func = Func((double s, int t, double u, double v, int w, double x, int y, double z) =>
                                $"{s + t + u + v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual("36", result);
        }

        [Test]
        public void NineParamFunc_CanBeTypedAndRun()
        {
            var func = Func((int r, double s, int t, double u, double v, int w, double x, int y, double z) =>
                                $"{r + s + t + u + v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual("45", result);
        }

        [Test]
        public void TenParamFunc_CanBeTypedAndRun()
        {
            var func =
                Func((double q, int r, double s, int t, double u, double v, int w, double x, int y, double z) =>
                         $"{q + r + s + t + u + v + w + x + y + z}");
            var result = func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual("55", result);
        }
    }
}
