using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleMatchableTestsUsingAnyT1T2
    {
        private class TestClass : ITupleMatchable<int, string>
        {
            public int A;
            public string B;

            public (int, string) PropertiesToMatch => (A, B);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(__, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(1, __).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedWithTwoAnys()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(__, __).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(any, "b").Or(any, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, __).Or(1, __).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithTwoAnys()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, __).Or(any, any).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatchDespiteAnyInt_ThrowsException()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            _ = Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>().With(__, "b").Or(__, "c").Do((x, y) => x).Result());
        }

        [Test]
        public void TupleNoMatchDespiteAnyString_ThrowsException()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            _ = Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>().With(2, __).Or(3, __).Do((x, y) => x).Result());
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyInt_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(__, "b").Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyString_ElseUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, __).Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithUsingAnyIntAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(any, "b").Or(any, "c").Do((x, y) => false)
                                                 .Where((x, y) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithUsingStringAnyAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(2, __).Or(3, __).Do((x, y) => false)
                                                 .Where((x, y) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithUsingIntAnyAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = new TestClass { A = 1, B = "a" };
            var result = tuple.Match().To<bool>().With(__, "a").Or(__, "b").Do(false)
                                                 .Where((x, y) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}