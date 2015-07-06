using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T, TResult>
    {
        private readonly MatchFunctionSelector<T, TResult> _functionSelector;
        private readonly T _item;

        internal ResultMatcher(T item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T, TResult>(
                x => { throw new NoMatchException(string.Format("No match action exists for value of {0}", _item)); });
        }

        public WithForFuncHandler<ResultMatcher<T, TResult>, T, TResult> With(T value)
        {
            return new WithForFuncHandler<ResultMatcher<T, TResult>, T, TResult>(value, RecordAction, this);
        }

        public WhereForFuncHandler<ResultMatcher<T, TResult>, T, TResult> Where(Func<T, bool> expression)
        {
            return new WhereForFuncHandler<ResultMatcher<T, TResult>, T, TResult>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T, bool> test, Func<T, TResult> action)
        {
            _functionSelector.AddTestAndAction(test, action);
        }

        public ResultMatcherWithElse<T, TResult> Else(Func<T, TResult> action)
        {
            return new ResultMatcherWithElse<T, TResult>(_functionSelector, action, _item);
        }

        public ResultMatcherWithElse<T, TResult> Else(TResult result)
        {
            return new ResultMatcherWithElse<T, TResult>(_functionSelector, x => result, _item);
        }

        public TResult Result()
        {
            return _functionSelector.DetermineResultUsingDefaultIfRequired(_item);
        }
    }
}