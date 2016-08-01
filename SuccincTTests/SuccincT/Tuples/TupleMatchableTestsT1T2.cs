using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleMatchableTestsT1T2
    {
        private class TestClass : ITupleMatchable<int, string>
        {
            public int A;
            public string B;

            public Tuple<int, string> PropertiesToMatch => Tuple.Create(A, B);
        }

        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(1, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOr()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, "a").Or(1, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>().With(2, "a").Or(1, "b").Do((x, y) => x).Result());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, "a").Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().Where((x, y) => x == 1 && y == "a").Do(true)
                                                 .Else((x, y) => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().Where((x, y) => x == 2).Do((x, y) => true)
                                                 .Else((x, y) => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().Where((x, y) => x == 2).Do((x, y) => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(1, "b").Or(2, "a").Do((x, y) => false)
                                                 .Where((x, y) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(1, "a").Or(1, "a").Do(false)
                                                 .Where((x, y) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}