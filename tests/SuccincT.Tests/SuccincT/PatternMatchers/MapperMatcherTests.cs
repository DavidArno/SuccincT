using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using System;
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
    }
}
