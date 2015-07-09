using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcher<T1, T2, T3, TResult>
    {
        private readonly MatchFunctionSelector<T1, T2, T3, TResult> _functionSelector;
        private readonly Tuple<T1, T2, T3> _item;

        internal ResultMatcher(Tuple<T1, T2, T3> item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T1, T2, T3, TResult>((x, y, z) =>
                {
                    throw new NoMatchException(string.Format("No match action exists for value of ({0}, {1}, {2})",
                                                             _item.Item1,
                                                             _item.Item2,
                                                             _item.Item3));
                });
        }

        public WithForFuncHandler<ResultMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>
            With(T1 value1, T2 value2, T3 value3)
        {
            return new WithForFuncHandler<ResultMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>(
                Tuple.Create(value1, value2, value3), RecordAction, this);
        }

        public WhereForFuncHandler<ResultMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult> Where(Func<T1, T2, T3, bool> expression)
        {
            return new WhereForFuncHandler<ResultMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>(expression,
                                                                                            RecordAction,
                                                                                            this);
        }

        private void RecordAction(Func<T1, T2, T3, bool> test, Func<T1, T2, T3, TResult> action)
        {
            _functionSelector.AddTestAndAction(test, action);
        }

        public ResultMatcherWithElse<T1, T2, T3, TResult> Else(Func<T1, T2, T3, TResult> action)
        {
            return new ResultMatcherWithElse<T1, T2, T3, TResult>(_functionSelector, action, _item);
        }

        public ResultMatcherWithElse<T1, T2, T3, TResult> Else(TResult result)
        {
            return new ResultMatcherWithElse<T1, T2, T3, TResult>(_functionSelector, (x, y, z) => result, _item);
        }

        public TResult Result()
        {
            return _functionSelector.DetermineResultUsingDefaultIfRequired(_item);
        }
    }
}