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
            option.Match().Some().Do(x => intValue = x).None().Do(() => { }).Exec();
            Assert.AreEqual(1, intValue);
        }

        [Test]
        public void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option<int>.None();
            option.Match().Some().Do(x => { }).None().Do(() => noneInvoked = true).Exec();
            Assert.IsTrue(noneInvoked);
        }

        [Test]
        public void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(x => x).None().Do(() => 0).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsNone_NoneResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).None().Do(() => 0).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenOptionIsNoneElseIsDefinedAndNoNoneMatch_ElseResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Else(o => 0).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenOptionIsSomeElseIsDefinedAndNoSomeMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().None().Do(() => 0).Else(o => o.Value).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsSomeElseIsDefinedAndSomeDoesntMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(2);
            var result = option.Match<int>().Some().Of(1).Do(x => 1).None().Do(() => 0).Else(o => o.Value).Result();
            Assert.AreEqual(2, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidOperationException))]
        public void WhenOptionIsNoneAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Result();
            Assert.AreEqual(0, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidOperationException))]
        public void WhenOptionIsSomeValueAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().None().Do(() => 0).Result();
            Assert.AreEqual(0, result);
        }
    }
}
