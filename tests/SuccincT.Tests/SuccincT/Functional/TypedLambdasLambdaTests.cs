using NUnit.Framework;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class TypedLambdasLambdaTests
    {
        [Test]
        public void OneParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>(x => x * 2);
            var result = func(1);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void TwoParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((x, y) => x + y);
            var result = func(9, 10);
            Assert.AreEqual(19, result);
        }

        [Test]
        public void ThreeParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((x, y, z) => x + y + z);
            var result = func(8, 9, 10);
            Assert.AreEqual(27, result);
        }

        [Test]
        public void FourParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((w, x, y, z) => w + x + y + z);
            var result = func(7, 8, 9, 10);
            Assert.AreEqual(34, result);
        }

        [Test]
        public void FiveParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((v, w, x, y, z) => v + w + x + y + z);
            var result = func(6, 7, 8, 9, 10);
            Assert.AreEqual(40, result);
        }

        [Test]
        public void SixParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((u, v, w, x, y, z) => u + v + w + x + y + z);
            var result = func(5, 6, 7, 8, 9, 10);
            Assert.AreEqual(45, result);
        }

        [Test]
        public void SevenParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((t, u, v, w, x, y, z) => t + u + v + w + x + y + z);
            var result = func(4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(49, result);
        }

        [Test]
        public void EightParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((s, t, u, v, w, x, y, z) => s + t + u + v + w + x + y + z);
            var result = func(3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(52, result);
        }

        [Test]
        public void NineParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((r, s, t, u, v, w, x, y, z) => r + s + t + u + v + w + x + y + z);
            var result = func(2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(54, result);
        }

        [Test]
        public void TenParamFuncLambda_CanBeTypedAndRun()
        {
            var func = Lambda<int>((q, r, s, t, u, v, w, x, y, z) => q + r + s + t + u + v + w + x + y + z);
            var result = func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(55, result);
        }

        [Test]
        public void OneParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>(x => result = x * 2);
            _ = func(4);
            Assert.AreEqual(8, result);
        }

        [Test]
        public void TwoParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((x, y) => { result = x + y; });
            func(4, 5);
            Assert.AreEqual(9, result);
        }

        [Test]
        public void ThreeParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((x, y, z) => { result = x + y + z; });
            func(4, 5, 6);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void FourParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((w, x, y, z) => { result = w + x + y + z; });
            func(3, 4, 5, 6);
            Assert.AreEqual(18, result);
        }

        [Test]
        public void FiveParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((v, w, x, y, z) => { result = v + w + x + y + z; });
            func(3, 4, 5, 6, 7);
            Assert.AreEqual(25, result);
        }

        [Test]
        public void SixParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((u, v, w, x, y, z) => { result = u + v + w + x + y + z; });
            func(2, 3, 4, 5, 6, 7);
            Assert.AreEqual(27, result);
        }

        [Test]
        public void SevenParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((t, u, v, w, x, y, z) => { result = t + u + v + w + x + y + z; });
            func(2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(35, result);
        }

        [Test]
        public void EightParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((s, t, u, v, w, x, y, z) => { result = s + t + u + v + w + x + y + z; });
            func(2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(44, result);
        }

        [Test]
        public void NineParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((r, s, t, u, v, w, x, y, z) => { result = r + s + t + u + v + w + x + y + z; });
            func(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(45, result);
        }

        [Test]
        public void TenParamActionLambda_CanBeTypedAndRun()
        {
            var result = -1;
            var func = Lambda<int>((q, r, s, t, u, v, w, x, y, z) => { result = q + r + s + t + u + v + w + x + y + z; });
            func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(55, result);
        }
    }
}
