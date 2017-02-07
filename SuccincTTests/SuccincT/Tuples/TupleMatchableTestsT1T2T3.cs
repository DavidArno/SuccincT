using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleMatchebleTestsT1T2T3
    {
        private enum Colors { Red, Green, Blue }

        private class TestClass : ITupleMatchable<int, string, Colors>
        {
            public int A;
            public string B;
            public Colors C;
            public (int, string, Colors) PropertiesToMatch => (A, B, C);
        }

        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOr()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, "a", Colors.Red).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>()
                           .With(2, "a", Colors.Green).Or(1, "b", Colors.Blue).Do((x, y, z) => x).Result());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Green).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().Where((x, y, z) => x == 1 && y == "a" && z == Colors.Red).Do(true)
                                                 .Else((x, y, z) => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().Where((x, y, z) => z == Colors.Green).Do((x, y, z) => true)
                                                 .Else((x, y, z) => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().Where((x, y, z) => x == 2).Do((x, y, z) => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result =
                tuple.Match().To<bool>().With(1, "b", Colors.Blue).Or(2, "a", Colors.Green).Do((x, y, z) => false)
                     .Where((x, y, z) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red).Or(1, "a", Colors.Blue).Do(false)
                                                 .Where((x, y, z) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}