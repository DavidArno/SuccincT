using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T1, TResult>
    {
        private readonly MatchFunctionSelector<T1, TResult> _functionSelector;
        private readonly T1 _item;

        internal ResultMatcher(T1 item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T1, TResult>(
                x => { throw new NoMatchException(string.Format("No match action exists for value of {0}", _item)); });
        }

        public WithForFuncHandler<ResultMatcher<T1, TResult>, T1, TResult> With(T1 value)
        {
            return new WithForFuncHandler<ResultMatcher<T1, TResult>, T1, TResult>(value, RecordAction, this);
        }

        public WhereForFuncHandler<ResultMatcher<T1, TResult>, T1, TResult> Where(Func<T1, bool> expression)
        {
            return new WhereForFuncHandler<ResultMatcher<T1, TResult>, T1, TResult>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T1, bool> test, Func<T1, TResult> action)
        {
            _functionSelector.AddTestAndAction(test, action);
        }

        public ResultMatcherWithElse<T1, TResult> Else(Func<T1, TResult> action)
        {
            return new ResultMatcherWithElse<T1, TResult>(_functionSelector, action, _item);
        }

        public ResultMatcherWithElse<T1, TResult> Else(TResult result)
        {
            return new ResultMatcherWithElse<T1, TResult>(_functionSelector, x => result, _item);
        }

        public TResult Result()
        {
            return _functionSelector.DetermineResultUsingDefaultIfRequired(_item);
        }
    }
}