using System;
using System.Collections.Generic;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelector<T1, T2, TResult>
    {
        private readonly Func<T1, TResult> _defaultFunction;

        private readonly List<MatchFunctionSelectorData<T1, T2, TResult>> _testsAndFunctions =
            new List<MatchFunctionSelectorData<T1, T2, TResult>>();

        public MatchFunctionSelector(Func<T1, TResult> defaultFunction)
        {
            _defaultFunction = defaultFunction;
        }

        public void AddTestAndAction(Func<T1, IList<T2>, bool> withFunc,
                                     IList<T2> withData,
                                     Func<T1, bool> whereFunc,
                                     Func<T1, TResult> action)
        {
            _testsAndFunctions.Add(new MatchFunctionSelectorData<T1, T2, TResult>(withFunc, whereFunc, withData, action));
        }

        public TResult DetermineResultUsingDefaultIfRequired(T1 value)
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

        public Option<TResult> DetermineResult(T1 value)
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

        private static bool InvokeCorrectTestMethod(MatchFunctionSelectorData<T1, T2, TResult> data, T1 value) =>
            data.WithTestFunc?.Invoke(value, data.WithList) ?? data.WhereTestFunc(value);
    }
}