using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleTestsT1
    {
        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = Tuple.Create(1);
            var result = tuple.Match().To<bool>().With(1).Do(_ => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOr()
        {
            var tuple = Tuple.Create("a");
            var result = tuple.Match().To<bool>().With("b").Or("a").Do(_ => true).Result();
            Assert.IsTrue(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = Tuple.Create(1);
            tuple.Match().To<int>().With(2).Or(3).Do(x => x).Result();
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = Tuple.Create("a");
            var result = tuple.Match().To<bool>().With("b").Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = Tuple.Create(1);
            var result = tuple.Match().To<bool>().Where(x => x == 1).Do(true)
                                                 .Else(_ => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = Tuple.Create("a");
            var result = tuple.Match().To<bool>().Where(x => x == "b").Do(_ => true)
                                                 .Else(_ => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = Tuple.Create(1);
            var result = tuple.Match().To<bool>().Where(x => x == 2).Do(_ => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = Tuple.Create("a");
            var result = tuple.Match().To<bool>().With("b").Or("c").Do(_ => false)
                                                 .Where(x => x == "a").Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = Tuple.Create("a");
            var result = tuple.Match().To<bool>().With("a").Or("b").Do(false)
                                                 .Where(x => x == "c").Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}