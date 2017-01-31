using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsT1T2
    {
        [Test]
        public void Tuple_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(1, "a").Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAnyString_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(1, _).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(1, "a").Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyInt_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(_, "a").Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyString_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(1, _).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWIthAnyAndAny_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Or(_, _).Do((x, y) => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_ExceptionIfNoMatchWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            Assert.Throws<NoMatchException>(() => tuple.Match().With(2, "a").Or(1, "b").Do((x, y) => { }).Exec());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().With(2, "a").Do((x, y) => result = true).Else((x, y) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhereWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().Where((x, y) => x == 1 && y == "a").Do((x, y) => result = true)
                         .Else((x, y) => result = false).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match().Where((x, y) => x == 2).Do((x, y) => result = true).Else((x, y) => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .With(1, "b").Or(2, "a").Do((x, y) => result = false)
                 .Where((x, y) => x == 1 && y == "a").Do((x, y) => result = true)
                 .Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(1, "a").Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleAnyInt_WithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(_, "a").Or(2, "a").Do((x, y) => result = false)
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
                 .With(1, _).Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleAnyANdAny_WithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1, "a");
            var result = false;
            tuple.Match()
                 .Where((x, y) => x == 3 && y == "a").Do((x, y) => result = true)
                 .With(_, _).Or(2, "a").Do((x, y) => result = false)
                 .Exec();
            Assert.IsFalse(result);
        }
    }
}