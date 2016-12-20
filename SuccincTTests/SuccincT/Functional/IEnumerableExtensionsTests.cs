using NUnit.Framework;
using SuccincT.Functional;
using System.Collections.Generic;
using System.Linq;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class IEnumerableExtensionsTests
    {
        [Test]
        public void ForEmptyEnumeration_EmptyIndexEnumerationReturned()
        {
            var emptyList = new List<string>();
            var indexedList = emptyList.Indexed().ToList();
            Assert.AreEqual(0, indexedList.Count);
        }
    }
}
