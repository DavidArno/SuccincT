using NUnit.Framework;
using SuccincT.Unions;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class UnionMatcherExamplesTests
    {
        [Test]
        public void PassingInAnIntToBoolOrIntMatcherExampleReturnsAnInt()
        {
            var result = UnionMatcherExamples.BoolOrIntMatcherExample(new Union<int, bool>(2));
            Assert.AreEqual("int=2", result);
        }

        [Test]
        public void PassingInAnIntToBoolOrIntMatcherExampleReturnsBool()
        {
            var result = UnionMatcherExamples.BoolOrIntMatcherExample(new Union<int, bool>(true));
            Assert.AreEqual("bool=True", result);
        }

        [Test]
        public void PassingTrueToYesNoOrIntMatcherExampleReturnsYes()
        {
            var result = UnionMatcherExamples.YesNoOrIntMatcherExample(new Union<int, bool>(true));
            Assert.AreEqual("Yes", result);
        }

        [Test]
        public void PassingFalseToYesNoOrIntMatcherExampleReturnsNo()
        {
            var result = UnionMatcherExamples.YesNoOrIntMatcherExample(new Union<int, bool>(false));
            Assert.AreEqual("No", result);
        }

        [Test]
        public void PassingTrueToYesNo123OrOtherIntMatcherExampleReturnsYes()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(true));
            Assert.AreEqual("Yes", result);
        }

        [Test]
        public void PassingFalseToYesNo123OrOtherIntMatcherExampleReturnsNo()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(false));
            Assert.AreEqual("No", result);
        }

        [Test]
        public void Passing12And3ToYesNo123OrOtherIntMatcherExampleHandledCorrectly()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(1)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(2)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(3));
            Assert.AreEqual("1 in range 1-32 in range 1-33 in range 1-3", result);
        }

        [Test]
        public void Passing0And4ToYesNo123OrOtherIntMatcherExampleHandledCorrectly()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(0)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(4));
            Assert.AreEqual("int=0int=4", result);
        }

        [Test]
        public void ElseExampleStillMatchesCorrectly()
        {
            var result = UnionMatcherExamples.ElseExample(new Union<int, bool>(0)) +
                         UnionMatcherExamples.ElseExample(new Union<int, bool>(false));
            Assert.AreEqual("0false", result);
        }

        [Test]
        public void ElseExampleHandlesElseCorrectly()
        {
            var result = UnionMatcherExamples.ElseExample(new Union<int, bool>(1)) +
                         UnionMatcherExamples.ElseExample(new Union<int, bool>(true));
            Assert.AreEqual("????", result);
        }
    }
}
