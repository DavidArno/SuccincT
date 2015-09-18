using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T1, T2, T3, T4, TResult>
    {
        private readonly MatchFunctionSelector<T1, T2, T3, T4, TResult> _functionSelector;
        private readonly Tuple<T1, T2, T3, T4> _item;

        internal ResultMatcher(Tuple<T1, T2, T3, T4> item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T1, T2, T3, T4, TResult>(
                (w, x, y, z) =>
                {
                    throw new NoMatchException(
                        $"No match action exists for value of ({_item.Item1}, {_item.Item2}, {_item.Item3})");
                });
        }

        public WithForFuncHandler<ResultMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>
            With(T1 value1, T2 value2, T3 value3, T4 value4) => 
                new WithForFuncHandler<ResultMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>(
                    Tuple.Create(value1, value2, value3, value4),
                    RecordAction,
                    this);

        public WhereForFuncHandler<ResultMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> 
            Where(Func<T1, T2, T3, T4, bool> expression) => 
                new WhereForFuncHandler<ResultMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>(expression,
                                                                                                         RecordAction,
                                                                                                         this);

        private void RecordAction(Func<T1, T2, T3, T4, bool> test, Func<T1, T2, T3, T4, TResult> action) => 
            _functionSelector.AddTestAndAction(test, action);

        public ResultMatcherWithElse<T1, T2, T3, T4, TResult> Else(Func<T1, T2, T3, T4, TResult> action) => 
            new ResultMatcherWithElse<T1, T2, T3, T4, TResult>(_functionSelector, action, _item);

        public ResultMatcherWithElse<T1, T2, T3, T4, TResult> Else(TResult result) => 
            new ResultMatcherWithElse<T1, T2, T3, T4, TResult>(_functionSelector, (w, x, y, z) => result, _item);

        public TResult Result() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);
    }
}