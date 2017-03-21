using NUnit.Framework;
using SuccincT.Parsers;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the FloatParsers sealed class.
    /// </summary>
    [TestFixture]
    public sealed class FloatParsersTests
    {
        [Test]
        public void ValidFloatString_ResultsInValue()
        {
            var result = "6.7567E20".TryParseFloat();
            AreEqual(6.7567E20f, result.Value);
        }

        [Test]
        public void ValidFloatString_ResultsInHasValue()
        {
            var result = "6.7567E20".TryParseFloat();
            IsTrue(result.HasValue);
        }

        [Test]
        public void ValidNegativeFloatString_ResultsInValue()
        {
            var result = "-1.78".TryParseFloat();
            AreEqual(-1.78f, result.Value);
        }

        [Test]
        public void ValidNegativeFloatString_ResultsInHasValue()
        {
            var result = "-1.78".TryParseFloat();
            IsTrue(result.HasValue);
        }

        [Test]
        public void ValidIntString_ResultsInValue()
        {
            var result = "27".TryParseFloat();
            AreEqual(27, result.Value);
        }

        [Test]
        public void ValidIntString_ResultsInHasValue()
        {
            var result = "27".TryParseFloat();
            IsTrue(result.HasValue);
        }

        [Test]
        public void InvalidFloatString_ResultsInNone()
        {
            var result = "1.1.1.1.1".TryParseFloat();
            IsFalse(result.HasValue);
        }

        [Test]
        public void ValidDoubleString_ResultsInValue()
        {
            var result = "5.0E-324".TryParseDouble();
            AreEqual(5.0E-324d, result.Value);
        }

        [Test]
        public void ValidDoubleString_ResultsInHasValue()
        {
            var result = "5.0E-324".TryParseDouble();
            IsTrue(result.HasValue);
        }

        [Test]
        public void InvalidDoubleString_ResultsInNone()
        {
            var result = "8.0E400".TryParseDouble();
            IsFalse(result.HasValue);
        }

        [Test]
        public void NonNumberDoubleString_ResultsInNone()
        {
            var result = "hello".TryParseDouble();
            IsFalse(result.HasValue);
        }

        [Test]
        public void ValidDecimalString_ResultsInValue()
        {
            var result = "12345678901234567890124567".TryParseDecimal();
            AreEqual(12345678901234567890124567m, result.Value);
        }

        [Test]
        public void ValidDecimalString_ResultsInHasValue()
        {
            var result = "12345678901234567890124567".TryParseDecimal();
            IsTrue(result.HasValue);
        }

        [Test]
        public void InvalidDecimalString_ResultsInNone()
        {
            var result = "8.1E10".TryParseDecimal();
            IsFalse(result.HasValue);
        }
    }
}