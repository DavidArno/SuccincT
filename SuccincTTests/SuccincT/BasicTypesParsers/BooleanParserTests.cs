using System;
using NUnit.Framework;
using SuccincT.BasicTypesParsers;

namespace SuccincTTests.SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the BooleanParser class.
    /// </summary>
    [TestFixture]
    public class BooleanParserTests
    {
        [Test]
        public void ValidTrueBooleanString_ResultsInValue()
        {
            var result = "true".ParseBoolean();
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void ValidFalseBooleanString_ResultsInValue()
        {
            var result = "false".ParseBoolean();
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void ValidBooleanString_ResultsInHavingValue()
        {
            var result = "true".ParseBoolean();
            Assert.IsTrue(result.HasValue);
        }

        [Test]
        public void InvalidBooleanString_ResultsInNone()
        {
            var result = "maybe".ParseBoolean();
            Assert.IsFalse(result.HasValue);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void InvalidBooleanString_ResultsInExceptionIfValueRead()
        {
            var result = "maybe".ParseBoolean();
            Assert.IsFalse(result.Value);
        }
    }
}
