using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsUsingAnyT1T2T3T4
    {
        private enum Colors { Red, Green, Blue }
        private enum Animals { Cow, Pig, Goat }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyIntWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(any, "a", Colors.Red, Animals.Cow).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, any, Colors.Red, Animals.Cow).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", any, Animals.Cow).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAnyAnimalWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Red, any).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingFourAnyValuesWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(any, any, any, any).Do((w, x, y, z) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyIntWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(__, "a", Colors.Green, Animals.Cow).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyStringWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(1, __, Colors.Green, Animals.Cow).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyColorWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(1, "a", __, Animals.Cow).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingAnyAnimalWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(1, "a", Colors.Green, __).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrUsingFourAnyValuesWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Green, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Green, Animals.Goat)
                         .Or(__, __, __, __).Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyInt_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(__, "a", Colors.Red, Animals.Goat).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyString_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, __, Colors.Red, Animals.Goat).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyColor_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", __, Animals.Goat).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchDespiteAnyAnimal_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(1, "a", Colors.Blue, __).Do((w, x, y, z) => result = true)
                         .Else((w, x, y, z) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefinedUsingAnyInt_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match().With(__, "a", Colors.Red, Animals.Goat)
                         .Or(__, "a", Colors.Red, Animals.Pig).Do((w, x, y, z) => result = false)
                         .Where((w, x, y, z) => w == 1 && x == "a" && y == Colors.Red && z == Animals.Cow)
                         .Do((w, x, y, z) => result = true)
                         .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefinedUsingFourAnyValues_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a", Colors.Red, Animals.Cow);
            var result = false;
            tuple.Match()
                 .Where((w, x, y, z) => z == Animals.Pig).Do((w, x, y, z) => result = true)
                 .With(1, "a", Colors.Red, Animals.Goat).Or(__, __, __, __)
                 .Do((w, x, y, z) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }
    }
}