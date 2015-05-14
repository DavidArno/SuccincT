using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Options
{
    public class OptionMatcherUnit<T>
    {
        private readonly Option<T> _option;
        private readonly List<Tuple<List<T>, Action<T>>> _specificValueActions = new List<Tuple<List<T>, Action<T>>>(); 

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

        internal void Some(List<T> values, Action<T> action)
        {
            _specificValueActions.Add(new Tuple<List<T>, Action<T>>(values, action));
        }

        private Action<T> FindSpecificValueAction()
        {
            return (from valueAction in _specificValueActions
                    where valueAction.Item1.Any(value => EqualityComparer<T>.Default.Equals(_option.Value, value))
                    select valueAction.Item2).FirstOrDefault();
        }
    }
}