using System;
using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class ValueOrErrorTVTEExecTests
    {
        [Test]
        public void WhenValueIsSet_OnlyValueActionOccurs()
        {
            var valueOrError = WithValue("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.Match().Value().Do(_ => valueSet = true).Error().Do(_ => errorSet = true).Exec();
            AreEqual(new[] { true, false }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenErrorIsSet_OnlyErrorActionOccurs()
        {
            var valueOrError = WithError(new Exception("x"));
            var valueSet = false;
            var errorSet = false;
            valueOrError.Match().Value().Do(_ => valueSet = true).Error().Do(_ => errorSet = true).Exec();
            AreEqual(new[] { false, true }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenValueIsSet_ValueSuppliedToAction()
        {
            var valueOrError = WithValue("x");
            var value = string.Empty;
            valueOrError.Match().Value().Do(s => value = s).Error().Do(_ => { }).Exec();
            AreEqual("x", value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToAction()
        {
            var valueOrError = WithError(new Exception("x"));
            var errorValue = string.Empty;
            valueOrError.Match().Value().Do(_ => { }).Error().Do(e => errorValue = e.Message).Exec();
            AreEqual("x", errorValue);
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = WithError(new Exception("x"));
            var result = "0";
            valueOrError.Match().Value().Do(x => result = x).Else(x => result = "1").Exec();
            AreEqual("1", result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = WithValue("2");
            var result = "0";
            valueOrError.Match().Error().Do(e => result = e.Message).Else(x => result = "1").Exec();
            AreEqual("1", result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = WithValue("1");
            Throws<NoMatchException>(() => valueOrError.Match().Error().Do(x => { }).Exec());
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = WithError(new Exception("x"));
            Throws<NoMatchException>(() => valueOrError.Match().Value().Do(x => { }).Exec());
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchDefinedForExecAndIgnoreElseUsed_NoActionTaken()
        {
            var result = "1";
            var valueOrError = WithValue("2");
            valueOrError.Match().Error().Do(x => result = "2").IgnoreElse().Exec();
            AreEqual("1", result);
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchDefinedForExecAndIgnoreElseUsed_NoActionTaken()
        {
            var result = 1;
            var valueOrError = WithError(new Exception("x"));
            valueOrError.Match().Value().Do(x => result = 2).IgnoreElse().Exec();
            AreEqual(1, result);
        }

        private static ValueOrError<string, Exception> WithValue(string s)
            => ValueOrError<string, Exception>.WithValue(s);

        private static ValueOrError<string, Exception> WithError(Exception e)
            => ValueOrError<string, Exception>.WithError(e);
    }
}