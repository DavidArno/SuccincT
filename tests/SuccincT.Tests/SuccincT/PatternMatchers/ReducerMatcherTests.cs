using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class ReducerMatcherTests
    {
        [Test]
        public void EmptyList_ResultsInOneElementEnumerationWhenEmptyClauseUsed()
        {
            var list = new List<int>();
            var result = list.Match().ReduceTo<(int, int)>()
                             .Empty().Do((0, 0))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((0, 0), head);
        }

        [Test]
        public void SingleList_ResultsInOneElementEnumerationWhenValueSingleClauseUsed()
        {
            var list = new List<int> { 1 };
            var result = list.Match().ReduceTo<(int, int)>()
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
            var result = list.Match().ReduceTo<(int, int)>()
                             .Single().Do(x => (x, 0))
                             .Result();

            var consResult = result.ToConsEnumerable();
            Assert.AreEqual(1, consResult.Count());
            var (head, _) = consResult;
            Assert.AreEqual((1, 0), head);
        }
    }
}
