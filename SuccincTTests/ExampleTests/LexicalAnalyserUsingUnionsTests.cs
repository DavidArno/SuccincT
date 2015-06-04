using System.Linq;
using NUnit.Framework;
using SuccinctExamples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class LexicalAnalyserUsingUnionsTests
    {
        [Test]
        public void WhenSuppliedWithEmptyString_ParserJustReturnsNoResults()
        {
            var result = LexicalAnalyserUsingUnions.GenerateTokens(string.Empty);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void WhenSuppliedWithTokens_CorrectTypesAreReturned()
        {
            var result = LexicalAnalyserUsingUnions.GenerateTokens("false 1.2 42 hello").ToList();
            var expected = new object[] { false, 1.2, 42, "hello" };
            var actual = new object[] { result[0].Case3, result[1].Case4, result[2].Case2, result[3].Case1 };
            Assert.AreEqual(expected, actual);
        }
    }
}