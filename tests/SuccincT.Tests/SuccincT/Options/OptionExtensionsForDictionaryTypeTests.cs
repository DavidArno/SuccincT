using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class OptionExtensionsForDictionaryTypeTests
    {
        [Test]
        public void WhenKeyExists_TryGetValueReturnsValue()
        {
            var dict = new Dictionary<int, int> { [1] = 1 };
            var (hasValue, value) = dict.TryGetValue(1);
            Assert.IsTrue(hasValue);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void WhenKeyDoesNotExist_TryGetValueReturnsNone()
        {
            var dict = new Dictionary<int, int> { [1] = 1 };
            Assert.IsFalse(dict.TryGetValue(2).HasValue);
        }
    }
}
