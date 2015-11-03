using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class SingleOrNoneTests
    {
        [Test]
        public void SingleOrNoneWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.SingleOrNone<bool>(null).HasValue);
        }

        [Test]
        public void SingleOrNoneWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.SingleOrNone<bool>(null, x => true).HasValue);
        }

        [Test]
        public void SingleOrNoneWithEmptyCollection_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.SingleOrNone().HasValue);
        }

        [Test]
        public void SingleOrNoneWithEmptyCollectionAndFunc_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.SingleOrNone(x => true).HasValue);
        }

        [Test]
        public void SingleOrNoneWithSingleItemList_ReturnsElement()
        {
            var collection = new List<int> { 1 };
            Assert.AreEqual(1, collection.LastOrNone().Value);
        }

        [Test]
        public void SingleOrNoneWithMultipleItemList_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.SingleOrNone().HasValue);
        }

        [Test]
        public void SingleOrNoneWithSingleItemNonListCollection_ReturnsElement()
        {
            Assert.AreEqual(1, SingleIntCollection().SingleOrNone().Value);
        }

        [Test]
        public void SingleOrNoneWithMultiItemNonListCollection_ReturnsNone()
        {
            Assert.IsFalse(MultiIntCollection().SingleOrNone().HasValue);
        }

        [Test]
        public void SingleOrNoneWithCollectionAndMatchOneElementFunc_ReturnsElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(2, collection.SingleOrNone(x => x == 2).Value);
        }

        [Test]
        public void SingleOrNoneWithCollectionAndMatchManyElementFunc_ReturnsNone()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.SingleOrNone(x => true).HasValue);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SingleOrNoneWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            var failMessage = collection.SingleOrNone(null).HasValue ? "value" : "none";
            Assert.Fail($"Expected exception but call returned {failMessage}");
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