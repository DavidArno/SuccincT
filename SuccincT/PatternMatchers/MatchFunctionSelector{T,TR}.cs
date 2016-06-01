using System;
using System.Collections.Generic;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelector<T, TResult>
    {
        private readonly Func<T, TResult> _defaultFunction;

        private readonly List<MatchFunctionSelectorData<T, TResult>> _testsAndFunctions =
            new List<MatchFunctionSelectorData<T, TResult>>();

        public MatchFunctionSelector(Func<T, TResult> defaultFunction)
        {
            _defaultFunction = defaultFunction;
        }

        public void AddTestAndAction(Func<T, IList<T>, bool> withFunc,
                                     IList<T> withData,
                                     Func<T, bool> whereFunc,
                                     Func<T, TResult> action)
        {
            _testsAndFunctions.Add(new MatchFunctionSelectorData<T, TResult>(withFunc, whereFunc, withData, action));
        }

        public TResult DetermineResultUsingDefaultIfRequired(T value)
        {
            foreach (var data in _testsAndFunctions)
            {
                if (InvokeCorrectTestMethod(data, value))
                {
                    return data.ActionFunc(value);
                }
            }

            return _defaultFunction(value);
        }

        public Option<TResult> DetermineResult(T value)
        {
            foreach (var data in _testsAndFunctions)
            {
                if (InvokeCorrectTestMethod(data, value))
                {
                    return Option<TResult>.Some(data.ActionFunc(value));
                }
            }

            return Option<TResult>.None();
        }

        private static bool InvokeCorrectTestMethod(MatchFunctionSelectorData<T, TResult> data, T value) => 
            data.WithTestFunc?.Invoke(value, data.WithList) ?? data.WhereTestFunc(value);
    }
}