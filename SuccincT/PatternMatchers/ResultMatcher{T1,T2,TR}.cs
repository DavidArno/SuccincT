using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T1, T2, TResult>
    {
        private readonly MatchFunctionSelector<T1, T2, TResult> _functionSelector;
        private readonly Tuple<T1, T2> _item;

        internal ResultMatcher(Tuple<T1, T2> item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T1, T2, TResult>((x, y) =>
            {
                throw new NoMatchException($"No match action exists for value of ({_item.Item1}, {_item.Item2}");
            });
        }

        public WithForFuncHandler<ResultMatcher<T1, T2, TResult>, T1, T2, TResult> With(T1 value1, T2 value2) => 
            new WithForFuncHandler<ResultMatcher<T1, T2, TResult>, T1, T2, TResult>(Tuple.Create(value1, value2), 
                                                                                    RecordAction, 
                                                                                    this);

        public WhereForFuncHandler<ResultMatcher<T1, T2, TResult>, T1, T2, TResult> Where(Func<T1, T2, bool> expression) => 
            new WhereForFuncHandler<ResultMatcher<T1, T2, TResult>, T1, T2, TResult>(expression,
                                                                                     RecordAction,
                                                                                     this);

        private void RecordAction(Func<T1, T2, bool> test, Func<T1, T2, TResult> action) =>
            _functionSelector.AddTestAndAction(test, action);

        public ResultMatcherWithElse<T1, T2, TResult> Else(Func<T1, T2, TResult> action) =>
            new ResultMatcherWithElse<T1, T2, TResult>(_functionSelector, action, _item);

        public ResultMatcherWithElse<T1, T2, TResult> Else(TResult result) => 
            new ResultMatcherWithElse<T1, T2, TResult>(_functionSelector, (x, y) => result, _item);

        public TResult Result() => 
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);
    }
}