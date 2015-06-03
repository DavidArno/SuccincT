using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.Unions
{
    public class UnionT1T2T3BasicMatchTests 
    {
        private enum Colors { Red, Green, Blue }

        [Test]
        public void UnionWithT1_MatchesBasicCase1Correctly()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>()
                              .Case1().Do(x => 0)
                              .Case2().Do(x => 1)
                              .Case3().Do(x => 2).Result();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void UnionWithT2_MatchesBasicCase2Correctly()
        {
            var union = new Union<int, string, Colors>("la la");
            var result = union.Match<int>()
                              .Case1().Do(x => 1)
                              .Case2().Do(x => 2)
                              .Case3().Do(x => 3).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT3_MatchesBasicCase3Correctly()
        {
            var union = new Union<int, string, Colors>(Colors.Green);
            var result = union.Match<int>()
                              .Case1().Do(x => 1)
                              .Case2().Do(x => 2)
                              .Case3().Do(x => 3).Result();
            Assert.AreEqual(3, result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT1AndNoCase1Match_ThrowsException()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<bool>().Case2().Do(x => true).Case3().Do(x => true).Result();
            Assert.IsFalse(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT2AndNoCase2Match_ThrowsException()
        {
            var union = new Union<int, string, Colors>("la la");
            var result = union.Match<bool>().Case1().Do(x => false).Case3().Do(x => false).Result();
            Assert.IsFalse(result);
        }

        [Test, ExpectedException(ExpectedException = typeof(NoMatchException))]
        public void UnionWithT3AndNoCase3Match_ThrowsException()
        {
            var union = new Union<int, string, Colors>(Colors.Red);
            var result = union.Match<bool>().Case1().Do(x => false).Case2().Do(x => false).Result();
            Assert.IsFalse(result);
        }

        [Test]
        public void UnionWithT1_UsesElseIfNoCase1Match()
        {
            var union = new Union<int, string, Colors>(5);
            var result = union.Match<bool>().Case2().Do(x => false).Case3().Do(x => false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT2_UsesElseIfNoCase2Match()
        {
            var union = new Union<int, string, Colors>("fred");
            var result = union.Match<bool>().Case1().Do(x => false).Case3().Do(x => false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT3_UsesElseIfNoCase3Match()
        {
            var union = new Union<int, string, Colors>(Colors.Green);
            var result = union.Match<bool>().Case1().Do(x => false).Case2().Do(x => false).Else(x => true).Result();
            Assert.IsTrue(result);
        }

        [Test]
        public void UnionWithT1_UsesCase1MatchOverElse()
        {
            var union = new Union<int, string, Colors>(2);
            var result = union.Match<int>().Case1().Do(x => x).Else(x => 1).Result();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void UnionWithT2_UsesCase2MatchOverElse()
        {
            var union = new Union<int, string, Colors>("x");
            var result = union.Match<int>().Case2().Do(x => 1).Else(x => 2).Result();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UnionWithT3_UsesCase3MatchOverElse()
        {
            var union = new Union<int, string, Colors>(Colors.Blue);
            var result = union.Match<int>().Case3().Do(x => 1).Else(x => 3).Result();
            Assert.AreEqual(1, result);
        }
    }
}