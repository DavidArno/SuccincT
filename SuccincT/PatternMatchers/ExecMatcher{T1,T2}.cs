using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// An instance of this class will start a fluent function chain for defining and evaluating a pattern match against
    /// values of T1 and T1. It either ends in Exec(), or if At{TResult}() is used, switches to a ResultMatcher{T1,T2}.
    /// </summary>
    public sealed class ExecMatcher<T1, T2>
    {
        private readonly MatchActionSelector<T1, T2> _actionSelector;
        private readonly Tuple<T1, T2> _item;

        internal ExecMatcher(T1 item1, T2 item2)
        {
            _item = Tuple.Create(item1, item2);
            _actionSelector = new MatchActionSelector<T1, T2>((x, y) =>
            {
                throw new NoMatchException($"No match action exists for value of ({_item.Item1},{_item.Item2})");
            });
        }

        public ResultMatcher<T1, T2, TResult> To<TResult>() => new ResultMatcher<T1, T2, TResult>(_item);

        public WithForActionHandler<ExecMatcher<T1, T2>, T1, T2> With(T1 value1, T2 value2) =>
            new WithForActionHandler<ExecMatcher<T1, T2>, T1, T2>(Tuple.Create(value1, value2), RecordAction, this);

        public WhereForActionHandler<ExecMatcher<T1, T2>, T1, T2> Where(Func<T1, T2, bool> expression) =>
            new WhereForActionHandler<ExecMatcher<T1, T2>, T1, T2>(expression, RecordAction, this);

        private void RecordAction(Func<T1, T2, bool> test, Action<T1, T2> action) =>
            _actionSelector.AddTestAndAction(test, action);

        public ExecMatcherAfterElse<T1, T2> Else(Action<T1, T2> action) =>
            new ExecMatcherAfterElse<T1, T2>(_actionSelector, action, _item);

        public ExecMatcherAfterElse<T1, T2> IgnoreElse() =>
            new ExecMatcherAfterElse<T1, T2>(_actionSelector, (x, y) => { }, _item);

        public void Exec() => _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item.Item1, _item.Item2);
    }
}