using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsUsingAnyT1T2
    {
        [Test]
        public void TupleWithAnyString_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(1, __).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyInt_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(any, "a").Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyString_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(1, any).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyAndAny_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(__, __).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleAnyInt_WithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(any, "a").Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleAnyString_WithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(1, any).Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleAnyAndAny_WithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(__, __).Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }
    }
}