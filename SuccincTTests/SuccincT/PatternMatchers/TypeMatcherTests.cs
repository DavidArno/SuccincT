using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.PatternMatchers.GeneralMatcher;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class TypeMatcherTests
    {
        [Test]
        public void Test1Type_CanBeTypeMatchedUsingITest()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(t => t.F2())
                             .CaseOf<Test2>().Do(t => t.F2())
                             .CaseOf<Test1>().Do(t => t.F1())
                             .Result();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test1Type_CanBeTypeMatchedUsingITestAndDoValue()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(3)
                             .CaseOf<Test2>().Do(2)
                             .CaseOf<Test1>().Do(1)
                             .Result();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test2Type_CanBeTypeMatchedUsingITest()
        {
            ITest test = new Test2();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(t => t.F2())
                             .CaseOf<Test2>().Do(t => t.F2())
                             .CaseOf<Test1>().Do(t => t.F1())
                             .Result();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test2Type_CanBeTypeMatchedUsingITestAndDoValue()
        {
            ITest test = new Test2();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(3)
                             .CaseOf<Test2>().Do(2)
                             .CaseOf<Test1>().Do(1)
                             .Result();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test3Type_CanBeTypeMatchedUsingITest()
        {
            ITest test = new Test3();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(t => t.F2())
                             .CaseOf<Test2>().Do(t => t.F2())
                             .CaseOf<Test1>().Do(t => t.F1())
                             .Result();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test3Type_CanBeTypeMatchedUsingITestAndDoValue()
        {
            ITest test = new Test3();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(3)
                             .CaseOf<Test2>().Do(2)
                             .CaseOf<Test1>().Do(1)
                             .Result();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void TypeTesting_CanBeUsedToReturnADifferentTypeToThatOfTheMethodUsed()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<string>()
                             .CaseOf<Test3>().Do(t => t.F2().ToString())
                             .CaseOf<Test2>().Do(t => t.F2().ToString())
                             .CaseOf<Test1>().Do(t => t.F1().ToString())
                             .Result();

            Assert.AreEqual("1", result);
        }

        [Test]
        public void WhenNoTypeMatches_ElseValueIsUsed()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(t => t.F2())
                             .CaseOf<Test2>().Do(t => t.F2())
                             .Else(0)
                             .Result();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenNoTypeMatches_ElseExpressionIsUsed()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Do(t => t.F2())
                             .CaseOf<Test2>().Do(t => t.F2())
                             .Else(t => 0)
                             .Result();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenNoTypeMatchesAndNoElseSpecified_NoMatchExceptionsIsThrown()
        {
            ITest test = new Test1();
            Assert.Catch<NoMatchException>(() => test.TypeMatch().To<int>()
                                                     .CaseOf<Test3>().Do(t => t.F2())
                                                     .CaseOf<Test2>().Do(t => t.F2())
                                                     .Result());
        }

        [Test]
        public void Test1Type_CanBeTypeMatchedUsingWhereAndDoExpressions()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test1>().Where(t => t.F1() == 2).Do(t => 2)
                             .CaseOf<Test1>().Where(t => t.F1() == 1).Do(t => 1)
                             .Result();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test1Type_CanBeTypeMatchedUsingWhereExpressionAndDoValue()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test1>().Where(t => t.F1() == 2).Do(2)
                             .CaseOf<Test1>().Where(t => t.F1() == 1).Do(1)
                             .Result();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test2Type_CanBeTypeMatchedUsingWhereAndDoExpressions()
        {
            ITest test = new Test2();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test2>().Where(t => t.F2() == 2).Do(t => 2)
                             .CaseOf<Test2>().Where(t => t.F2() == 1).Do(t => 1)
                             .Result();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test2Type_CanBeTypeMatchedUsingWhereExpressionAndDoValue()
        {
            ITest test = new Test2();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test2>().Where(t => t.F2() == 2).Do(2)
                             .CaseOf<Test2>().Where(t => t.F2() == 1).Do(1)
                             .Result();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test3Type_CanBeTypeMatchedUsingWhereAndDoExpressions()
        {
            ITest test = new Test3();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Where(t => t.F2() == 2).Do(t => 2)
                             .CaseOf<Test3>().Where(t => t.F2() == 3).Do(t => 3)
                             .Result();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test3Type_CanBeTypeMatchedUsingWhereExpressionAndDoValue()
        {
            ITest test = new Test3();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test3>().Where(t => t.F2() == 2).Do(2)
                             .CaseOf<Test3>().Where(t => t.F2() == 3).Do(3)
                             .Result();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenNoWhereExpressionsAndNoElseSpecified_NoMatchExceptionsIsThrown()
        {
            ITest test = new Test1();
            Assert.Catch<NoMatchException>(() => test.TypeMatch().To<int>()
                                                     .CaseOf<Test1>().Where(_ => false).Do(t => t.F1())
                                                     .Result());
        }

        [Test]
        public void WhenNoWhereExpressionsMatch_ElseExpressionIsUsed()
        {
            ITest test = new Test1();
            var result = test.TypeMatch().To<int>()
                             .CaseOf<Test1>().Where(_ => false).Do(t => 1)
                             .Else(t => 0)
                             .Result();

            Assert.AreEqual(0, result);
        }

        private interface ITest {}

        private class Test1 : ITest
        {
            public int F1() => 1;
        }

        private class Test2 : ITest
        {
            public int F2() => 2;
        }

        private class Test3 : ITest
        {
            public int F2() => 3;
        }
    }
}
