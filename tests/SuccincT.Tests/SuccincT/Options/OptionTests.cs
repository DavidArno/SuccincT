﻿using System;
using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class OptionTests
    {
        [Test]
        public void WhenOptionIsValue_ResultIsValue()
        {
            var result = Option<int>.Some(1);
            AreEqual(1, result.Value);
        }

        [Test]
        public void WhenOptionIsValue_ResultHasValue()
        {
            var result = Option<int>.Some(1);
            IsTrue(result.HasValue);
        }

        [Test]
        public void WhenOptionNotValue_ResultIsNone()
        {
            var result = Option<int>.None();
            IsFalse(result.HasValue);
        }

        [Test]
        public void WhenOptionIsValueAndCastToMaybe_ResultIsStillValue()
        {
            Maybe<int> result = Option<int>.Some(1);
            AreEqual(1, result.Value);
        }

        [Test]
        public void WhenOptionIsValueAndCastToMaybe_MaybeHasValue()
        {
            Maybe<int> result = Option<int>.Some(1);
            IsTrue(result.HasValue);
        }

        [Test]
        public void WhenOptionNotValueAndCastToMaybe_ResultIsStillNone()
        {
            Maybe<int> result = Option<int>.None();
            IsFalse(result.HasValue);
        }

        [Test]
        public void WhenOptionNotValue_ResultsInExceptionIfValueRead()
        {
            var result = Option<bool>.None();
            Throws<InvalidOperationException>(() => Ignore(result.Value));
        }

        [Test]
        public void WhenOptionIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Option<int>.Some(1);
            option.Match().Some().Do(x => intValue = x).None().Do(() => { }).Exec();
            AreEqual(1, intValue);
        }

        [Test]
        public void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option<int>.None();
            option.Match().Some().Do(x => { }).None().Do(() => noneInvoked = true).Exec();
            IsTrue(noneInvoked);
        }

        [Test]
        public void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(x => x).None().Do(() => 0).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsNone_NoneResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).None().Do(() => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenOptionIsNoneElseIsDefinedAndNoNoneMatch_ElseResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Else(o => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenOptionIsNoneElseIsDefinedAndNoNoneMatch_ElseExpressionIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Else(0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenOptionIsSomeElseIsDefinedAndNoSomeMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenOptionIsSomeElseIsDefinedAndSomeDoesntMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(2);
            var result = option.Match<int>().Some().Of(1).Do(x => 1).None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(2, result);
        }

        [Test]
        public void WhenOptionIsNoneAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.None();
            Throws<NoMatchException>(() => Ignore(option.Match<int>().Some().Do(x => 1).Result()));
        }

        [Test]
        public void WhenOptionIsSomeValueAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            Throws<NoMatchException>(() => Ignore(option.Match<int>().None().Do(() => 0).Result()));
        }

        [Test]
        public void WhenOptionIsNoneAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.None();
            Throws<NoMatchException>(() => option.Match().Some().Do(x => { }).Exec());
        }

        [Test]
        public void WhenOptionIsSomeValueAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            Throws<NoMatchException>(() => option.Match().None().Do(() => { }).Exec());
        }

        [Test]
        public void WhenSome_SimpleSomeDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Of(1).Do(1).Some().Do(2).None().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Where(x => x < 2).Do(0).Some().Do(2).None().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenNone_NoneDoWithExpressionSupported()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(2, result);
        }
    }
}