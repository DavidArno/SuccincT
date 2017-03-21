﻿using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.PatternMatchers.GeneralMatcher;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class ValueMatchTests
    {
        [Test]
        public void IntValue_CanBeMatched()
        {
            var result = 1.Match().To<bool>().With(1).Do(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_CanBeMatchedViaOr()
        {
            var result = 1.Match().To<bool>().With(2).Or(1).Do(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_ExceptionIfNoMatch() =>
            Assert.Throws<NoMatchException>(() => 3.Match().To<int>().With(2).Or(1).Do(x => x).Result());

        [Test]
        public void IntValue_WhenNoMatchElseUsed()
        {
            var result = 1.Match().To<bool>().With(2).Do(x => true).Else(x => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_CanBeMatchedViaWhere()
        {
            var result = 1.Match().To<bool>().Where(x => x == 1).Do(x => true)
                                             .Else(x => false).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_WhenNoMatchViaWhereElseLambdaUsed()
        {
            var result = 1.Match().To<bool>().Where(x => x == 2).Do(x => true)
                                             .Else(x => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_WhenNoMatchViaWhereElseExpressionUsed()
        {
            var result = 1.Match().To<bool>().Where(x => x == 2).Do(x => true)
                                             .Else(false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_WithAndWhereDefinedWhereCorrectlyUsed()
        {
            var result = 5.Match().To<bool>().With(1).Or(2).Do(x => false)
                                            .Where(x => x == 5).Do(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_WithAndWhereDefinedWithCorrectlyUsed()
        {
            var result = 2.Match().To<bool>().With(1).Or(2).Do(x => false)
                                             .Where(x => x == 5).Do(x => true).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_WithDoExpressionHandledCorrectly()
        {
            var result = 2.Match().To<bool>().With(1).Or(2).Do(false)
                                             .Where(x => x == 5).Do(x => true).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_WhereDoExpressionHandledCorrectly()
        {
            var result = 3.Match().To<bool>().With(1).Or(2).Do(x => false)
                                             .Where(x => x > 2).Do(true).Result();
            Assert.IsTrue(result);
        }
    }
}