using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleTestsT1T2T3
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOr()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, "a", Colors.Red).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>()
                           .With(2, "a", Colors.Green).Or(1, "b", Colors.Blue).Do((x, y, z) => x).Result());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Green).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().Where((x, y, z) => x == 1 && y == "a" && z == Colors.Red).Do(true)
                                                 .Else((x, y, z) => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().Where((x, y, z) => z == Colors.Green).Do((x, y, z) => true)
                                                 .Else((x, y, z) => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().Where((x, y, z) => x == 2).Do((x, y, z) => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result =
                tuple.Match().To<bool>().With(1, "b", Colors.Blue).Or(2, "a", Colors.Green).Do((x, y, z) => false)
                     .Where((x, y, z) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red).Or(1, "a", Colors.Blue).Do(false)
                                                 .Where((x, y, z) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}