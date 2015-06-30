using NUnit.Framework;
using SuccincT.Options;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class ValueOrErrorExamplesTests
    {
        [Test]
        public void IntPrefex_ReturnsInt()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("Int:6"));
            Assert.AreEqual(6, result.Value);
        }

        [Test]
        public void IntPrefexReturnsNone_IfFollowedByJunk()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("Int:no number here"));
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValueOtherThanIntPrefex_ReturnsNone()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("xxx"));
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void Error_ReturnsNone()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithError("xxx"));
            Assert.IsFalse(result.HasValue);
        }
    }
}