using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// An instance of this class will start a fluent function chain for defining and evaluating a pattern match against
    /// a value of T. It either ends in Exec(), or if At{TResult}() is used, switches to a ResultMatcher{T}.
    /// </summary>
    public sealed class ExecMatcher<T>
    {
        private readonly MatchActionSelector<T> _actionSelector;
        private readonly T _item;

        internal ExecMatcher(T item)
        {
            _item = item;
            _actionSelector = new MatchActionSelector<T>(
                x => { throw new NoMatchException(string.Format("No match action exists for value of {0}", _item)); });
        }

        public ResultMatcher<T, TResult> To<TResult>()
        {
            return new ResultMatcher<T, TResult>(_item);
        }

        public WithForActionHandler<ExecMatcher<T>, T> With(T value)
        {
            return new WithForActionHandler<ExecMatcher<T>, T>(value, RecordAction, this);
        }

        public WhereForActionHandler<ExecMatcher<T>, T> Where(Func<T, bool> expression)
        {
            return new WhereForActionHandler<ExecMatcher<T>, T>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T, bool> test, Action<T> action)
        {
            _actionSelector.AddTestAndAction(test, action);
        }

        public ExecMatcherAfterElse<T> Else(Action<T> action)
        {
            return new ExecMatcherAfterElse<T>(_actionSelector, action, _item);
        }

        public void Exec()
        {
            _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item);
        }
    }
}