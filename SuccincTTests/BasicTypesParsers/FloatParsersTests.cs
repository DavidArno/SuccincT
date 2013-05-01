using NUnit.Framework;
using SuccincT.BasicTypesParsers;

namespace SuccincTTests.BasicTypesParsers
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
            var expected = new object[] { true, 6.7567E20f };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidNegativeFloatString_ResultsInSuccess()
        {
            var result = "-1.78".ParseFloat();
            var expected = new object[] { true, -1.78f };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidIntString_ResultsInSuccess()
        {
            var result = "27".ParseFloat();
            var expected = new object[] { true, 27 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidFloatString_ResultsInError()
        {
            var result = "1.1.1.1.1".ParseFloat();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidDoubleString_ResultsInSuccess()
        {
            var result = "5.0E-324".ParseDouble();
            var expected = new object[] { true, 5.0E-324d };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidDoubleString_ResultsInError()
        {
            var result = "8.0E400".ParseDouble();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidDecimalString_ResultsInSuccess()
        {
            var result = "12345678901234567890124567".ParseDecimal();
            var expected = new object[] { true, 12345678901234567890124567m };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidDecimalString_ResultsInError()
        {
            var result = "8.1E10".ParseDecimal();
            Assert.IsFalse(result.Successful);
        }
    }
}
