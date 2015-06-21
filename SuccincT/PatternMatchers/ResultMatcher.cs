using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T, TValue>
    {
        private readonly MatchActionSelector<T, TValue> _actionSelector;
        private readonly T _item;

        internal ResultMatcher(T item)
        {
            _item = item;
            _actionSelector = new MatchActionSelector<T, TValue>(
                x => { throw new NoMatchException(string.Format("No match action exists for value of {0}", _item)); });
        }

        public MatchExpressionHandler<ResultMatcher<T, TValue>, T, TValue> With(T value)
        {
            return new MatchExpressionHandler<ResultMatcher<T, TValue>, T, TValue>(value, RecordAction, this);
        }

        public MatchWhereHandler<ResultMatcher<T, TValue>, T, TValue> Where(Func<T, bool> expression)
        {
            return new MatchWhereHandler<ResultMatcher<T, TValue>, T, TValue>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T, bool> test, Func<T, TValue> action)
        {
            _actionSelector.AddTestAndAction(test, action);
        }

        public ResultMatcherWithElse<T, TValue> Else(Func<T, TValue> action)
        {
            return new ResultMatcherWithElse<T, TValue>(_actionSelector, action, _item);
        }

        public ResultMatcherWithElse<T, TValue> Else(TValue value)
        {
            return new ResultMatcherWithElse<T, TValue>(_actionSelector, x => value, _item);
        }

        public TValue Result()
        {
            return _actionSelector.DetermineResultUsingDefaultIfRequired(_item);
        }
    }
}