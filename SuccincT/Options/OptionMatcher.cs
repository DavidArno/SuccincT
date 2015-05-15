using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Options
{
    public class OptionMatcher<T, TReturn>
    {
        private readonly Option<T> _option;

        private readonly List<Tuple<List<Func<T, bool>>, Func<T, TReturn>>> _specificValueActions =
            new List<Tuple<List<Func<T, bool>>, Func<T, TReturn>>>();

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

        public OptionMatcherExpressionBuilder<T, TReturn> When(Func<T, bool> testExpression)
        {
            return new OptionMatcherExpressionBuilder<T, TReturn>(this, testExpression);
        }

        public OptionMatcher<T, TReturn> Some(T value, Func<T, TReturn> func)
        {
            AddMatchExpressions(new List<Func<T, bool>> { x => EqualityComparer<T>.Default.Equals(x, value) }, func);
            return this;
        }

        public OptionMatcher<T, TReturn> When(Func<T, bool> testExpression, Func<T, TReturn> action)
        {
            AddMatchExpressions(new List<Func<T, bool>> { testExpression }, action);
            return this;
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

        internal void AddMatchExpressions(List<Func<T, bool>> values, Func<T, TReturn> action)
        {
            _specificValueActions.Add(new Tuple<List<Func<T, bool>>, Func<T, TReturn>>(values, action));
        }

        private Func<T, TReturn> FindSpecificValueAction()
        {
            return (from valueAction in _specificValueActions
                    where valueAction.Item1.Any(func => func(_option.Value))
                    select valueAction.Item2).FirstOrDefault();
        }
    }
}
