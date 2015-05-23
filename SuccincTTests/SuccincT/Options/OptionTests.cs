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
            var result = Option<int>.Some(1);
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void WhenOptionIsValue_ResultHasValue()
        {
            var result = Option<int>.Some(1);
            Assert.IsTrue(result.HasValue);
        }

        [Test]
        public void WhenOptionNotValue_ResultIsNone()
        {
            var result = Option<int>.None();
            Assert.IsFalse(result.HasValue);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void WhenOptionNotValue_ResultsInExceptionIfValueRead()
        {
            var result = Option<bool>.None();
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void WhenOptionIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Option<int>.Some(1);
            option.Match().Some(x => intValue = x).None(() => { }).Exec();
            Assert.AreEqual(1, intValue);
        }

        [Test]
        public void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option<int>.None();
            option.Match().Some(x => { }).None(() => noneInvoked = true).Exec();
            Assert.IsTrue(noneInvoked);
        }

        [Test]
        public void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some(x => x).None(() => 0).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsNone_NoneResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some(x => 1).None(() => 0).Result();
            Assert.AreEqual(0, result);
        }
    }
}
