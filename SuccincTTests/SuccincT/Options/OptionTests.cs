using System;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void WhenOptionIsValue_ResultIsValue()
        {
            var result = Option.Some(1);
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void WhenOptionIsValue_ResultHasValue()
        {
            var result = Option.Some(1);
            Assert.IsTrue(result.HasValue);
        }

        [Test]
        public void WhenOptionNotValue_ResultIsNone()
        {
            var result = Option.None<int>();
            Assert.IsFalse(result.HasValue);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void WhenOptionNotValue_ResultsInExceptionIfValueRead()
        {
            var result = Option.None<bool>();
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void WhenOptionIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Option.Some(1);
            option.MatchAndAction(x => { intValue = x; }, () => { });
            Assert.AreEqual(1, intValue);
        }

        [Test]
        public void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option.None<int>();
            option.MatchAndAction(x => { }, () => { noneInvoked = true; });
            Assert.IsTrue(noneInvoked);
        }

        [Test]
        public void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option.Some(1);
            var result = option.MatchAndResult(x => x, () => 0);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsNone_NoneResultIsReturned()
        {
            var option = Option.None<int>();
            var result = option.MatchAndResult(x => 1, () => 0);
            Assert.AreEqual(0, result);
        }
    }
}
