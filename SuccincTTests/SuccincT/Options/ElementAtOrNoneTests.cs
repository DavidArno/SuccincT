using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class TryElementAtTests
    {
        [Test]
        public void TryElementAtWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TryElementAt<bool>(null, 0).HasValue);
        }

        [Test]
        public void TryElementAtWithEmptyCollection_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TryElementAt(0).HasValue);
        }

        [Test]
        public void TryElementAtWithList_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.TryElementAt(2).Value);
        }

        [Test]
        public void TryElementAtWithTooSmallList_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.TryElementAt(3).HasValue);
        }

        [Test]
        public void TryElementAtWithNegativeIndex_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.TryElementAt(-1).HasValue);
        }

        [Test]
        public void TryElementAtWithNonListCollection_ReturnsCorrectElement()
        {
            Assert.AreEqual(2, IntCollection().TryElementAt(1).Value);
        }

        [Test]
        public void TryElementAtResult_CanBeAssignedToMaybe()
        {
            Maybe<int> maybe = IntCollection().TryElementAt(1);
            Assert.AreEqual(2, maybe.Value);
        }

        [Test]
        public void TryElementAtWithTooSmallNoListCollection_ReturnsNone()
        {
            Assert.IsFalse(IntCollection().TryElementAt(3).HasValue);
        }

        [Test]
        public void TryElementAtWithEmptyNoListCollection_ReturnsNone()
        {
            Assert.IsFalse(IntCollection().Where(x => false).TryElementAt(0).HasValue);
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}