using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class FirstOrNoneExtensionsTests
    {
        [Test]
        public void FirstOrNoneWithNull_ReturnsNone()
        {
            Assert.IsFalse(FirstOrNoneExtensions.FirstOrNone<bool>(null).HasValue);
        }

        [Test]
        public void FirstOrNoneWithNullAndFunc_ReturnsNone()
        {
            Assert.IsFalse(FirstOrNoneExtensions.FirstOrNone<bool>(null, x => true).HasValue);
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
        public void FirstOrNoneWithCollection_ReturnsFirstElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(1, collection.FirstOrNone().Value);
        }

        [Test]
        public void FirstOrNoneWithCollectionAndFunc_ReturnsCorrectElement()
        {
            var collection = new List<int> { 1, 2, 3 };
            Assert.AreEqual(2, collection.FirstOrNone(x => x == 2).Value);
        }
    }
}