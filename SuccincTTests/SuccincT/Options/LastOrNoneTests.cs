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
            Assert.IsFalse(OptionExtensionsForIEnumerable.LastOrNone<bool>(null).HasValue);
        }

        [Test]
        public void LastOrNoneWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.LastOrNone<bool>(null, x => true).HasValue);
        }

        [Test]
        public void LastOrNoneWithEmptyCollection_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.LastOrNone().HasValue);
        }

        [Test]
        public void LastOrNoneWithEmptyCollectionAndFunc_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.LastOrNone(x => true).HasValue);
        }

        [Test]
        public void LastOrNoneWithList_ReturnsLastElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.LastOrNone().Value);
        }

        [Test]
        public void LastOrNoneWithNoListCollection_ReturnsLastElement()
        {
            Assert.AreEqual(3, IntCollection().LastOrNone().Value);
        }

        [Test]
        public void LastOrNoneWithCollectionAndFunc_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(3, collection.LastOrNone(x => true).Value);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void LastOrNoneWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            var failMessage = collection.LastOrNone(null).HasValue ? "value" : "none";
            Assert.Fail($"Expected exception but call returned {failMessage}");
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}