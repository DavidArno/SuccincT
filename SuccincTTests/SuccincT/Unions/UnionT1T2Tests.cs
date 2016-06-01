using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public sealed class UnionT1T2Tests
    {
        [Test]
        public void UnionWithT1_HasVariantCase1()
        {
            var union = new Union<int, string>(2);
            AreEqual(Variant.Case1, union.Case);
        }

        [Test]
        public void UnionWithT2_HasVariantCase2()
        {
            var union = new Union<int, string>("test");
            AreEqual(Variant.Case2, union.Case);
        }

        [Test]
        public void UnionWithT1_HasCase1Value()
        {
            var union = new Union<int, string>(2);
            AreEqual(2, union.Case1);
        }

        [Test]
        public void UnionWithT2_HasCase2Value()
        {
            var union = new Union<int, string>("Test");
            AreEqual("Test", union.Case2);
        }

        [Test, ExpectedException(typeof(InvalidCaseException))]
        public void AccessingCase2ForUnionWithT1_CausesException()
        {
            var union = new Union<int, string>(2);
            AreEqual("", union.Case2);
        }

        [Test]
        public void AccessingCase2ForUnionWithT1_GivesMeaningfulException()
        {
            var union = new Union<int, string>(2);
            try
            {
                Unit.Ignore(union.Case2);
                Fail("Expected exception to be thrown");
            }
            catch (InvalidCaseException e)
            {
                AreEqual($"Cannot access union case {Variant.Case2} when case {Variant.Case1} is selected one.",
                    e.Message);
            }
        }

        [Test, ExpectedException(typeof(InvalidCaseException))]
        public void AccessingCase1ForUnionWithT2_CausesException()
        {
            var union = new Union<int, string>("Test");
            AreEqual(1, union.Case1);
        }

        [Test]
        public void AccessingCase1ForUnionWithT2_GivesMeaningfulException()
        {
            var union = new Union<int, string>("x");
            try
            {
                Ignore(union.Case1);
                Fail("Expected exception to be thrown");
            }
            catch (InvalidCaseException e)
            {
                AreEqual($"Cannot access union case {Variant.Case1} when case {Variant.Case2} is selected one.",
                    e.Message);
            }
        }

        [Test]
        public void UnionWithT1_MatchesBasicCase1Correctly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<bool>().Case1().Do(x => false).Case2().Do(x => true).Result();
            IsFalse(result);
        }

        [Test]
        public void UnionWithT1_MatchesBasicCase1CorrectlyWithExec()
        {
            var union = new Union<int, string>(2);
            var result = 0;
            union.Match().Case1().Do(x => result = x).Case2().Do(x => result = 3).Exec();
            AreEqual(2, result);
        }

        [Test, ExpectedException(typeof(NoMatchException))]
        public void UnionWithT1AndNoCase1Match_ThrowsException()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<bool>().Case2().Do(x => true).Result();
            IsFalse(result);
        }

        [Test, ExpectedException(typeof(NoMatchException))]
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
            IsTrue(result);
        }

        [Test]
        public void UnionWithT2_MatchesBasicCase2CorrectlyWithExec()
        {
            var union = new Union<int, string>("la la");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case2().Do(x => result = 2).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2Match()
        {
            var union = new Union<int, string>("fred");
            var result = union.Match<bool>().Case1().Do(x => false).Else(x => true).Result();
            IsTrue(result);
        }

        [Test]
        public void UnionWithT1_UsesElseIfNoCase1MatchWithExec()
        {
            var union = new Union<int, string>("fred");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Else(x => result = 2).Exec();
            AreEqual(2, result);
        }

        [Test, ExpectedException(typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2Match_ThrowsException()
        {
            var union = new Union<int, string>("la la");
            var result = union.Match<bool>().Case1().Do(x => false).Result();
            IsFalse(result);
        }

        [Test, ExpectedException(typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string>("la la");
            union.Match().Case1().Do(x => { }).Exec();
        }

        [Test]
        public void UnionWithT1_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_CaseOfExpressionSupported()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>().Case1().Of(2).Do(0).Case1().Do(1).Case2().Do(2).Result();
            AreEqual(0, result);
        }

        [Test]
        public void UnionWithT1_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string>(3);
            var result = union.Match<int>().Case1().Of(2).Do(0).Case1().Where(x => x == 3).Do(1).Case2().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_SimpleCaseExpressionSupported()
        {
            var union = new Union<int, string>("2");
            var result = union.Match<int>().Case1().Do(1).Case2().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseOfExpressionSupported()
        {
            var union = new Union<int, string>("1");
            var result = union.Match<int>().Case1().Do(0).Case2().Of("1").Do(2).Case2().Do(1).Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_CaseWhereExpressionSupported()
        {
            var union = new Union<int, string>("2");
            var result = union.Match<int>()
                              .Case1().Do(0).Case2().Where(x => x == "2").Do(2).Case2().Of("1").Do(1).Result();
            AreEqual(2, result);
        }
    }
}