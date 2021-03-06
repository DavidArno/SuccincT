﻿using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using System;
using static NUnit.Framework.Assert;
using static SuccincT.Options.Option;

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
            _ = Throws<InvalidOperationException>(() => _ = result.Value);
        }

        [Test]
        public static void WhenOptionIsValue_ValueActionInvoked()
        {
            var intValue = 0;
            var option = Option<int>.Some(1);
            option.Match().Some().Do(x => intValue = x).None().Do(() => {}).Exec();
            AreEqual(1, intValue);
        }

        [Test]
        public static void WhenOptionIsNone_NoneActionInvoked()
        {
            var noneInvoked = false;
            var option = Option<int>.None();
            option.Match().Some().Do(x => {}).None().Do(() => noneInvoked = true).Exec();
            IsTrue(noneInvoked);
        }

        [Test]
        public static void WhenOptionIsValue_ValueResultIsReturned()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>().Some().Do(x => x).None().Do(() => 0).Result();
            var result2 = option switch {
                (None, _) => 0,
                (_, var x) => x
            };

            AreEqual(1, result);
            AreEqual(1, result2);
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
            _ = Throws<NoMatchException>(() => _ = option.Match<int>().Some().Do(x => 1).Result());
        }

        [Test]
        public static void WhenOptionIsSomeValueAndNoMatchDefined_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            _ = Throws<NoMatchException>(() => _ = option.Match<int>().None().Do(() => 0).Result());
        }

        [Test]
        public static void WhenOptionIsNoneAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.None();
            _ = Throws<NoMatchException>(() => option.Match().Some().Do(x => {}).Exec());
        }

        [Test]
        public static void WhenOptionIsSomeValueAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var option = Option<int>.Some(1);
            _ = Throws<NoMatchException>(() => option.Match().None().Do(() => {}).Exec());
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
            var result2 = option switch {
                (Some, 1) => 1,
                (Some, _) => 2,
                _ => 3
            };

            AreEqual(1, result);
            AreEqual(1, result2);
        }

        [Test]
        public static void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var option = Option<int>.Some(1);
            var result = option.Match<int>()
                               .Some().Where(x => x < 2).Do(0)
                               .Some().Do(2)
                               .None().Do(3)
                               .Result();
            
            var result2 = option switch {
                (Some, var x) when x < 2 => 0,
                (Some, _) => 2,
                _ => 3
            };
            
            AreEqual(0, result);
            AreEqual(0, result2);
        }

        [Test]
        public static void WhenNone_NoneDoWithExpressionSupported()
        {
            var option = Option<int>.None();
            var result = option.Match<int>().Some().Do(1).None().Do(2).Result();
            var result2 = option switch
            {
                (None, _) => 2,
                (_, var x) => x
            };
            AreEqual(2, result);
            AreEqual(2, result2);
        }

        [Test]
        public static void WhenSome_DecomposeReturnsTrueAndValue()
        {
            var option = Option<int>.Some(1);
            var (state, value) = option;
            AreEqual(Some, state);
            AreEqual(1, value);
        }

        [Test]
        public static void WhenNone_DecomposeReturnsFalseAndDefault()
        {
            var option = Option<int>.None();
            var (state, value) = option;
            AreEqual(None, state);
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
            _ = Throws<ArgumentNullException>(() => _ = Option<string>.Some(null));
        }

        [Test]
        public static void OldStyleDeconstruct_CanBeSimulated()
        {
            Option<int> option = 3;

            var (hasValue, value) = option is (Some, var v) ? (true, v) : (false, default);

            Multiple(() => {
                IsTrue(hasValue);
                AreEqual(3, value);
            });
        }
    }
}