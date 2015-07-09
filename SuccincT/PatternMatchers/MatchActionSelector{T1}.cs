using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    ///
    /// </summary>
    internal sealed class MatchActionSelector<T1>
    {
        private readonly Action<T1> _defaultAction;

        private readonly List<Tuple<Func<T1, bool>, Action<T1>>> _testsAndActions =
            new List<Tuple<Func<T1, bool>, Action<T1>>>();

        public MatchActionSelector(Action<T1> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<T1, bool> test, Action<T1> action)
        {
            _testsAndActions.Add(new Tuple<Func<T1, bool>, Action<T1>>(test, action));
        }

        public void InvokeMatchedActionUsingDefaultIfRequired(T1 value)
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

        public Option<Action<T1>> FindMatchedActionOrNone(T1 value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            return action != null ? Option<Action<T1>>.Some(action.Item2) : Option<Action<T1>>.None();
        }
    }
}