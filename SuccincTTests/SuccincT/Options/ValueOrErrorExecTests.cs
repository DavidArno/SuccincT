using NUnit.Framework;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static NUnit.Framework.Assert;

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
            valueOrError.Match().Value().Do(_ => valueSet = true).Error().Do(_ => errorSet = true).Exec();
            AreEqual(new[] { true, false }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenErrorIsSet_OnlyErrorActionOccurs()
        {
            var valueOrError = ValueOrError.WithError("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.Match().Value().Do(_ => valueSet = true).Error().Do(_ => errorSet = true).Exec();
            AreEqual(new[] { false, true }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenValueIsSet_ValueSuppliedToAction()
        {
            var valueOrError = ValueOrError.WithValue("x");
            var value = string.Empty;
            valueOrError.Match().Value().Do(s => value = s).Error().Do(_ => { }).Exec();
            AreEqual("x", value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToAction()
        {
            var valueOrError = ValueOrError.WithError("x");
            var errorValue = string.Empty;
            valueOrError.Match().Value().Do(_ => { }).Error().Do(s => errorValue = s).Exec();
            AreEqual("x", errorValue);
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithError("2");
            var result = "0";
            valueOrError.Match().Value().Do(x => result = x).Else(x => result = "1").Exec();
            AreEqual("1", result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchForExec_ElseResultIsReturned()
        {
            var valueOrError = ValueOrError.WithValue("2");
            var result = "0";
            valueOrError.Match().Error().Do(x => result = x).Else(x => result = "1").Exec();
            AreEqual("1", result);
        }

        [Test]
        public void WhenValueIsSetAndNoValueMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithValue("1");
            Throws<NoMatchException>(() => valueOrError.Match().Error().Do(x => { }).Exec());
        }

        [Test]
        public void WhenErrorIsSetAndNoErrorMatchDefinedForExec_ExceptionThrown()
        {
            var valueOrError = ValueOrError.WithError("1");
            Throws<NoMatchException>(() => valueOrError.Match().Value().Do(x => { }).Exec());
        }
    }
}