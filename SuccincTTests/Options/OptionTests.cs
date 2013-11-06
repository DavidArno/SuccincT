using NUnit.Framework;

using SuccincT.Options;
using SuccincT.Unions;

namespace SuccincTTests.Options
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void WhenResultIsSuccessful_ResultHasValue()
        {
            var result = Option.Some(1);
            Assert.AreEqual(1, result.Case1);
        }

        [Test]
        public void WhenResultNotSuccessful_ResultIsNone()
        {
            var result = Option.None<int>();
            Assert.AreEqual(Variant.Case2, result.Case);
        }
    }
}
