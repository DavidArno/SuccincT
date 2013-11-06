using NUnit.Framework;
using SuccincT.BasicTypesParsers;
using SuccincT.Unions;

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
            Assert.IsTrue(result.Case1);
        }

        [Test]
        public void InvalidBooleanString_ResultsInError()
        {
            var result = "maybe".ParseBoolean();
            Assert.AreEqual(Variant.Case2, result.Case);
        }
    }
}
