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
            var valueOrError = ValueOrError.CreateWithValue("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.MatchAndAction(s => { valueSet = true; }, s => { errorSet = true; });
            Assert.AreEqual(new[] { true, false }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenErrorIsSet_OnlyErrorActionOccurs()
        {
            var valueOrError = ValueOrError.CreateWithError("x");
            var valueSet = false;
            var errorSet = false;
            valueOrError.MatchAndAction(s => { valueSet = true; }, s => { errorSet = true; });
            Assert.AreEqual(new[] { false, true }, new[] { valueSet, errorSet });
        }

        [Test]
        public void WhenValueIsSet_ValueSuppliedToAction()
        {
            var valueOrError = ValueOrError.CreateWithValue("x");
            var value = string.Empty;
            valueOrError.MatchAndAction(s => { value = s; }, s => { });
            Assert.AreEqual("x", value);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToAction()
        {
            var valueOrError = ValueOrError.CreateWithError("x");
            var errorValue = string.Empty;
            valueOrError.MatchAndAction(s => { }, s => { errorValue = s; });
            Assert.AreEqual("x", errorValue);
        }

        [Test]
        public void WhenValueIsSet_ValueSuppliedToFunction()
        {
            var valueOrError = ValueOrError.CreateWithValue("1");
            var result = valueOrError.MatchAndResult(s => "v" + s, s => "e" + s);
            Assert.AreEqual("v1", result);
        }

        [Test]
        public void WhenErrorIsSet_ErrorSuppliedToFunction()
        {
            var valueOrError = ValueOrError.CreateWithError("2");
            var result = valueOrError.MatchAndResult(s => "v" + s, s => "e" + s);
            Assert.AreEqual("e2", result);
        }

        [Test]
        public void WhenValueIsSet_HasValueIsTrue()
        {
            var valueOrError = ValueOrError.CreateWithValue("1");
            Assert.IsTrue(valueOrError.HasValue);
        }

        [Test]
        public void WhenErrorIsSet_HasValueIsFalse()
        {
            var valueOrError = ValueOrError.CreateWithError("2");
            Assert.IsFalse(valueOrError.HasValue);
        }

        [Test]
        public void WhenValueIsSet_ValueCanBeAccessed()
        {
            var valueOrError = ValueOrError.CreateWithValue("1");
            Assert.AreEqual("1", valueOrError.ValueOrErrorString);
        }

        [Test]
        public void WhenErrorIsSet_ErrorCanBeAccessed()
        {
            var valueOrError = ValueOrError.CreateWithError("2");
            Assert.AreEqual("2", valueOrError.ValueOrErrorString);
        }
    }
}
