using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3BasicMatchExecTests
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void UnionWithT1_MatchesBasicCase1CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors>(1);
            var result = 0;
            union.Match().Case1().Do(x => result = x)
                 .Case2().Do(x => result = 2)
                 .Case3().Do(x => result = 3).Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_MatchesBasicCase2CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors>("la la");
            var result = 0;
            union.Match().Case1().Do(x => result = 1)
                 .Case2().Do(x => result = 2)
                 .Case3().Do(x => result = 3).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_MatchesBasicCase3CorrectlyWithExec()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            var result = 0;
            union.Match().Case1().Do(x => result = 1)
                 .Case2().Do(x => result = 2)
                 .Case3().Do(x => result = 3).Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1AndNoCase1MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string, Colors>(2);
            _ = Throws<NoMatchException>(() => union.Match().Case2().Do(x => {}).Case3().Do(x => {}).Exec());
        }

        [Test]
        public void UnionWithT2AndNoCase2MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string, Colors>("la la");
            _ = Throws<NoMatchException>(() => union.Match().Case1().Do(x => {}).Case3().Do(x => {}).Exec());
        }

        [Test]
        public void UnionWithT3AndNoCase3MatchWithExec_ThrowsException()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            _ = Throws<NoMatchException>(() => union.Match().Case1().Do(x => {}).Case2().Do(x => {}).Exec());
        }

        [Test]
        public void UnionWithT1_UsesElseIfNoCase1MatchWithExec()
        {
            var union = new Union<int, string, Colors>(11);
            var result = 0;
            union.Match().Case2().Do(x => result = 1).Case3().Do(x => result = 3).Else(x => result = x.Case1).Exec();
            AreEqual(11, result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2MatchWithExec()
        {
            var union = new Union<int, string, Colors>("fred");
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case3().Do(x => result = 3).Else(x => result = 2).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_UsesElseIfNoCase3MatchWithExec()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            var result = 0;
            union.Match().Case1().Do(x => result = 1).Case2().Do(x => result = 2).Else(x => result = 3).Exec();
            AreEqual(3, result);
        }

        [Test]
        public void UnionWithT1_UsesCase1MatchOverElseExec()
        {
            var union = new Union<int, string, Colors>(2);
            var result = 0;
            union.Match().Case1().Do(x => result = x).Else(x => result = 1).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesCase2MatchOverElseExec()
        {
            var union = new Union<int, string, Colors>("x");
            var result = 0;
            union.Match().Case2().Do(x => result = 1).Else(x => result = 2).Exec();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT3_UsesCase3MatchOverElseExec()
        {
            var union = new Union<int, string, Colors>(Colors.Green);
            var result = 0;
            union.Match().Case3().Do(x => result = 1).Else(x => result = 3).Exec();
            AreEqual(1, result);
        }
    }
}