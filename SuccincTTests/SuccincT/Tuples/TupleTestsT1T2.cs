﻿using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static SuccincT.PatternMatchers.Any;

namespace SuccincTTests.SuccincT.Tuples
{
    [TestFixture]
    internal class TupleTestsT1T2
    {
        [Test]
        public void Tuple_CanBeMatched()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(1, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingIntWildcard()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(_, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingAllWildcards()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(_, _).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingOrWithIntWildcard()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(2, "a").Or(_, "a").Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingOrWIthStringWildcard()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(1, "b").Or(1, _).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void Tuple_CanBeMatchedUsingOrWithAllWildcards()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(2, "b").Or(_, _).Do((x, y) => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleNoMatch_ThrowsException()
        {
            var tuple = Tuple.Create(1, "a");
            Assert.Throws<NoMatchException>(
                () => tuple.Match().To<int>().With(2, "a").Or(1, "b").Do((x, y) => x).Result());
        }

        [Test]
        public void TupleWhenNoMatch_ElseUsed()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(2, "a").Do(true).Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void Tuple_CanBeMatchedViaWhere()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().Where((x, y) => x == 1 && y == "a").Do(true)
                                                 .Else((x, y) => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseLambdaUsed()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().Where((x, y) => x == 2).Do((x, y) => true)
                                                 .Else((x, y) => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWhenNoMatchViaWhere_ElseExpressionUsed()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().Where((x, y) => x == 2).Do((x, y) => true)
                                                 .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WhereCorrectlyUsed()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(1, "b").Or(2, "a").Do((x, y) => false)
                                                 .Where((x, y) => x == 1).Do(true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void TupleWithAndWhereDefined_WithCorrectlyUsed()
        {
            var tuple = Tuple.Create(1, "a");
            var result = tuple.Match().To<bool>().With(1, "a").Or(1, "a").Do(false)
                                                 .Where((x, y) => x == 5).Do(true).Result();
            Assert.IsFalse(result);
        }
    }
}