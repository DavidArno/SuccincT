using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Options
{
    public class OptionMatcher<T, TReturn>
    {
        private readonly Option<T> _option;

        private readonly List<Tuple<List<T>, Func<T, TReturn>>> _specificValueActions =
            new List<Tuple<List<T>, Func<T, TReturn>>>();

        private Func<T, TReturn> _someAction =
            x => { throw new InvalidOperationException("No match action defined for Option with arbitary value"); };

        private Func<TReturn> _noneAction =
            () => { throw new InvalidOperationException("No match action defined for Option with no value"); };

        public OptionMatcher(Option<T> option)
        {
            _option = option;
        }

        public OptionMatcher<T, TReturn> Some(Func<T, TReturn> action)
        {
            _someAction = action;
            return this;
        }

        public OptionMatcherExpressionBuilder<T, TReturn> Some(T value)
        {
            return new OptionMatcherExpressionBuilder<T, TReturn>(this, value);
        }

        public OptionMatcher<T, TReturn> None(Func<TReturn> action)
        {
            _noneAction = action;
            return this;
        }

        public TReturn Result()
        {
            if (!_option.HasValue) { return _noneAction(); }

            var action = FindSpecificValueAction();
            if (action != null) { return action(_option.Value); }
            return _someAction(_option.Value);
        }

        internal void Some(List<T> values, Func<T, TReturn> action)
        {
            _specificValueActions.Add(new Tuple<List<T>, Func<T, TReturn>>(values, action));
        }

        private Func<T, TReturn> FindSpecificValueAction()
        {
            return (from valueAction in _specificValueActions
                    where valueAction.Item1.Any(value => EqualityComparer<T>.Default.Equals(_option.Value, value))
                    select valueAction.Item2).FirstOrDefault();
        }
    }
}
