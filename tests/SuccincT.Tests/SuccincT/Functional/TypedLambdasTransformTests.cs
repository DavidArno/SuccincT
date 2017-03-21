using NUnit.Framework;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class TypedLambdasTransformTests
    {
        [Test]
        public void OneParamTransform_CanBeTypedAndRun()
        {
            var func = Transform((int x) => $"{x}");
            var result = func(1);
            Assert.AreEqual("1", result);
        }

        [Test]
        public void TwoParamTranform_CanBeTypedAndRun()
        {
            var func = Transform((int x, int y) => (x + y) * 0.5);
            var result = func(1, 2);
            Assert.AreEqual(1.5, result);
        }

        [Test]
        public void ThreeParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<int, string>((x, y, z) => $"{(x + y) * z}");
            var result = func(1, 2, 3);
            Assert.AreEqual("9", result);
        }

        [Test]
        public void FourParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<int, string>((w, x, y, z) => $"{w + x + y + z}");
            var result = func(1, 2, 3, 4);
            Assert.AreEqual("10", result);
        }

        [Test]
        public void FiveParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((v, w, x, y, z) => $"{v + w + x + y + z}");
            var result = func(1.1, 2.2, 3.3, 4, 5.5);
            Assert.AreEqual("16.1", result);
        }

        [Test]
        public void SixParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((u, v, w, x, y, z) => $"{u + v + w + x + y + z}");
            var result = func(1.1, 2.2, 3.3, 4, 5.5, 6);
            Assert.AreEqual("22.1", result);
        }

        [Test]
        public void SevenParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((t, u, v, w, x, y, z) => $"{t + u + v + w + x + y + z}");
            var result = func(1.1, 2.2, 3, 4, 5.5, 6, 7);
            Assert.AreEqual("28.8", result);
        }

        [Test]
        public void EightParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((s, t, u, v, w, x, y, z) => $"{s + t + u + v + w + x + y + z}");
            var result = func(1.1, 2.2, 3, 4, 5.5, 6, 7, 8);
            Assert.AreEqual("36.8", result);
        }

        [Test]
        public void NineParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((r, s, t, u, v, w, x, y, z) => $"{r + s + t + u + v + w + x + y + z}");
            var result = func(1.1, 2.2, 3, 4, 5, 6.6, 7, 8, 9);
            Assert.AreEqual("45.9", result);
        }

        [Test]
        public void TenParamTransform_CanBeTypedAndRun()
        {
            var func = Transform<double, string>((q, r, s, t, u, v, w, x, y, z) =>
                         $"{q + r + s + t + u + v + w + x + y + z}");
            var result = func(1.1, 2.2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual("55.3", result);
        }
    }
}
