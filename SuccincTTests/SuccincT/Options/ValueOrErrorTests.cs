using NUnit.Framework;
using SuccincT.Options;

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
    }
}
