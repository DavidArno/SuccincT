using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class LastOrNoneTests
    {
        [Test]
        public void LastOrNoneWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TryLast<bool>(null).HasValue);
        }

        [Test]
        public void LastOrNoneWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TryLast<bool>(null, x => true).HasValue);
        }

        [Test]
        public void LastOrNoneWithEmptyCollection_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TryLast().HasValue);
        }

        [Test]
        public void LastOrNoneWithEmptyCollectionAndFunc_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TryLast(x => true).HasValue);
        }

        [Test]
        public void LastOrNoneWithList_ReturnsLastElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.TryLast().Value);
        }

        [Test]
        public void LastOrNoneWithNoListCollection_ReturnsLastElement()
        {
            Assert.AreEqual(3, IntCollection().TryLast().Value);
        }

        [Test]
        public void LastOrNoneWithCollectionAndFunc_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.TryLast(x => true).Value);
        }

        [Test]
        public void LastOrNoneWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            _ = Assert.Throws<ArgumentNullException>(() => collection.TryLast(null));
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}