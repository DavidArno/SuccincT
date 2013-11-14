using NUnit.Framework;
using SuccincT.BasicTypesParsers;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the FloatParsers class.
    /// </summary>
    [TestFixture]
    public class FloatParsersTests
    {
        [Test]
        public void ValidFloatString_ResultsInSuccess()
        {
            var result = "6.7567E20".ParseFloat();
            Assert.AreEqual(6.7567E20f, result.Case1);
        }

        [Test]
        public void ValidNegativeFloatString_ResultsInSuccess()
        {
            var result = "-1.78".ParseFloat();
            Assert.AreEqual(-1.78f, result.Case1);
        }

        [Test]
        public void ValidIntString_ResultsInSuccess()
        {
            var result = "27".ParseFloat();
            Assert.AreEqual(27, result.Case1);
        }

        [Test]
        public void InvalidFloatString_ResultsInError()
        {
            var result = "1.1.1.1.1".ParseFloat();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidDoubleString_ResultsInSuccess()
        {
            var result = "5.0E-324".ParseDouble();
            Assert.AreEqual(5.0E-324d, result.Case1);
        }

        [Test]
        public void InvalidDoubleString_ResultsInError()
        {
            var result = "8.0E400".ParseDouble();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidDecimalString_ResultsInSuccess()
        {
            var result = "12345678901234567890124567".ParseDecimal();
            Assert.AreEqual(12345678901234567890124567m, result.Case1);
        }

        [Test]
        public void InvalidDecimalString_ResultsInError()
        {
            var result = "8.1E10".ParseDecimal();
            Assert.AreEqual(Variant.Case2, result.Case);
        }
    }
}
