using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class ValueOrErrorExecTests
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