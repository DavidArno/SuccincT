using System;
using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class ValueOrErrorTests
    {
        [Test]
        public void WhenValueIsSet_ValueSuppliedToFunction()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<string>().Value().Do(s => "v" + s).Error().Do(s => "e" + s).Result();
            Assert.AreEqual("v1", result);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToFunction()
        {
            var valueOrError = ValueOrError.WithError("2");
            var result = valueOrError.Match<string>().Value().Do(s => "v" + s).Error().Do(s => "e" + s).Result();
            Assert.AreEqual("e2", result);
        }

        [Test]
        public void WhenValueIsSet_HasValueIsTrue()
        {
            var valueOrError = ValueOrError.WithValue("1");
            Assert.IsTrue(valueOrError.HasValue);
        }

        [Test]
        public void WhenErrorIsSet_HasValueIsFalse()
        {
            var valueOrError = ValueOrError.WithError("2");
            Assert.IsFalse(valueOrError.HasValue);
        }

        [Test]
        public void WhenValueIsSet_ValueCanBeAccessed()
        {
            var valueOrError = ValueOrError.WithValue("1");
            Assert.AreEqual("1", valueOrError.Value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorCanBeAccessed()
        {
            var valueOrError = ValueOrError.WithError("2");
            Assert.AreEqual("2", valueOrError.Error);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidOperationException))]
        public void WhenValueIsSet_AccessingErrorCausesException()
        {
            var valueOrError = ValueOrError.WithValue("2");
            Assert.AreEqual("2", valueOrError.Error);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidOperationException))]
        public void WhenErrorIsSet_AccessingValueCausesAnException()
        {
            var valueOrError = ValueOrError.WithError("2");
            Assert.AreEqual("2", valueOrError.Value);
        }

        [Test]
        public void WhenValueIsSet_PrintStringYieldsValue()
        {
            var valueOrError = ValueOrError.WithValue("42");
            Assert.AreEqual("Value of 42", valueOrError.ToString());
        }

        [Test]
        public void WhenErrorIsSet_PrintStringYieldsError()
        {
            var valueOrError = ValueOrError.WithError("42");
            Assert.AreEqual("Error of 42", valueOrError.ToString());
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithError("2");
            var result = valueOrError.Match<int>().Value().Do(x => 0).Else(x => 3).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenValueIsSetAndNoErrorMatch_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<int>().Error().Do(x => 2).Else(x => 3).Result();
            Assert.AreEqual(3, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void WhenValueIsSetAndNoValueMatchDefined_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<int>().Error().Do(x => 2).Result();
            Assert.AreEqual(result, -1);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void WhenErrorIsSetAndNoErrorMatchDefined_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithError("1");
            var result = valueOrError.Match<int>().Value().Do(x => 2).Result();
            Assert.AreEqual(result, -1);
        }

        [Test, ExpectedException(ExpectedException = typeof(ArgumentNullException))]
        public void CreatingWithNullValue_CausesNullException()
        {
            var a = ValueOrError.WithValue(null);
            Assert.IsInstanceOf(typeof(ValueOrError), a);
        }

        [Test, ExpectedException(ExpectedException = typeof(ArgumentNullException))]
        public void CreatingWithNullError_CausesNullException()
        {
            var a = ValueOrError.WithError(null);
            Assert.IsInstanceOf(typeof(ValueOrError), a);
        }

        [Test]
        public void WhenValue_SimpleValueDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeOfDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<int>().Value().Of("1").Do(1).Value().Do(2).Error().Do(3).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void WhenSome_SomeWhereDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithValue("1");
            var result = valueOrError.Match<int>()
                                     .Value().Where(x => x == "1").Do(0).Value().Do(2).Error().Do(3).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenError_SimpleErrorDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithError("1");
            var result = valueOrError.Match<int>().Value().Do(1).Error().Do(2).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void WhenError_ErrorOfDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithError("1");
            var result = valueOrError.Match<int>().Value().Of("1").Do(1)
                                     .Value().Do(2)
                                     .Error().Of("1").Do(3).Result();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenError_ErrorWhereDoWithExpressionSupported()
        {
            var valueOrError = ValueOrError.WithError("1");
            var result = valueOrError.Match<int>()
                                     .Value().Where(x => x == "1").Do(0)
                                     .Value().Do(2)
                                     .Error().Where(x => x == "1").Do(3).Result();
            Assert.AreEqual(3, result);
        }
    }
}