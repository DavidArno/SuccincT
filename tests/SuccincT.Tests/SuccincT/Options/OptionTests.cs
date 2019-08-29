using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using System;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public static class OptionTests
    {
        [Test]
        public static void WhenOptionIsValue_ResultIsValue()
        {
            var result = Option<int>.Some(1);
            AreEqual(1, result.Value);
        }

        [Test]
        public static void WhenOptionIsValue_ResultHasValue()
        {
            var result = Option<int>.Some(1);
            IsTrue(result.HasValue);
        }

        [Test]
        public static void WhenOptionNotValue_ResultIsNone()
        {
            var result = Option<int>.None();
            IsFalse(result.HasValue);
        }

        [Test]
        public static void WhenOptionNotValue_ResultsInExceptionIfValueRead()
        {
            var result = Option<bool>.None();
            Throws<InvalidOperationException>(() => _ = result.Value);
        }

        [Test]
        public static void WhenOptionIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Option<int>.Some(1);
            option.Match().Some().Do(x => intValue = x).None().Do(() => { }).Exec();
            AreEqual(1, intValue);
        }

        [Test]
        public static void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option<int>.None();
            option.Match().Some().Do(x => { }).None().Do(() => noneInvoked = true).Exec();
            IsTrue(noneInvoked);
        }

        [Test]
        public static void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(x => x).None().Do(() => 0).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenOptionIsNone_NoneResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).None().Do(() => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void WhenOptionIsNoneElseIsDefinedAndNoNoneMatch_ElseResultIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Else(o => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void WhenOptionIsNoneElseIsDefinedAndNoNoneMatch_ElseExpressionIsReturned()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(x => 1).Else(0).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void WhenOptionIsSomeElseIsDefinedAndNoSomeMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenOptionIsSomeElseIsDefinedAndSomeDoesntMatch_ElseResultIsReturned()
        {
            var option = Option<int>.Some(2);
            var result = option.Match<int>().Some().Of(1).Do(x => 1).None().Do(() => 0).Else(o => o.Value).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void WhenOptionIsNoneAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.None();
            Throws<NoMatchException>(() => _ = option.Match<int>().Some().Do(x => 1).Result());
        }

        [Test]
        public static void WhenOptionIsSomeValueAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            Throws<NoMatchException>(() => _ = option.Match<int>().None().Do(() => 0).Result());
        }

        [Test]
        public static void WhenOptionIsNoneAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.None();
            Throws<NoMatchException>(() => option.Match().Some().Do(x => { }).Exec());
        }

        [Test]
        public static void WhenOptionIsSomeValueAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            Throws<NoMatchException>(() => option.Match().None().Do(() => { }).Exec());
        }

        [Test]
        public static void WhenSome_SimpleSomeDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Of(1).Do(1).Some().Do(2).None().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Where(x => x < 2).Do(0).Some().Do(2).None().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void WhenNone_NoneDoWithExpressionSupported()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void WhenSome_DecomposeReturnsTrueAndValue()
        {
            var option = Option<int>.Some(1);
            var (hasValue, value) = option;
            IsTrue(hasValue);
            AreEqual(1, value);
        }

        [Test]
        public static void WhenNone_DecomposeReturnsFalseAndDefault()
        {
            var option = Option<int>.None();
            var (hasValue, value) = option;
            IsFalse(hasValue);
            AreEqual(0, value);
        }

        [Test]
        public static void WhenSome_ValueOrDefaultReturnsValue()
        {
            var option = Option<int>.Some(1);
            AreEqual(1, option.ValueOrDefault);
        }

        [Test]
        public static void WhenNone_ValueOrDefaultReturnsDefault()
        {
            var option = Option<int>.None();
            AreEqual(0, option.ValueOrDefault);
        }

        [Test]
        public static void WhenImplicitlyConverting_ValueEqualsProvidedParameter()
        {
            Option<int> option = 3;
            AreEqual(3, option.Value);
        }

        [Test]
        public static void Null_CannotBeUsedWithSomeAndThrowsException()
        {
            Throws<ArgumentNullException>(() => _ = Option<string>.Some(null));
        }
    }
}