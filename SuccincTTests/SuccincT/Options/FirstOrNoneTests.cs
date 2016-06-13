using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class TryFirstTests
    {
        [Test]
        public void TryFirstWithNull_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TryFirst<bool>(null).HasValue);
        }

        [Test]
        public void TryFirstWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(OptionExtensionsForIEnumerable.TryFirst<bool>(null, x => true).HasValue);
        }

        [Test]
        public void TryFirstWithEmptyCollection_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TryFirst().HasValue);
        }

        [Test]
        public void TryFirstWithEmptyCollectionAndFunc_ReturnsNone()
        {
            Assert.IsFalse(new List<bool>().TryFirst(x => true).HasValue);
        }

        [Test]
        public void TryFirstWithList_ReturnsFirstElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(1, collection.TryFirst().Value);
        }

        [Test]
        public void TryFirstWithNoListCollection_ReturnsFirstElement()
        {
            Assert.AreEqual(1, IntCollection().TryFirst().Value);
        }

        [Test]
        public void TryFirstWithCollectionAndFunc_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(2, collection.TryFirst(x => x == 2).Value);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TryFirstWithNullFunc_ThrowsExcpetion()
        {
            var collection = new List<int> { 1, 2, 3 };
            var failMessage = collection.TryFirst(null).HasValue ? "value" : "none";
            Assert.Fail($"Expected exception but call returned {failMessage}");
        }

        [Test]
        public void TryFirst_CanBeAssignedToMaybe()
        {
            var collection = new List<int> { 1, 2, 3 };
            Maybe<int> maybe = collection.TryFirst(x => x == 2);
            Assert.AreEqual(2, maybe.Value);
        }

        private static IEnumerable<int> IntCollection()
        {
            yield return 1;
        }
    }
}