using NUnit.Framework;
using SuccincT.Options;
using SuccincT.Parsers;
using SuccincT.PatternMatchers;
using System;
using System.Collections.Generic;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class MaybeTests
    {
        [Test]
        public void WhenMaybeIsValue_ResultIsValue()
        {
            var result = Maybe<int>.Some(1);
            AreEqual(1, result.Value);
        }

        [Test]
        public void WhenMaybeIsValue_ResultHasValue()
        {
            var result = Maybe<int>.Some(1);
            IsTrue(result.HasValue);
        }

        [Test]
        public void WhenMaybeNotValue_ResultIsNone()
        {
            var result = Maybe<int>.None();
            IsFalse(result.HasValue);
        }

        [Test]
        public void WhenMaybeIsValueAndCastToOption_ResultIsStillValue()
        {
            Option<int> result = Maybe<int>.Some(1);
            AreEqual(1, result.Value);
        }

        [Test]
        public void WhenMaybeIsValueAndCastToOption_OptionHasValue()
        {
            Option<int> result = Maybe<int>.Some(1);
            IsTrue(result.HasValue);
        }

        [Test]
        public void WhenMaybeNotValueAndCastToOption_ResultIsStillNone()
        {
            Option<int> result = Maybe<int>.None();
            IsFalse(result.HasValue);
        }

        [Test]
        public void WhenMaybeNotValue_ResultsInExceptionIfValueRead()
        {
            var result = Maybe<bool>.None();
            Throws<InvalidOperationException>(() => _ = result.Value);
        }

        [Test]
        public void WhenMaybeIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Maybe<int>.Some(1);
            option.Match().Some().Do(x => intValue = x).None().Do(() => { }).Exec();
            AreEqual(1, intValue);
        }

        [Test]
        public void WhenMaybeIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Maybe<int>.None();
            option.Match().Some().Do(x => { }).None().Do(() => noneInvoked = true).Exec();
            IsTrue(noneInvoked);
        }

        [Test]
        public void WhenMaybeIsValue_ValueResultIsReturned()
        {
            var option = Maybe<int>.Some(1);
            var result = option.Match<int>().Some().Do(x => x).None().Do(() => 0).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenMaybeIsNone_NoneResultIsReturned()
        {
            var option = Maybe<int>.None();
            var result = option.Match<int>().Some().Do(_ => 1).None().Do(() => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenMaybeIsNoneElseIsDefinedAndNoNoneMatch_ElseResultIsReturned()
        {
            var option = Maybe<int>.None();
            var result = option.Match<int>().Some().Do(_ => 1).Else(_ => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenMaybeIsNoneElseIsDefinedAndNoNoneMatch_ElseExpressionIsReturned()
        {
            var option = Maybe<int>.None();
            var result = option.Match<int>().Some().Do(_ => 1).Else(0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenMaybeIsSomeElseIsDefinedAndNoSomeMatch_ElseResultIsReturned()
        {
            var option = Maybe<int>.Some(1);
            var result = option.Match<int>().None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenMaybeIsSomeElseIsDefinedAndSomeDoesntMatch_ElseResultIsReturned()
        {
            var option = Maybe<int>.Some(2);
            var result = option.Match<int>().Some().Of(1).Do(_ => 1).None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(2, result);
        }

        [Test]
        public void WhenMaybeIsNoneAndNoMatchDefined_ExceptionThrown()
        {
            var option = Maybe<int>.None();
            Throws<NoMatchException>(() => _ = option.Match<int>().Some().Do(_ => 1).Result());
        }

        [Test]
        public void WhenMaybeIsSomeValueAndNoMatchDefined_ExceptionThrown()
        {
            var option = Maybe<int>.Some(1);
            Throws<NoMatchException>(() => _ = option.Match<int>().None().Do(() => 0).Result());
        }

        [Test]
        public void WhenMaybeIsNoneAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Maybe<int>.None();
            Throws<NoMatchException>(() => option.Match().Some().Do(_ => { }).Exec());
        }

        [Test]
        public void WhenMaybeIsSomeValueAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Maybe<int>.Some(1);
            Throws<NoMatchException>(() => option.Match().None().Do(() => { }).Exec());
        }

        [Test]
        public void WhenSome_SimpleSomeDoWithExpressionSupported()
        {
            var option = Maybe<int>.Some(1);
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var option = Maybe<int>.Some(1);
            var result = option.Match<int>().Some().Of(1).Do(1).Some().Do(2).None().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var option = Maybe<int>.Some(1);
            var result = option.Match<int>().Some().Where(x => x < 2).Do(0).Some().Do(2).None().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenNone_NoneDoWithExpressionSupported()
        {
            var option = Maybe<int>.None();
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public void OptionParsers_CanBeUsedWithMaybe()
        {
            Maybe<int> intValue = "8".TryParseInt();
            Maybe<bool> boolValue = "True".TryParseBoolean();

            AreEqual(8, intValue.Value);
            IsTrue(boolValue.Value);
        }

        [Test]
        public void IEnumerableExtensions_CanBeUsedWithMaybe()
        {
            var list = new List<int> {1, 2, 3, 4};
            Maybe<int> match = list.TryFirst(item => item == 2);
            Maybe<int> noMatch = list.TryElementAt(5);

            AreEqual(2, match.Value);
            IsFalse(noMatch.HasValue);
        }

        [Test]
        public void WhenSome_DecomposeReturnsTrueAndValue()
        {
            var option = Maybe<int>.Some(1);
            var (hasValue, value) = option;
            IsTrue(hasValue);
            AreEqual(1, value);
        }

        [Test]
        public void WhenNone_DecomposeReturnsFalseAndDefault()
        {
            var option = Maybe<int>.None();
            var (hasValue, value) = option;
            IsFalse(hasValue);
            AreEqual(0, value);
        }

        [Test]
        public void WhenSome_ValueOrDefaultReturnsValue()
        {
            var option = Maybe<int>.Some(1);
            AreEqual(1, option.ValueOrDefault);
        }

        [Test]
        public void WhenNone_ValueOrDefaultReturnsDefault()
        {
            var option = Maybe<int>.None();
            AreEqual(0, option.ValueOrDefault);
        }

        [Test]
        public void WhenImplicitlyConverting_ValueEqualsProvidedParameter()
        {
            Maybe<int> maybe = 3;
            AreEqual(3, maybe.Value);
        }
    }
}