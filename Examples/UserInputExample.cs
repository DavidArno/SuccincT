using System;
using SuccincT.FunctionalComposition;
using SuccincT.Options;
using SuccincT.Parsers;
using SuccincT.PatternMatchers;

namespace SuccinctExamples
{
    public static class UserInputExample
    {
        public static Option<int> GetNumberFromUser(Action askMethod,
                                                    Action reAskMethod,
                                                    Func<string> getValueMethod,
                                                    int minValue,
                                                    int maxValue,
                                                    int numberOfAttempts)
        {
            Func<Action, Action, Func<string>, int, int, int, Option<int>> getNumber = GetNumberFromUser;
            var askAgain = getNumber.Compose(reAskMethod, reAskMethod, getValueMethod, minValue, maxValue);

            askMethod();
            return getValueMethod()
                .ParseInt()
                .Match<Option<int>>()
                .Some().Where(i => i >= minValue && i <= maxValue).Do(Option<int>.Some)
                .Else(_ => numberOfAttempts.Match().To<Option<int>>()
                                           .Where(i => i <= 1).Do(Option<int>.None())
                                           .Else(__ => askAgain(--numberOfAttempts))
                                           .Result())
                .Result();
        }
    }
}