using System;
using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class ValueOrErrorTests
    {
        [Test]
        public void WhenValueIsSet_OnlyValueActionOccurs()
        {
            var valueOrError = ValueOrError.WithValue("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.Match().Value().Do(s => { valueSet = true; }).Error().Do(s => { errorSet = true; }).Exec();
            Assert.AreEqual(new[] { true, false }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenErrorIsSet_OnlyErrorActionOccurs()
        {
            var valueOrError = ValueOrError.WithError("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.Match().Value().Do(s => valueSet = true).Error().Do(s => errorSet = true).Exec();
            Assert.AreEqual(new[] { false, true }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenValueIsSet_ValueSuppliedToAction()
        {
            var valueOrError = ValueOrError.WithValue("x");
            var value = string.Empty;
            valueOrError.Match().Value().Do(s => value = s).Error().Do(s => { }).Exec();
            Assert.AreEqual("x", value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToAction()
        {
            var valueOrError = ValueOrError.WithError("x");
            var errorValue = string.Empty;
            valueOrError.Match().Value().Do(s => { }).Error().Do(s => errorValue = s).Exec();
            Assert.AreEqual("x", errorValue);
        }

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
        public void WhenErrorIsSetAndNoErrorMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithError("2");
            var result = "0";
            valueOrError.Match().Value().Do(x => result = x).Else(x => result = "1").Exec();
            Assert.AreEqual("1", result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithValue("2");
            var result = "0";
            valueOrError.Match().Error().Do(x => result = x).Else(x => result = "1").Exec();
            Assert.AreEqual("1", result);
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

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void WhenValueIsSetAndNoValueMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithValue("1");
            valueOrError.Match().Error().Do(x => { }).Exec();
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void WhenErrorIsSetAndNoErrorMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithError("1");
            valueOrError.Match().Value().Do(x => { }).Exec();
        }
    }
}
