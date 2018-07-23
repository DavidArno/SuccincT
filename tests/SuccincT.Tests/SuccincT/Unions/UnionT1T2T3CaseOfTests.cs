using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3CaseOfTests
    {
        private enum Plants { Rose, Tree, Weed }

        [Test]
        public void UnionWithT1_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(2);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>("red");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Tree);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>(99);
            var result = union.Match<int>()
                              .CaseOf<int>().Of(1).Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .Else(4)
                              .Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT2WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>("blue");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Of("red").Do(2)
                              .CaseOf<Plants>().Do(3)
                              .Else(4)
                              .Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT3WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Weed);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Of(Plants.Rose).Do(3)
                              .Else(4)
                              .Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionT1T2T3WithInvalidTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string, Plants>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match<int>()
                                                          .CaseOf<Plants>().Do(1)
                                                          .CaseOf<float>().Do(2)
                                                          .Result());
        }

        [Test]
        public void UnionWithT1_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(2);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>("yellow");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Tree);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>(99);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Of(1).Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .Else(_ => result = 4)
                 .Exec();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT2WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>("cyan");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .Else(_ => result = 4)
                 .Exec();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT3WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Weed);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .CaseOf<Plants>().Of(Plants.Tree).Do(_ => result = 3)
                 .Else(_ => result = 4)
                 .Exec();
            AreEqual(4, result);
        }

        [Test]
        public void UnionT1T2WithInvalidExecTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string,Plants>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match()
                                                          .CaseOf<int>().Do(_ => { })
                                                          .CaseOf<Plants>().Do(_ => { })
                                                          .CaseOf<float>().Do(_ => { })
                                                          .Exec());
        }
    }
}