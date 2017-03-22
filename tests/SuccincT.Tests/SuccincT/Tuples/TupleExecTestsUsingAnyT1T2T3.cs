using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsUsingAnyT1T2T3
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(any, "a", Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, any, Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", any).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntAndStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(any, any, Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntAndColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(__, "a", __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyStringAndColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, __, __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingThreeAnysWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(__, __, __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyIntWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(__, "a", Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(1, __, Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(1, "a", __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyIntAndStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(__, __, Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyIntAndColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(__, "a", __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyStringAndColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(1, __, __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingThreeAnysWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(__, __, __).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithUsingIntAnyAndWhereDefined_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(__, "a", Colors.Blue).Or(__, "a", Colors.Green).Do((x, y, z) => result = false)
                         .Where((x, y, z) => x == 1 && y == "a" && z == Colors.Red).Do((x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }
    }
}