using NUnit.Framework;
using SuccincT.PatternMatchers;
using System;
using SuccincT.Options;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class ValueOrErrorTVTETests
    {
        [Test]
        public void WhenValueIsSet_ValueSuppliedToFunction()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<string>().Value().Do(s => "v" + s).Error().Do(s => "e" + s).Result();
            AreEqual("v1", result);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToFunction()
        {
            var valueOrError = WithError(new Exception("2"));
            var result = valueOrError.Match<string>().Value().Do(s => "v" + s).Error().Do(e => "e" + e.Message)
                                     .Result();
            AreEqual("e2", result);
        }

        [Test]
        public void WhenValueIsSet_HasValueIsTrue()
        {
            var valueOrError = WithValue("1");
            IsTrue(valueOrError.HasValue);
        }

        [Test]
        public void WhenErrorIsSet_HasValueIsFalse()
        {
            var valueOrError = WithError(new Exception("error"));
            IsFalse(valueOrError.HasValue);
        }

        [Test]
        public void WhenValueIsSet_ValueCanBeAccessed()
        {
            var valueOrError = WithValue("1");
            AreEqual("1", valueOrError.Value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorCanBeAccessed()
        {
            var valueOrError = WithError(new Exception("error"));
            AreEqual("error", valueOrError.Error.Message);
        }

        [Test]
        public void WhenValueIsSet_AccessingErrorCausesException()
        {
            var valueOrError = WithValue("2");
            Throws<InvalidOperationException>(() => _ = valueOrError.Error);
        }

        [Test]
        public void WhenErrorIsSet_AccessingValueCausesAnException()
        {
            var valueOrError = WithError(new Exception("error"));
            Throws<InvalidOperationException>(() => _ = valueOrError.Value);
        }

        [Test]
        public void WhenValueIsSet_PrintStringYieldsValue()
        {
            var valueOrError = WithValue("42");
            AreEqual("Value of 42", valueOrError.ToString());
        }

        [Test]
        public void WhenErrorIsSet_PrintStringYieldsError()
        {
            var valueOrError = WithError(new Exception("error"));
            AreEqual("Error of System.Exception: error", valueOrError.ToString());
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = WithError(new Exception("error"));
            var result = valueOrError.Match<int>().Value().Do(x => 0).Else(x => 3).Result();
            AreEqual(3, result);
        }

        [Test]
        public void WhenValueIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Error().Do(x => 2).Else(3).Result();
            AreEqual(3, result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchDefined_ExceptionThrown()
        {
            var valueOrError = WithValue("1");
            Throws<NoMatchException>(() => valueOrError.Match<int>().Error().Do(x => 2).Result());
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchDefined_ExceptionThrown()
        {
            var valueOrError = WithError(new Exception("error"));
            Throws<NoMatchException>(() => _ = valueOrError.Match<int>().Value().Do(x => 2).Result());
        }

        [Test]
        public void WhenValue_SimpleValueDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>().Value().Of("1").Do(1).Value().Do(2).Error().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var valueOrError = WithValue("1");
            var result = valueOrError.Match<int>()
                                     .Value().Where(x => x == "1").Do(0).Value().Do(2).Error().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenError_SimpleErrorDoWithExpressionSupported()
        {
            var valueOrError = WithError(new Exception("error"));
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public void WhenError_ErrorOfDoWithExpressionSupported()
        {
            var error = new Exception("error");
            var valueOrError = WithError(error);
            var result = valueOrError.Match<int>().Value().Of("1").Do(1).Value().Do(2).Error().Of(error).Do(3).Result();
            AreEqual(3, result);
        }

        [Test]
        public void WhenError_ErrorWhereDoWithExpressionSupported()
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