using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.Unions.PatternMatchers
{
    internal class UnionCaseActionSelector<TValue>
    {
        private readonly Action<TValue> _defaultAction;
        private readonly List<Tuple<Func<TValue, bool>, Action<TValue>>> _testsAndActions = 
            new List<Tuple<Func<TValue, bool>, Action<TValue>>>();  

        public UnionCaseActionSelector(Action<TValue> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<TValue, bool> test, Action<TValue> action)
        {
            _testsAndActions.Add(new Tuple<Func<TValue, bool>, Action<TValue>>(test, action));
        }

        public void InvokeMatchedActionUsingDefaultIfRequired(TValue value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            if (action != null)
            {
                action.Item2(value);
            }
            else
            {
                _defaultAction(value);
            }
        }

        public Option<Action<TValue>> FindMatchedActionOrNone(TValue value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            return action != null ? Option<Action<TValue>>.Some(action.Item2) : Option<Action<TValue>>.None();
        }
    }
}
