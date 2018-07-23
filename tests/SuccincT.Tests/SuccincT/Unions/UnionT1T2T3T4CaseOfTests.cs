using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4CaseOfTests
    {
        private enum Plants { Rose, Tree, Weed }
        private enum Foods { Cheese, Cake, Chocolate }

        [Test]
        public void UnionWithT1_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Result();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("red");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Result();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Tree);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Result();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT4_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Result();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT1WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(99);
            var result = union.Match<int>()
                              .CaseOf<int>().Of(1).Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Else(5)
                              .Result();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT2WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("blue");
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Of("red").Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Else(5)
                              .Result();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT3WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Weed);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Of(Plants.Rose).Do(3)
                              .CaseOf<Foods>().Do(4)
                              .Else(5)
                              .Result();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT4WhenCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cheese);
            var result = union.Match<int>()
                              .CaseOf<int>().Do(1)
                              .CaseOf<string>().Do(2)
                              .CaseOf<Plants>().Do(3)
                              .CaseOf<Foods>().Of(Foods.Chocolate).Do(4)
                              .Else(5)
                              .Result();
            AreEqual(5, result);
        }

        [Test]
        public void UnionT1T2T3WithInvalidTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match<int>()
                                                          .CaseOf<Foods>().Do(1)
                                                          .CaseOf<float>().Do(2)
                                                          .Result());
        }

        [Test]
        public void UnionWithT1_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("yellow");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Tree);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT4_ExecCaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Chocolate);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Exec();
            AreEqual(4, result);
        }

        [Test]
        public void UnionWithT1WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(99);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Of(1).Do(_ => result = 1)
                 .CaseOf<string>().Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Else(_ => result = 5)
                 .Exec();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT2WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("cyan");
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Else(_ => result = 5)
                 .Exec();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT3WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Weed);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .CaseOf<Plants>().Of(Plants.Tree).Do(_ => result = 3)
                 .CaseOf<Foods>().Do(_ => result = 4)
                 .Else(_ => result = 5)
                 .Exec();
            AreEqual(5, result);
        }

        [Test]
        public void UnionWithT4WhenExecCaseOfDoesNotMatch_ElseUsedCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var result = -1;
            union.Match()
                 .CaseOf<int>().Do(_ => result = 1)
                 .CaseOf<string>().Of("green").Do(_ => result = 2)
                 .CaseOf<Plants>().Do(_ => result = 3)
                 .CaseOf<Foods>().Of(Foods.Cheese).Do(_ => result = 4)
                 .Else(_ => result = 5)
                 .Exec();
            AreEqual(5, result);
        }

        [Test]
        public void UnionT1T2WithInvalidExecTypeCaseOf_ThrowsException()
        {
            var union = new Union<int, string,Plants,Foods>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Match()
                                                          .CaseOf<Foods>().Do(_ => { })
                                                          .CaseOf<Plants>().Do(_ => { })
                                                          .CaseOf<float>().Do(_ => { })
                                                          .Exec());
        }
    }
}