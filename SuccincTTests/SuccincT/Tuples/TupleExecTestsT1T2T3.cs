using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsT1T2T3
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void Tuple_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(1, "a", Colors.Red).Do((x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_ExceptionIfNoMatchWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            Assert.Throws<NoMatchException>(
                () => tuple.Match().With(1, "a", Colors.Green).Or(1, "a", Colors.Blue).Do((x, y, z) => { }).Exec());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green).Do((x, y, z) => result = true)
                         .Else((x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhereWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().Where((x, y, z) => x == 1 && y == "a" && z == Colors.Red).Do((x, y, z) => result = true)
                         .Else((x, y, z) => result = false).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().Where((x, y, z) => z == Colors.Green).Do((x, y, z) => result = true)
                         .Else((x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue).Or(1, "a", Colors.Green).Do((x, y, z) => result = false)
                         .Where((x, y, z) => x == 1 && y == "a" && z == Colors.Red).Do((x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red);
            var result = false;
            tuple.Match()
                 .Where((x, y, z) => z == Colors.Blue).Do((x, y, z) => result = true)
                 .With(1, "a", Colors.Green).Or(1, "a", Colors.Red).Do((x, y, z) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }
    }
}