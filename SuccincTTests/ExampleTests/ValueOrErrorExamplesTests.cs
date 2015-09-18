using NUnit.Framework;
using SuccincT.Options;
using SuccincTTests.Examples;
using static NUnit.Framework.Assert;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class ValueOrErrorExamplesTests
    {
        [Test]
        public void IntPrefex_ReturnsInt()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("Int:6"));
            AreEqual(6, result.Value);
        }

        [Test]
        public void IntPrefexReturnsNone_IfFollowedByJunk()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("Int:no number here"));
            IsFalse(result.HasValue);
        }

        [Test]
        public void ValueOtherThanIntPrefex_ReturnsNone()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithValue("xxx"));
            IsFalse(result.HasValue);
        }

        [Test]
        public void Error_ReturnsNone()
        {
            var result = ValueOrErrorExamples.IntParser(ValueOrError.WithError("xxx"));
            IsFalse(result.HasValue);
        }
    }
}