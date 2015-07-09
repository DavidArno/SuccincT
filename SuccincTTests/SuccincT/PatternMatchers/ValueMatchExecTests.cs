using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.PatternMatchers.GeneralMatcher;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class ValueMatchExecTests
    {
        [Test]
        public void IntValue_CanBeMatchedWithExec()
        {
            var result = false;
            1.Match().With(1).Do(x => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_CanBeMatchedViaOrWithExec()
        {
            var result = false;
            1.Match().With(2).Or(1).Do(x => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void IntValue_ExceptionIfNoMatchWithExec()
        {
            3.Match().With(2).Or(1).Do(x => { }).Exec();
        }

        [Test]
        public void IntValue_WhenNoMatchElseUsedWithExec()
        {
            var result = false;
            1.Match().With(2).Do(x => result = true).Else(x => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_CanBeMatchedViaWhereWithExec()
        {
            var result = false;
            1.Match().Where(x => x == 1).Do(x => result = true).Else(x => result = false).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_WhenNoMatchViaWhereElseUsedWithExec()
        {
            var result = false;
            1.Match().Where(x => x == 2).Do(x => result = true).Else(x => result = false).Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void IntValue_WithAndWhereDefinedWhereCorrectlyUsedWithExec()
        {
            var result = false;
            5.Match().With(1).Or(2).Do(x => result = false)
                     .Where(x => x == 5).Do(x => result = true).Exec();
            Assert.IsTrue(result);
        }

        [Test]
        public void IntValue_WithAndWhereDefinedWithCorrectlyUsedWithExec()
        {
            var result = false;
            2.Match().With(1).Or(2).Do(x => result = false)
                     .Where(x => x == 5).Do(x => result = true).Exec();
            Assert.IsFalse(result);
        }
    }
}