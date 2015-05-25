using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal class UnionCaseActionSelector<TValue, TResult>
    {
        private readonly Func<TValue, TResult> _defaultAction;
        private readonly List<Tuple<Func<TValue, bool>, Func<TValue, TResult>>> _testsAndActions = 
            new List<Tuple<Func<TValue, bool>, Func<TValue, TResult>>>();  

        public UnionCaseActionSelector(Func<TValue, TResult> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<TValue, bool> test, Func<TValue, TResult> action)
        {
            _testsAndActions.Add(new Tuple<Func<TValue, bool>, Func<TValue, TResult>>(test, action));
        }

        public TResult DetermineResultUsingDefaultIfRequired(TValue value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            return action != null ? action.Item2(value) : _defaultAction(value);
        }

        public Option<TResult> DetermineResult(TValue value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            return action != null ? Option<TResult>.Some(action.Item2(value)) : Option<TResult>.None();
        }
    }
}
