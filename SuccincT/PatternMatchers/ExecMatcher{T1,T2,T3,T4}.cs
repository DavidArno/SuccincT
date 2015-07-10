using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// An instance of this class will start a fluent function chain for defining and evaluating a pattern match against
    /// values of T1 and T1. It either ends in Exec(), or if At{TResult}() is used, switches to a ResultMatcher{T1,T2}.
    /// </summary>
    public sealed class ExecMatcher<T1, T2, T3, T4>
    {
        private readonly MatchActionSelector<T1, T2, T3, T4> _actionSelector;
        private readonly Tuple<T1, T2, T3, T4> _item;

        internal ExecMatcher(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            _item = Tuple.Create(item1, item2, item3, item4);
            _actionSelector = new MatchActionSelector<T1, T2, T3, T4>(
                (w, x, y, z) =>
                {
                    throw new NoMatchException(
                        string.Format(
                            "No match action exists for value of ({0}, {1}, {2})",
                            _item.Item1,
                            _item.Item2,
                            _item.Item3));
                });
        }

        public ResultMatcher<T1, T2, T3, T4, TResult> To<TResult>()
        {
            return new ResultMatcher<T1, T2, T3, T4, TResult>(_item);
        }

        public WithForActionHandler<ExecMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> With(T1 value1,
                                                                                      T2 value2,
                                                                                      T3 value3,
                                                                                      T4 value4)
        {
            return new WithForActionHandler<ExecMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>(
                Tuple.Create(value1, value2, value3, value4),
                RecordAction,
                this);
        }

        public WhereForActionHandler<ExecMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> Where(
            Func<T1, T2, T3, T4, bool> expression)
        {
            return new WhereForActionHandler<ExecMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>(expression, RecordAction, this);
        }

        private void RecordAction(Func<T1, T2, T3, T4, bool> test, Action<T1, T2, T3, T4> action)
        {
            _actionSelector.AddTestAndAction(test, action);
        }

        public ExecMatcherAfterElse<T1, T2, T3, T4> Else(Action<T1, T2, T3, T4> action)
        {
            return new ExecMatcherAfterElse<T1, T2, T3, T4>(_actionSelector, action, _item);
        }

        public void Exec()
        {
            _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item.Item1, _item.Item2, _item.Item3, _item.Item4);
        }
    }
}