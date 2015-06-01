using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class UnionTestsT1T2
    {
        private readonly UnionT1T2ComplexMatchersTests _unionT1T2ComplexMatchersTests = new UnionT1T2ComplexMatchersTests();

        [Test]
        public void UnionWithT1_HasVariantCase1()
        {
            var union = new Union<int, string>(2);
            Assert.AreEqual(Variant.Case1, union.Case);
        }

        [Test]
        public void UnionWithT2_HasVariantCase2()
        {
            var union = new Union<int, string>("test");
            Assert.AreEqual(Variant.Case2, union.Case);
        }

        [Test]
        public void UnionWithT1_HasCase1Value()
        {
            var union = new Union<int, string>(2);
            Assert.AreEqual(2, union.Case1);
        }

        [Test]
        public void UnionWithT2_HasCase2Value()
        {
            var union = new Union<int, string>("Test");
            Assert.AreEqual("Test", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string>(2);
            Assert.AreEqual("", union.Case2);
        }

        [Test, ExpectedException(ExpectedException = typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string>("Test");
            Assert.AreEqual(1, union.Case1);
        }

        [Test]
        public void UnionWithT1_MatchesBasicCase1Correctly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<bool>().Case1().Do(x => false).Case2().Do(x => true).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void UnionWithT1_MatchesBasicCase1CorrectlyWithExec()
        {
            var union = new Union<int, string>(2);
            var result = 0;
            union.Match().Case1().Do(x => result = x).Case2().Do(x => result = 3).Exec();
            Assert.AreEqual(2, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT1AndNoCase1Match_ThrowsException()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<bool>().Case2().Do(x => true).Result();
            Assert.IsFalse(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT1AndNoCase1MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string>(2);
            union.Match().Case2().Do(x => { }).Exec();
        }

        [Test]
        public void UnionWithT2_MatchesBasicCase2Correctly()
        {
            var union = new Union<int, string>("la la");
            var result = union.Match<bool>().Case1().Do(x => false).Case2().Do(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_MatchesBasicCase2CorrectlyWithExec()
        {
            var union = new Union<int, string>("la la");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case2().Do(x => result = 2).Exec();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2Match()
        {
            var union = new Union<int, string>("fred");
            var result = union.Match<bool>().Case1().Do(x => false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2MatchWithExec()
        {
            var union = new Union<int, string>("fred");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Else(x => result = 2).Exec();
            Assert.AreEqual(2, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2Match_ThrowsException()
        {
            var union = new Union<int, string>("la la");
            var result = union.Match<bool>().Case1().Do(x => false).Result();
            Assert.IsFalse(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string>("la la");
            union.Match().Case1().Do(x => { }).Exec();
        }
    }
}
