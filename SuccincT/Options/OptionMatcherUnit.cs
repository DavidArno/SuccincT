using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Options
{
    public class OptionMatcherUnit<T>
    {
        private readonly Option<T> _option;

        private readonly List<Tuple<List<Func<T, bool>>, Action<T>>> _specificValueActions =
            new List<Tuple<List<Func<T, bool>>, Action<T>>>();

        private Action<T> _someAction =
            x => { throw new InvalidOperationException("No match action defined for Option with arbitary value"); };

        private Action _noneAction =
            () => { throw new InvalidOperationException("No match action defined for Option with no value"); };

        public OptionMatcherUnit(Option<T> option)
        {
            _option = option;
        }

        public OptionMatcherUnit<T> Some(Action<T> action)
        {
            _someAction = action;
            return this;
        }

        public OptionMatcherUnitExpressionBuilder<T> Some(T value)
        {
            return new OptionMatcherUnitExpressionBuilder<T>(this, value);
        }

        public OptionMatcherUnitExpressionBuilder<T> When(Func<T, bool> testExpression)
        {
            return new OptionMatcherUnitExpressionBuilder<T>(this, testExpression);
        }

        public OptionMatcherUnit<T> Some(T value, Action<T> action)
        {
            AddMatchExpressions(new List<Func<T, bool>> { x => EqualityComparer<T>.Default.Equals(x, value) }, action);
            return this;
        }

        public OptionMatcherUnit<T> When(Func<T, bool> testExpression, Action<T> action)
        {
            AddMatchExpressions(new List<Func<T, bool>> { testExpression }, action);
            return this;
        }

        public OptionMatcherUnit<T> None(Action action)
        {
            _noneAction = action;
            return this;
        }

        public void Exec()
        {
            if (_option.HasValue)
            {
                var action = FindSpecificValueAction();
                if (action != null)
                {
                    action(_option.Value);
                }
                else
                {
                    _someAction(_option.Value);
                }
            }
            else
            {
                _noneAction();
            }
        }

        internal void AddMatchExpressions(List<Func<T, bool>> expressions, Action<T> action)
        {
            _specificValueActions.Add(new Tuple<List<Func<T, bool>>, Action<T>>(expressions, action));
        }

        private Action<T> FindSpecificValueAction()
        {
            return (from valueAction in _specificValueActions
                    where valueAction.Item1.Any(func => func(_option.Value))
                    select valueAction.Item2).FirstOrDefault();
        }
    }
}