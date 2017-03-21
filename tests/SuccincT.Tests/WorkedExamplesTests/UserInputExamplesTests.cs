﻿using System;
using NUnit.Framework;
using SuccincT.Examples;
using SuccincT.Options;
using SuccincT.Functional;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.WorkedExamplesTests
{
    [TestFixture]
    internal class UserInputExamplesTests
    {
        private static readonly Func<Action, Action, Func<string>, Option<int>> GetNumberBetween0And10Max2Times =
           Func<Action, Action, Func<string>, int, int, int, Option<int>>(UserInputExamples.GetNumberFromUser)
            .TailApply(2).TailApply(10).TailApply(0);

        [Test]
        public void ProvidingValidValue1stTime_CallsAskAndGetValueAndReturnsValue()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var result = GetNumberBetween0And10Max2Times(() => askedCalled = true,
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
            var result = GetNumberBetween0And10Max2Times(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => responses[index++]);
            IsTrue(askedCalled);
            IsTrue(reAskCalled);
            AreEqual(9, result.Value);
        }

        [Test]
        public void ProvidingValidValue3rdTime_CallsAskAndAskAgainButNotThirdTimeAndReturnsNone()
        {
            var askedCalled = false;
            var reAskCalled = false;
            var responses = new[] { "-1", "11", "5" };
            var index = 0;
            var result = GetNumberBetween0And10Max2Times(() => askedCalled = true,
                                                () => reAskCalled = true,
                                                () => responses[index++]);
            IsTrue(askedCalled);
            IsTrue(reAskCalled);
            IsFalse(result.HasValue);
        }
    }
}