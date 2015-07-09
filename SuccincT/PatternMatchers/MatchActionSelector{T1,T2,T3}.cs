using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchActionSelector<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> _defaultAction;

        private readonly List<Tuple<Func<T1, T2, T3, bool>, Action<T1, T2, T3>>> _testsAndActions =
            new List<Tuple<Func<T1, T2, T3, bool>, Action<T1, T2, T3>>>();

        public MatchActionSelector(Action<T1, T2, T3> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<T1, T2, T3, bool> test, Action<T1, T2, T3> action)
        {
            _testsAndActions.Add(new Tuple<Func<T1, T2, T3, bool>, Action<T1, T2, T3>>(test, action));
        }

        public void InvokeMatchedActionUsingDefaultIfRequired(T1 value1, T2 value2, T3 value3)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value1, value2, value3));
            if (action != null)
            {
                action.Item2(value1, value2, value3);
            }
            else
            {
                _defaultAction(value1, value2, value3);
            }
        }

        public Option<Action<T1, T2, T3>> FindMatchedActionOrNone(T1 value1, T2 value2, T3 value3)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value1, value2, value3));
            return action != null ? Option<Action<T1, T2, T3>>.Some(action.Item2) : Option<Action<T1, T2, T3>>.None();
        }
    }
}