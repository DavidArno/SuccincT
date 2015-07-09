using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelector<T1, TResult>
    {
        private readonly Func<T1, TResult> _defaultFunction;

        private readonly List<Tuple<Func<T1, bool>, Func<T1, TResult>>> _testsAndFunctions =
            new List<Tuple<Func<T1, bool>, Func<T1, TResult>>>();

        public MatchFunctionSelector(Func<T1, TResult> defaultFunction)
        {
            _defaultFunction = defaultFunction;
        }

        public void AddTestAndAction(Func<T1, bool> test, Func<T1, TResult> action)
        {
            _testsAndFunctions.Add(new Tuple<Func<T1, bool>, Func<T1, TResult>>(test, action));
        }

        public TResult DetermineResultUsingDefaultIfRequired(T1 value)
        {
            var function = _testsAndFunctions.FirstOrDefault(tuple => tuple.Item1(value));
            return function != null ? function.Item2(value) : _defaultFunction(value);
        }

        public Option<TResult> DetermineResult(T1 value)
        {
            var function = _testsAndFunctions.FirstOrDefault(tuple => tuple.Item1(value));
            return function != null ? Option<TResult>.Some(function.Item2(value)) : Option<TResult>.None();
        }
    }
}