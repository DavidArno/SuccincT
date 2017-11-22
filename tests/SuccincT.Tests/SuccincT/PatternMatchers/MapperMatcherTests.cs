using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using System.Collections.Generic;
using System.Linq;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class MapperMatcherTests
    {
        [Test]
        public void EmptyList_ResultsInOneElementEnumerationWhenEmptyClauseUsed()
        {
            var list = new List<int>();
            var result = list.Match().MapTo<(int, int)>()
                             .Empty().Do((0, 0))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((0, 0), head);
        }

        [Test]
        public void EmptyListWithNoEmptyClause_ThrowsCorrectException()
        {
            var list = new List<int>();
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<(int, int)>()
                                                      .Single().Do((0, 0))
                                                      .Result());
        }

        [Test]
        public void SingleList_ResultsInOneElementEnumerationWhenValueSingleClauseUsed()
        {
            var list = new List<int> { 1 };
            var result = list.Match().MapTo<(int, int)>()
                             .Single().Do((0, 1))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((0, 1), head);
        }

        [Test]
        public void SingleList_ResultsInOneElementEnumerationWhenFuncSingleClauseUsed()
        {
            var list = new List<int> { 1 };
            var result = list.Match().MapTo<(int, int)>()
                             .Single().Do(x => (x, 0))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((1, 0), head);
        }

        [Test]
        public void SingleListWithMultipleValueSingleClauses_UsesTheCorrectSingleClause()
        {
            var list = new List<int> { 1 };
            var result = list.Match().MapTo<int>()
                             .Single().Where(x => x == 0).Do(0)
                             .Single().Where(x => x == 1).Do(1)
                             .Single().Where(x => x == 2).Do(2)
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual(1, head);
        }

        [Test]
        public void SingleListWithMultipleFuncSingleClauses_UsesTheCorrectSingleClause()
        {
            var list = new List<int> { 1 };
            var result = list.Match().MapTo<(int, int)>()
                             .Single().Where(x => x == 0).Do(x => (x, 0))
                             .Single().Where(x => x == 1).Do(x => (x, 1))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((1, 1), head);
        }

        [Test]
        public void SingleListWithNoMatchingSingleClause_ThrowsCorrectException()
        {
            var list = new List<int> { 1 };
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<int>()
                                                      .Single().Where(x => x == 0).Do(x => x)
                                                      .Result());
        }

        [Test]
        public void MultipleListWithNoMatchingSingleClause_ThrowsCorrectException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<int>()
                                                      .Single().Where(x => x == 0).Do(x => x)
                                                      .Result());
        }

        [Test]
        public void MultipleList_WithSimpleReturnValuesLogicReturnsSameList()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Match().MapTo<int>()
                             .Single().Do(x => x)
                             .RecursiveCons().Do((x, _, soFar) => soFar.Cons(x))
                             .Result();

            var consResult = result.ToConsEnumerable();
            var (one, (two, (three, _))) = consResult;
            Assert.AreEqual(1, one);
            Assert.AreEqual(2, two);
            Assert.AreEqual(3, three);
        }

        [Test]
        public void MultipleList_WithCalculatedReturnValuesLogicReturnsCorrectMap()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Match().MapTo<(int x, int y)>()
                             .Single().Do(x => (x, x))
                             .RecursiveCons().Do((x, y, soFar) => soFar.Cons((x, x + y)))
                             .Result();

            var consResult = result.ToConsEnumerable();
            var (one, (two, (three, _))) = consResult;
            Assert.AreEqual((1, 3), one);
            Assert.AreEqual((2, 5), two);
            Assert.AreEqual((3, 3), three);
        }

        [Test]
        public void MultipleList_CorrectlyMapsToNewCollectionUsingWhereClause()
        {
            var list = new List<int> { 1, 1, 2, 3, 3, 3, 1, 2, 2, 2, 2 };
            var result = list.Match().MapTo<(int value, int count)>()
                             .Single().Do(x => (x, 1))
                             .RecursiveCons().Where((current, last) => current == last)
                                             .Do((x, _, soFar) => soFar.ReplaceHead((x, soFar.Head().count + 1)))
                             .RecursiveCons().Do((x, _, soFar) => soFar.Cons((x, 1)))
                             .Result()
                             .ToList();
            Assert.Multiple(() =>
            {
                Assert.That(result[0], Is.EqualTo((1, 2)));
            });
            Assert.AreEqual((1, 2), result[0]);
            Assert.AreEqual((2, 1), result[1]);
            Assert.AreEqual((3, 3), result[2]);
            Assert.AreEqual((1, 1), result[3]);
            Assert.AreEqual((2, 4), result[4]);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void MultipleListWithNoSingleClause_ThrowsCorrectException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<int>()
                                                      .RecursiveCons().Do((x, _, soFar) => soFar.Cons(x))
                                                      .Result());
        }

        [Test]
        public void MultipleListWithNoConsClause_ThrowsCorrectException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<int>()
                                                      .Single().Do(x => x)
                                                      .Result());
        }

        [Test]
        public void MultipleListWithNoMatchingConsClause_ThrowsCorrectException()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.Throws<NoMatchException>(() => list.Match().MapTo<int>()
                                                      .Single().Do(x => x)
                                                      .RecursiveCons().Where((x, y) => false).Do((x, y, z) => null)
                                                      .Result());
        }
    }
}
