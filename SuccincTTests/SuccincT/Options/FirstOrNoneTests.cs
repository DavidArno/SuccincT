using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class FirstOrNoneTests
    {
        [Test]
        public void FirstOrNoneWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.FirstOrNone<bool>(null).HasValue);
        }

        [Test]
        public void FirstOrNoneWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.FirstOrNone<bool>(null, x => true).HasValue);
        }

        [Test]
        public void FirstOrNoneWithEmptyCollection_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.FirstOrNone().HasValue);
        }

        [Test]
        public void FirstOrNoneWithEmptyCollectionAndFunc_ReturnsNone()
        {
            var collection = new List<bool>();
            Assert.IsFalse(collection.FirstOrNone(x => true).HasValue);
        }

        [Test]
        public void FirstOrNoneWithList_ReturnsFirstElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(1, collection.FirstOrNone().Value);
        }

        [Test]
        public void FirstOrNoneWithNoListCollection_ReturnsFirstElement()
        {
            Assert.AreEqual(1, IntCollection().FirstOrNone().Value);
        }

        [Test]
        public void FirstOrNoneWithCollectionAndFunc_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(2, collection.FirstOrNone(x => x == 2).Value);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void FirstOrNoneWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            var failMessage = collection.FirstOrNone(null).HasValue ? "value" : "none";
            Assert.Fail($"Expected exception but call returned {failMessage}");
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
        }
    }
}