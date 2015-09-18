using NUnit.Framework;
using SuccincT.Unions;
using SuccincTTests.Examples;
using static NUnit.Framework.Assert;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class UnionMatcherExamplesTests
    {
        [Test]
        public void PassingInAnIntToBoolOrIntMatcherExampleReturnsAnInt()
        {
            var result = UnionMatcherExamples.BoolOrIntMatcherExample(new Union<int, bool>(2));
            AreEqual("int=2", result);
        }

        [Test]
        public void PassingInAnIntToBoolOrIntMatcherExampleReturnsBool()
        {
            var result = UnionMatcherExamples.BoolOrIntMatcherExample(new Union<int, bool>(true));
            AreEqual("bool=True", result);
        }

        [Test]
        public void PassingTrueToYesNoOrIntMatcherExampleReturnsYes()
        {
            var result = UnionMatcherExamples.YesNoOrIntMatcherExample(new Union<int, bool>(true));
            AreEqual("Yes", result);
        }

        [Test]
        public void PassingFalseToYesNoOrIntMatcherExampleReturnsNo()
        {
            var result = UnionMatcherExamples.YesNoOrIntMatcherExample(new Union<int, bool>(false));
            AreEqual("No", result);
        }

        [Test]
        public void PassingTrueToYesNo123OrOtherIntMatcherExampleReturnsYes()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(true));
            AreEqual("Yes", result);
        }

        [Test]
        public void PassingFalseToYesNo123OrOtherIntMatcherExampleReturnsNo()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(false));
            AreEqual("No", result);
        }

        [Test]
        public void Passing12And3ToYesNo123OrOtherIntMatcherExampleHandledCorrectly()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(1)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(2)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(3));
            AreEqual("1 in range 1-32 in range 1-33 in range 1-3", result);
        }

        [Test]
        public void Passing0And4ToYesNo123OrOtherIntMatcherExampleHandledCorrectly()
        {
            var result = UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(0)) +
                         UnionMatcherExamples.YesNo123OrOtherIntMatcherExample(new Union<int, bool>(4));
            AreEqual("int=0int=4", result);
        }

        [Test]
        public void ElseExampleStillMatchesCorrectly()
        {
            var result = UnionMatcherExamples.ElseExample(new Union<int, bool>(0)) +
                         UnionMatcherExamples.ElseExample(new Union<int, bool>(false));
            AreEqual("0false", result);
        }

        [Test]
        public void ElseExampleHandlesElseCorrectly()
        {
            var result = UnionMatcherExamples.ElseExample(new Union<int, bool>(1)) +
                         UnionMatcherExamples.ElseExample(new Union<int, bool>(true));
            AreEqual("????", result);
        }
    }
}