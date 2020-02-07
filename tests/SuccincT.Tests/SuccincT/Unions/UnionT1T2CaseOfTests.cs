using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2CaseOfTests
    {
        [Test]
        public void UnionWithT1_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string>(2);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string>("red");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1WhenCaseOfDoesntMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string>(99);
            var result = union.Match<int>()
                              .CaseOf<int>().Of(1).Do(1)
                              .CaseOf<string>().Do(2)
                              .Else(3)
                              .Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2WhenCaseOfDoesntMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string>("blue");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Of("red").Do(2)
                              .Else(3)
                              .Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionT1T2WithInvalidTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match<int>()
                                                          .CaseOf<int>().Do(1)
                                                          .CaseOf<float>().Do(2)
                                                          .Result());
        }

        [Test]
        public void UnionWithT1_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string>(2);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string>("yellow");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT1WhenExecCaseOfDoesntMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string>(99);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Of(1).Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .Else(_ => result = 3)
                 .Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT2WhenExecCaseOfDoesntMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string>("cyan");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .Else(_ => result = 3)
                 .Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionT1T2WithInvalidExecTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match()
                                                          .CaseOf<int>().Do(_ => {})
                                                          .CaseOf<float>().Do(_ => {})
                                                          .Exec());
        }
    }
}