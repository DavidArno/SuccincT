using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleMatchableTestsT1T2T3T4
    {
        private enum Colors { Red, Green, Blue }
        private enum Animals { Cow, Pig, Goat }

        private class TestClass : ITupleMatchable<int, string, Colors, Animals>
        {
            public int A;
            public string B;
            public Colors C;
            public Animals D;
            public Tuple<int, string, Colors, Animals> PropertiesToMatch => Tuple.Create(A, B, C, D);
        }

        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Goat };
            var result = tuple.Match().To<bool>()
                              .With(1, "a", Colors.Red, Animals.Goat).Do((w, x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOr()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue, Animals.Goat)
                                                 .Or(1, "a", Colors.Blue, Animals.Cow).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>().With(1, "a", Colors.Red, Animals.Goat)
                           .Or(1, "b", Colors.Red, Animals.Pig).Do((w, x, y, z) => w).Result());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red, Animals.Pig).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            var result = tuple.Match().To<bool>().Where((w, x, y, z) => z == Animals.Cow).Do(true)
                                                 .Else((w, x, y, z) => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            var result = tuple.Match().To<bool>().Where((w, x, y, z) => z == Animals.Goat).Do((w, x, y, z) => true)
                                                 .Else((w, x, y, z) => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            var result = tuple.Match().To<bool>().Where((w, x, y, z) => z == Animals.Pig).Do((w, x, y, z) => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Cow };
            var result =
                tuple.Match().To<bool>().With(1, "a", Colors.Red, Animals.Goat)
                .Or(2, "a", Colors.Green, Animals.Pig).Do((w, x, y, z) => false)
                     .Where((w, x, y, z) => z == Animals.Cow).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Red, Animals.Cow)
                                                 .Or(1, "a", Colors.Red, Animals.Goat).Do(false)
                                                 .Where((w, x, y, z) => z == Animals.Pig).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}