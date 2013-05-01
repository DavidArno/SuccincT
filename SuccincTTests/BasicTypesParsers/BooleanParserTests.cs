using NUnit.Framework;
using SuccincT.BasicTypesParsers;

namespace SuccincTTests.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the BooleanParser class.
    /// </summary>
    [TestFixture]
    public class BooleanParserTests
    {
        [Test]
        public void ValidBooleanString_ResultsInSuccess()
        {
            var result = "true".ParseBoolean();
            var expected = new[] { true, true };
            var actual = new[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidBooleanString_ResultsInError()
        {
            var result = "maybe".ParseBoolean();
            Assert.IsFalse(result.Successful);
        }
    }
}
