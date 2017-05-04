using NUnit.Framework;
using SuccincT.Parsers;
using System;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the BooleanParser sealed class.
    /// </summary>
    [TestFixture]
    public sealed class BooleanParserTests
    {
        [Test]
        public void ValidTrueBooleanString_ResultsInValue()
        {
            var result = "true".TryParseBoolean();
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void ValidFalseBooleanString_ResultsInValue()
        {
            var result = "false".TryParseBoolean();
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void ValidBooleanString_ResultsInHavingValue()
        {
            var result = "true".TryParseBoolean();
            Assert.IsTrue(result.HasValue);
        }

        [Test]
        public void InvalidBooleanString_ResultsInNone()
        {
            var result = "maybe".TryParseBoolean();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void InvalidBooleanString_ResultsInExceptionIfValueRead()
        {
            var result = "maybe".TryParseBoolean();
            Assert.Throws<InvalidOperationException>(() => Ignore(result.Value));
        }
    }
}