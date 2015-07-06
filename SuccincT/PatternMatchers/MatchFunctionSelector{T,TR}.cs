using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelector<T, TResult>
    {
        private readonly Func<T, TResult> _defaultFunction;

        private readonly List<Tuple<Func<T, bool>, Func<T, TResult>>> _testsAndFunctions =
            new List<Tuple<Func<T, bool>, Func<T, TResult>>>();

        public MatchFunctionSelector(Func<T, TResult> defaultFunction)
        {
            _defaultFunction = defaultFunction;
        }

        public void AddTestAndAction(Func<T, bool> test, Func<T, TResult> action)
        {
            _testsAndFunctions.Add(new Tuple<Func<T, bool>, Func<T, TResult>>(test, action));
        }

        public TResult DetermineResultUsingDefaultIfRequired(T value)
        {
            var function = _testsAndFunctions.FirstOrDefault(tuple => tuple.Item1(value));
            return function != null ? function.Item2(value) : _defaultFunction(value);
        }

        public Option<TResult> DetermineResult(T value)
        {
            var function = _testsAndFunctions.FirstOrDefault(tuple => tuple.Item1(value));
            return function != null ? Option<TResult>.Some(function.Item2(value)) : Option<TResult>.None();
        }
    }
}