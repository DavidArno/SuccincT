using System;
using NUnit.Framework;
using SuccincT.FunctionalComposition;

namespace SuccincTTests.FunctionalComposition
{
    [TestFixture]
    public class ExtendedTests
    {
        [Test]
        public void TwoParamFunctionIsComposableWithParam1()
        {
            Func<string, int, string> testFunction = (p1, p2) => string.Format(p1, p2);
            var expected = testFunction("{0}", 3);
            var composedFunction = Extended.Compose(testFunction, "{0}");
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TwoParamFunctionIsComposableWithParam2()
        {
            Func<string, int, string> testFunction = (p1, p2) => string.Format(p1, p2);
            var expected = testFunction("{0}", 3);
            var composedFunction = Extended.Compose(testFunction, 3);
            var actual = composedFunction("{0}");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam1()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, "{0}{1}");
            var actual = composedFunction(3, 4.0);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam2()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, 3);
            var actual = composedFunction("{0}{1}", 4.0);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam3()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, 4.0);
            var actual = composedFunction("{0}{1}", 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam1And2()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, "{0}{1}", 3);
            var actual = composedFunction(4.0);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam1And3()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, "{0}{1}", 4.0);
            var actual = composedFunction(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThreeParamFunctionIsComposableWithParam2And3()
        {
            Func<string, int, double, string> testFunction = (p1, p2, p3) => string.Format(p1, p2, p3);
            var expected = testFunction("{0}{1}", 3, 4.0);
            var composedFunction = Extended.Compose(testFunction, 3, 4.0);
            var actual = composedFunction("{0}{1}");
            Assert.AreEqual(expected, actual);
        }
    }
}
