using NUnit.Framework;

using SuccincT.Exceptions;
using SuccincT.Unions;

namespace SuccincTTests.Unions
{
    [TestFixture]
    public class FluentMatcherT1T2Tests
    {
        [Test, ExpectedException(typeof(NoMatchFoundException))]
        public void EmptyMatcher_ThrowsExceptionWhenEvaluated()
        {
            var matcher = new Union<string, int>(2).Matcher<bool>();
            matcher.Result();
        }

        [Test]
        public void MatcherWithJustElse_ReturnsElseValue()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Else(true);
            Assert.IsTrue(matcher.Result());
        }

        [Test, ExpectedException(typeof(NoMatchFoundException))]
        public void MatcherWithNonMatchingCase_ThrowsExceptionWhenEvaluated()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Case(x => x == 1, false);
            matcher.Result();
        }

        [Test]
        public void MatcherWithMatchingCase_ReturnsCaseValue()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Case(x => x == 2, false);
            Assert.IsFalse(matcher.Result());
        }

        [Test, ExpectedException(typeof(NoMatchFoundException))]
        public void MatcherWithOtherTypeCase_ThrowsExceptionWhenEvaluated()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Case(x => x == "a", false);
            matcher.Result();
        }

        [Test]
        public void MatcherWithMatchingStringCase_ReturnsCorrectCaseValue()
        {
            var matcher = new Union<string, int>("a")
                .Matcher<bool>()
                .Case(x => x == "a", false)
                .Case(x => x == "b", true);
            Assert.IsFalse(matcher.Result());
        }

        [Test]
        public void MatcherWithTwoMatchingCases_ReturnsFirstCorrectCaseValue()
        {
            var matcher = new Union<string, int>("a")
                .Matcher<bool>()
                .Case(x => x == "a", true)
                .Case(x => x == "a", false);
            Assert.IsTrue(matcher.Result());
        }

        [Test]
        public void MatcherWithNoMatchingCase_ReturnsElseValue()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Case(x => x == 1, false)
                .Else(true);
            Assert.IsTrue(matcher.Result());
        }

        [Test]
        public void MatcherWithMatchingCaseAndElse_ReturnsCaseValue()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Case(x => x == 2, false)
                .Else(true);
            Assert.IsFalse(matcher.Result());
        }

        [Test]
        public void MatcherWithIntCaseAddedAfterElse_ReturnsCaseValue()
        {
            var matcher = new Union<string, int>(2)
                .Matcher<bool>()
                .Else(true)
                .Case(x => x == 2, false);
            Assert.IsFalse(matcher.Result());
        }

        [Test]
        public void MatcherWithStringCaseAddedAfterElse_ReturnsCaseValue()
        {
            var matcher = new Union<string, int>("b")
                .Matcher<bool>()
                .Else(true)
                .Case(x => x == "b", false);
            Assert.IsFalse(matcher.Result());
        }
    }
}
