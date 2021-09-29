using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class TrySingleTests
    {
        [Test]
        public void TrySingleWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TrySingle<bool>(null).HasValue);
        }

        [Test]
        public void TrySingleWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TrySingle<bool>(null, x => true).HasValue);
        }

        [Test]
        public void TrySingleWithEmptyCollection_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TrySingle().HasValue);
        }

        [Test]
        public void TrySingleWithEmptyCollectionAndFunc_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TrySingle(x => true).HasValue);
        }

        [Test]
        public void TrySingleWithSingleItemList_ReturnsElement()
        {
            var collection = new List<int> { 1 };
            Assert.AreEqual(1, collection.TryLast().Value);
        }

        [Test]
        public void TrySingleWithMultipleItemList_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.TrySingle().HasValue);
        }

        [Test]
        public void TrySingleWithSingleItemNonListCollection_ReturnsElement()
        {
            Assert.AreEqual(1, SingleIntCollection().TrySingle().Value);
        }

        [Test]
        public void TrySingleWithMultiItemNonListCollection_ReturnsNone()
        {
            Assert.IsFalse(MultiIntCollection().TrySingle().HasValue);
        }

        [Test]
        public void TrySingleWithCollectionAndMatchOneElementFunc_ReturnsElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(2, collection.TrySingle(x => x == 2).Value);
        }

        [Test]
        public void TrySingleWithCollectionAndMatchManyElementFunc_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.TrySingle(x => true).HasValue);
        }

        [Test]
        public void TrySingleWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            _ = Assert.Throws<ArgumentNullException>(() => collection.TrySingle(null));
        }

        private static IEnumerable<int> SingleIntCollection()
        {
            yield return 1;
        }

        private static IEnumerable<int> MultiIntCollection()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}