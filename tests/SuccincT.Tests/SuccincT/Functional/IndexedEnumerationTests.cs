using NUnit.Framework;
using SuccincT.Functional;
using System.Collections.Generic;
using System.Linq;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class IndexedEnumerationTests
    {
        [Test]
        public void ForEmptyEnumeration_EmptyIndexEnumerationReturned()
        {
            var emptyList = new List<string>();
            var indexedList = emptyList.Indexed().ToList();
            AreEqual(0, indexedList.Count);
        }

        [Test]
        public void ForList_IndexedEnumerationReturned()
        {
            var list = new List<int> { 1, 2, 3 };
            var indexedList = list.Indexed();
            foreach (var indexedItem in indexedList)
            {
                AreEqual(indexedItem.index + 1, indexedItem.item);
            }
        }

        [Test]
        public void ForEnumeration_IndexedEnumerationReturned()
        {
            var indexedEnumeration = StringEnumeration().Indexed();
            foreach (var indexedItem in indexedEnumeration)
            {
                AreEqual($"{indexedItem.index}", indexedItem.item);
            }
        }

        private IEnumerable<string> StringEnumeration()
        {
            yield return "0";
            yield return "1";
            yield return "2";
        }
    }
}
