using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Options;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    ///
    /// </summary>
    internal sealed class MatchActionSelector<T>
    {
        private readonly Action<T> _defaultAction;

        private readonly List<Tuple<Func<T, bool>, Action<T>>> _testsAndActions =
            new List<Tuple<Func<T, bool>, Action<T>>>();

        public MatchActionSelector(Action<T> defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public void AddTestAndAction(Func<T, bool> test, Action<T> action)
        {
            _testsAndActions.Add(new Tuple<Func<T, bool>, Action<T>>(test, action));
        }

        public void InvokeMatchedActionUsingDefaultIfRequired(T value)
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

        public Option<Action<T>> FindMatchedActionOrNone(T value)
        {
            var action = _testsAndActions.FirstOrDefault(tuple => tuple.Item1(value));
            return action != null ? Option<Action<T>>.Some(action.Item2) : Option<Action<T>>.None();
        }
    }
}