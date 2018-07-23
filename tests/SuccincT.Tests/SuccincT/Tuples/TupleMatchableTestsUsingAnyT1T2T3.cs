using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleMatchableTestsUsingAnyT1T2T3
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
        public void Tuple_CanBeMatchedUsingAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(any, "a", Colors.Red).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, any, Colors.Red).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", any).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntAndString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(__, __, Colors.Red).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntAndColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(__, "a", __).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyStringAndColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, __, __).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingThreeAnyValues()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(__, __, __).Do((x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(__, "a", Colors.Red).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, __, Colors.Red).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, "a", __).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyIntAndString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(__, __, Colors.Red).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyAndColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(__, "a", __).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyStringAndColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, __, __).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingThreeAnyValues()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(__, __, __).Do((x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyInt_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(__, "a", Colors.Green).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyString_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, __, Colors.Green).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyColor_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(2, "a", __).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefinedAndAnyIntUsed_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result =
                tuple.Match().To<bool>().With(__, "b", Colors.Blue).Or(2, "a", Colors.Green).Do((x, y, z) => false)
                     .Where((x, y, z) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefinedAndAnyColor_WithCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue).Or(1, "a", __).Do(false)
                                                 .Where((x, y, z) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}