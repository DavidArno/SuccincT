using System;
using NUnit.Framework;
using SuccincT.FunctionalComposition;
using SuccincT.Options;
using SuccinctExamples;

namespace SuccincTTests.WorkedExamplesTests
{
    [TestFixture]
    internal class UserInputExampleTests
    {
        private static readonly Func<Action, Action, Func<string>, int, int, int, Option<int>> GetNumber =
            UserInputExample.GetNumberFromUser;

        private static readonly Func<Action, Action, Func<string>, Option<int>> GetNumberBetween0And10 =
            GetNumber.TailCompose(2).TailCompose(10).TailCompose(0);

        [Test]
        public void ProvidingValidValue1stTime_CallsAskAndGetValueAndReturnsValue()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var result = GetNumberBetween0And10(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => "10");
            Assert.IsTrue(askedCalled);
            Assert.IsFalse(reAskCalled);
            Assert.AreEqual(10, result.Value);
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
            Assert.IsTrue(askedCalled);
            Assert.IsTrue(reAskCalled);
            Assert.AreEqual(9, result.Value);
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
            Assert.IsTrue(askedCalled);
            Assert.IsTrue(reAskCalled);
            Assert.IsFalse(result.HasValue);
        }
    }
}