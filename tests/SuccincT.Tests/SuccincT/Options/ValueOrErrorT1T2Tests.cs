﻿using NUnit.Framework;
using SuccincT.PatternMatchers;
using System;
using SuccincT.Options;
using static NUnit.Framework.Assert;
using static SuccincT.Options.ValueOrErrorState;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public static class ValueOrErrorT1T2Tests
    {
        [Test]
        public static void WhenValueIsSet_ValueSuppliedToFunction()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<string>().Value().Do(s => "v" + s).Error().Do(s => "e" + s).Result();
            var result2 = valueOrError switch {
                (Value, var v, _) => $"vv{v}",
                (_, _, var e) => $"ee{e}"
            };
            
            AreEqual("v1", result);
            AreEqual("vv1", result2);
        }

        [Test]
        public static void WhenErrorIsSet_ErrorSuppliedToFunction()
        {
            var valueOrError = WithError(new Exception("2"));
            var result = valueOrError.Match<string>()
                                     .Value().Do(s => "v" + s)
                                     .Error().Do(e => "e" + e.Message)
                                     .Result();

            var result2 = valueOrError switch {
                (Error, _, var e) => $"ee{e.Message}",
                (_, var v, _) => $"vv{v}"
            };

            AreEqual("e2", result);
            AreEqual("ee2", result2);
        }

        [Test]
        public static void WhenValueIsSet_HasValueIsTrue()
        {
            var valueOrError = WithValue("1");
            IsTrue(valueOrError.HasValue);
        }

        [Test]
        public static void WhenErrorIsSet_HasValueIsFalse()
        {
            var valueOrError = WithError(new Exception("error"));
            IsFalse(valueOrError.HasValue);
        }

        [Test]
        public static void WhenValueIsSet_ValueCanBeAccessed()
        {
            var valueOrError = WithValue("1");
            AreEqual("1", valueOrError.Value);
        }

        [Test]
        public static void WhenErrorIsSet_ErrorCanBeAccessed()
        {
            var valueOrError = WithError(new Exception("error"));
            AreEqual("error", valueOrError.Error.Message);
        }

        [Test]
        public static void WhenValueIsSet_AccessingErrorCausesException()
        {
            var valueOrError = WithValue("2");
            _ = Throws<InvalidOperationException>(() => _ = valueOrError.Error);
        }

        [Test]
        public static void WhenErrorIsSet_AccessingValueCausesAnException()
        {
            var valueOrError = WithError(new Exception("error"));
            _ = Throws<InvalidOperationException>(() => _ = valueOrError.Value);
        }

        [Test]
        public static void WhenValueIsSet_PrintStringYieldsValue()
        {
            var valueOrError = WithValue("42");
            AreEqual("Value of 42", valueOrError.ToString());
        }

        [Test]
        public static void WhenErrorIsSet_PrintStringYieldsError()
        {
            var valueOrError = WithError(new Exception("error"));
            AreEqual("Error of System.Exception: error", valueOrError.ToString());
        }

        [Test]
        public static void WhenErrorIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = WithError(new Exception("error"));
            var result = valueOrError.Match<int>().Value().Do(x => 0).Else(x => 3).Result();
            AreEqual(3, result);
        }

        [Test]
        public static void WhenValueIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Error().Do(x => 2).Else(x => 3).Result();
            AreEqual(3, result);
        }

        [Test]
        public static void WhenValueIsSetAndNoValueMatchDefined_ExceptionThrown()
        {
            var valueOrError = WithValue("1");
            _ = Throws<NoMatchException>(() => valueOrError.Match<int>().Error().Do(x => 2).Result());
        }

        [Test]
        public static void WhenErrorIsSetAndNoErrorMatchDefined_ExceptionThrown()
        {
            var valueOrError = WithError(new Exception("error"));
            _ = Throws<NoMatchException>(() => _ = valueOrError.Match<int>().Value().Do(x => 2).Result());
        }

        [Test]
        public static void WhenValue_SimpleValueDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Value().Of("1").Do(1).Value().Do(2).Error().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public static void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>()
                                     .Value().Where(x => x == "1").Do(0).Value().Do(2).Error().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public static void WhenError_SimpleErrorDoWithExpressionSupported()
        {
            var valueOrError = WithError(new Exception("error"));
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public static void WhenError_ErrorOfDoWithExpressionSupported()
        {
            var error = new Exception("error");
            var valueOrError = WithError(error);
            var result = valueOrError.Match<int>().Value().Of("1").Do(1).Value().Do(2).Error().Of(error).Do(3).Result();
            AreEqual(3, result);
        }

        [Test]
        public static void WhenError_ErrorWhereDoWithExpressionSupported()
        {
            var valueOrError = WithError(new Exception("error"));
            var result = valueOrError.Match<int>().Value().Where(x => x == "error").Do(0).Value().Do(2)
                                     .Error().Where(x => x.Message == "error").Do(3).Result();
            AreEqual(3, result);
        }

        private static ValueOrError<string, Exception> WithValue(string s)
            => ValueOrError<string, Exception>.WithValue(s);

        private static ValueOrError<string, Exception> WithError(Exception e)
            => ValueOrError<string, Exception>.WithError(e);
    }
}