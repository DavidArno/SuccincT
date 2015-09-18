using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelector<T1, T2, TResult>
    {
        private readonly Func<T1, T2, TResult> _defaultFunction;

        private readonly List<Tuple<Func<T1, T2, bool>, Func<T1, T2, TResult>>> _testsAndFunctions =
            new List<Tuple<Func<T1, T2, bool>, Func<T1, T2, TResult>>>();

        public MatchFunctionSelector(Func<T1, T2, TResult> defaultFunction)
        {
            _defaultFunction = defaultFunction;
        }

        public void AddTestAndAction(Func<T1, T2, bool> test, Func<T1, T2, TResult> action) => 
            _testsAndFunctions.Add(new Tuple<Func<T1, T2, bool>, Func<T1, T2, TResult>>(test, action));

        public TResult DetermineResultUsingDefaultIfRequired(Tuple<T1, T2> value)
        {
            var function = _testsAndFunctions.FirstOrDefault(x => x.Item1(value.Item1, value.Item2));
            return function != null
                ? function.Item2(value.Item1, value.Item2)
                : _defaultFunction(value.Item1, value.Item2);
        }

        public Option<TResult> DetermineResult(Tuple<T1, T2> value)
        {
            var function = _testsAndFunctions.FirstOrDefault(tuple => tuple.Item1(value.Item1, value.Item2));
            return function != null
                ? Option<TResult>.Some(function.Item2(value.Item1, value.Item2))
                : Option<TResult>.None();
        }
    }
}