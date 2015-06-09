using System;

namespace SuccincT.PatternMatchers
{
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

        public MatchExpressionHandler<ExecMatcher<T>, T> With(T value)
        {
            return new MatchExpressionHandler<ExecMatcher<T>, T>(value, RecordAction, this);
        }

        public MatchWhereHandler<ExecMatcher<T>, T> Where(Func<T, bool> expression)
        {
            return new MatchWhereHandler<ExecMatcher<T>, T>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T, bool> test, Action<T> action)
        {
            _actionSelector.AddTestAndAction(test, action);
        }

        public ExecMatcherWithElse<T> Else(Action<T> action)
        {
            return new ExecMatcherWithElse<T>(_actionSelector, action, _item);
        }

        public void Exec()
        {
            _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item);
        }
    }
}