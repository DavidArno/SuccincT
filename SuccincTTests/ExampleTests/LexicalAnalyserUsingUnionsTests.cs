using System.Linq;
using NUnit.Framework;
using SuccinctExamples;
using static NUnit.Framework.Assert;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class LexicalAnalyserUsingUnionsTests
    {
        [Test]
        public void WhenSuppliedWithEmptyString_ParserJustReturnsNoResults()
        {
            var result = LexicalAnalyserUsingUnions.GenerateTokens(string.Empty);
            AreEqual(0, result.Count());
        }

        [Test]
        public void WhenSuppliedWithTokens_CorrectTypesAreReturned()
        {
            var result = LexicalAnalyserUsingUnions.GenerateTokens("false 1.2 42 hello").ToList();
            AreEqual(false, result[0].Case3);
            AreEqual(1.2, result[1].Case4);
            AreEqual(42, result[2].Case2);
            AreEqual("hello", result[3].Case1);
        }
    }
}