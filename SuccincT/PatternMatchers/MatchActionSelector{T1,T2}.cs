using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchActionSelector<T1, T2>
    {
        private readonly Action<T1, T2> _defaultAction;

        private readonly List<Tuple<Func<T1, T2, bool>, Action<T1, T2>>> _testsAndActions =
            new List<Tuple<Func<T1, T2, bool>, Action<T1, T2>>>();

        public MatchActionSelector(Action<T1, T2> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<T1, T2, bool> test, Action<T1, T2> action)
        {
            _testsAndActions.Add(new Tuple<Func<T1, T2, bool>, Action<T1, T2>>(test, action));
        }

        public void InvokeMatchedActionUsingDefaultIfRequired(T1 value1, T2 value2)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value1, value2));
            if (action != null)
            {
                action.Item2(value1, value2);
            }
            else
            {
                _defaultAction(value1, value2);
            }
        }

        public Option<Action<T1, T2>> FindMatchedActionOrNone(T1 value1, T2 value2)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value1, value2));
            return action != null ? Option<Action<T1, T2>>.Some(action.Item2) : Option<Action<T1, T2>>.None();
        }
    }
}