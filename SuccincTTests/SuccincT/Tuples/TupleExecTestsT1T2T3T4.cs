using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsT1T2T3T4
    {
        private enum Colors { Red, Green, Blue }
        private enum Animals { Cow, Pig, Goat }

        [Test]
        public void Tuple_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Red, Animals.Cow).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(1, "a", Colors.Green, Animals.Cow).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test, ExpectedException(typeof(NoMatchException))]
        public void Tuple_ExceptionIfNoMatchWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Blue, Animals.Cow);
            tuple.Match().With(1, "a", Colors.Blue, Animals.Goat)
                         .Or(1, "a", Colors.Blue, Animals.Pig).Do((w, x, y, z) => { }).Exec();
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Red, Animals.Goat).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhereWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().Where((w, x, y, z) => w == 1 && x == "a" && y == Colors.Red && z == Animals.Cow)
                         .Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().Where((w, x, y, z) => z == Animals.Goat).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Red, Animals.Goat)
                         .Or(1, "a", Colors.Red, Animals.Pig).Do((w, x, y, z) => result = false)
                         .Where((w, x, y, z) => w == 1 && x == "a" && y == Colors.Red && z == Animals.Cow)
                         .Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match()
                 .Where((w, x, y, z) => z == Animals.Pig).Do((w, x, y, z) => result = true)
                 .With(1, "a", Colors.Red, Animals.Cow).Or(1, "a", Colors.Red, Animals.Goat)
                 .Do((w, x, y, z) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }
    }
}