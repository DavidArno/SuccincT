using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleMatchableTestsUsingAnyT1T2T3T4
    {
        private enum Colors { Red, Green, Blue }
        private enum Animals { Cow, Pig, Goat }

        private class TestClass : ITupleMatchable<int, string, Colors, Animals>
        {
            public int A;
            public string B;
            public Colors C;
            public Animals D;

            public (int, string, Colors, Animals) PropertiesToMatch => (A, B, C, D);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Goat };
            var result = tuple.Match().To<bool>()
                              .With(any, "a", Colors.Red, Animals.Goat).Do((w, x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Goat };
            var result = tuple.Match().To<bool>()
                              .With(1, any, Colors.Red, Animals.Goat).Do((w, x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Goat };
            var result = tuple.Match().To<bool>()
                              .With(1, "a", any, Animals.Goat).Do((w, x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedWithAnyAnimal()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Red, D = Animals.Goat };
            var result = tuple.Match().To<bool>()
                              .With(1, "a", Colors.Red, any).Do((w, x, y, z) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyInt()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue, Animals.Goat)
                                                 .Or(__, "a", Colors.Blue, Animals.Cow).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyString()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue, Animals.Goat)
                                                 .Or(1, __, Colors.Blue, Animals.Cow).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyColor()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue, Animals.Goat)
                                                 .Or(1, "a", __, Animals.Cow).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithAnyAnimal()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Blue, Animals.Goat)
                                                 .Or(1, "a", Colors.Blue, __).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithFourAnyValues()
        {
            var tuple = new TestClass { A = 1, B = "a", C = Colors.Blue, D = Animals.Cow };
            var result = tuple.Match().To<bool>().With(1, "a", Colors.Green, Animals.Pig)
                                                 .Or(__, __, __, __).Do((w, x, y, z) => true)
                                      .Result();
            Assert.IsTrue(result);
        }
    }
}