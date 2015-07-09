using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    public class TupleExecTestsT1
    {
        [Test]
        public void Tuple_CanBeMatchedWithExec()
        {
            var tuple = Tuple.Create(1);
            var result = false;
            tuple.Match().With(1).Do(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaOrWithExec()
        {
            var tuple = Tuple.Create("a");
            var result = false;
            tuple.Match().With("b").Or("a").Do(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void Tuple_ExceptionIfNoMatchWithExec()
        {
            var tuple = Tuple.Create(1);
            tuple.Match().With(2).Or(3).Do(_ => { }).Exec();
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsedWithExec()
        {
            var tuple = Tuple.Create("a");
            var result = false;
            tuple.Match().With("b").Do(_ => result = true).Else(_ => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhereWithExec()
        {
            var tuple = Tuple.Create(1);
            var result = false;
            tuple.Match().Where(x => x == 1).Do(_ => result = true)
                         .Else(_ => result = false).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseUsedWithExec()
        {
            var tuple = Tuple.Create("a");
            var result = false;
            tuple.Match().Where(x => x == "b").Do(_ => result = true)
                         .Else(_ => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create(1);
            var result = false;
            tuple.Match().With(2).Or(3).Do(_ => result = false)
                         .Where(x => x == 1).Do(_ => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsedWithExec()
        {
            var tuple = Tuple.Create("a");
            var result = false;
            tuple.Match().With("a").Or("b").Do(_ => result = false)
                         .Where(x => x == "c").Do(_ => result = true).Exec();
            Assert.IsFalse(result);
        }
    }
}