using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class ElementAtOrNoneTests
    {
        [Test]
        public void ElementAtOrNoneWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.ElementAtOrNone<bool>(null, 0).HasValue);
        }

        [Test]
        public void ElementAtOrNoneWithEmptyCollection_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().ElementAtOrNone(0).HasValue);
        }

        [Test]
        public void ElementAtOrNoneWithList_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.ElementAtOrNone(2).Value);
        }

        [Test]
        public void ElementAtOrNoneWithTooSmallList_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.ElementAtOrNone(3).HasValue);
        }

        [Test]
        public void ElementAtOrNoneWithNegativeIndex_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.ElementAtOrNone(-1).HasValue);
        }

        [Test]
        public void ElementAtOrNoneWithNoListCollection_ReturnsCorrectElement()
        {
            Assert.AreEqual(2, IntCollection().ElementAtOrNone(1).Value);
        }

        [Test]
        public void ElementAtOrNoneWithTooSmallNoListCollection_ReturnsNone()
        {
            Assert.IsFalse(IntCollection().ElementAtOrNone(3).HasValue);
        }

        [Test]
        public void ElementAtOrNoneWithEmptyNoListCollection_ReturnsNone()
        {
            Assert.IsFalse(IntCollection().Where(x => false).ElementAtOrNone(0).HasValue);
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}