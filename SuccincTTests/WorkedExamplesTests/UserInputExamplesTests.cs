using System;
using NUnit.Framework;
using SuccincT.Options;
using SuccinctExamples;
using SuccincT.Functional;
using static NUnit.Framework.Assert;

namespace SuccincTTests.WorkedExamplesTests
{
    [TestFixture]
    internal class UserInputExamplesTests
    {
        private static readonly Func<Action, Action, Func<string>, int, int, int, Option<int>> GetNumber =
            UserInputExamples.GetNumberFromUser;

        private static readonly Func<Action, Action, Func<string>, Option<int>> GetNumberBetween0And10 =
            GetNumber.TailApply(2).TailApply(10).TailApply(0);

        [Test]
        public void ProvidingValidValue1stTime_CallsAskAndGetValueAndReturnsValue()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var result = GetNumberBetween0And10(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => "10");
            IsTrue(askedCalled);
            IsFalse(reAskCalled);
            AreEqual(10, result.Value);
        }

        [Test]
        public void ProvidingValidValue2ndTime_CallsAskAndAskAgainAndGetValueAndReturnsValue()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var responses = new[] { "-1", "9" };
            var index = 0;
            var result = GetNumberBetween0And10(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => responses[index++]);
            IsTrue(askedCalled);
            IsTrue(reAskCalled);
            AreEqual(9, result.Value);
        }

        [Test]
        public void ProvidingValidValue3rdTime_CallsAskAndAskAgainAndGetValueAndReturnsNone()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var responses = new[] { "-1", "11", "5" };
            var index = 0;
            var result = GetNumberBetween0And10(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => responses[index++]);
            IsTrue(askedCalled);
            IsTrue(reAskCalled);
            IsFalse(result.HasValue);
        }
    }
}