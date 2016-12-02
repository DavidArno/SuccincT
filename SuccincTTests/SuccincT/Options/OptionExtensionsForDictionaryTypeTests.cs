using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    class OptionExtensionsForDictionaryTypeTests
    {
        [Test]
        public void WhenKeyExists_TryGetValueReturnsValue()
        {
            var dict = new Dictionary<int, int> { [1] = 1 };
            var value = dict.TryGetValue(1);
            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(1, value.Value);
        }

        [Test]
        public void WhenKeyDoesntExists_TryGetValueReturnsNone()
        {
            var dict = new Dictionary<int, int> { [1] = 1 };
            Assert.IsFalse(dict.TryGetValue(2).HasValue);
        }
    }
}
