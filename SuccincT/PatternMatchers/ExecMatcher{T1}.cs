using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// An instance of this class will start a fluent function chain for defining and evaluating a pattern match against
    /// a value of T. It either ends in Exec(), or if At{TResult}() is used, switches to a ResultMatcher{T}.
    /// </summary>
    public sealed class ExecMatcher<T1>
    {
        private readonly MatchActionSelector<T1> _actionSelector;
        private readonly T1 _item;

        internal ExecMatcher(T1 item)
        {
            _item = item;
            _actionSelector = new MatchActionSelector<T1>(
                x => { throw new NoMatchException($"No match action exists for value of {_item}"); });
        }

        public ResultMatcher<T1, TResult> To<TResult>() => new ResultMatcher<T1, TResult>(_item);

        public WithForActionHandler<ExecMatcher<T1>, T1> With(T1 value) =>
            new WithForActionHandler<ExecMatcher<T1>, T1>(value, RecordAction, this);

        public WhereForActionHandler<ExecMatcher<T1>, T1> Where(Func<T1, bool> expression) =>
            new WhereForActionHandler<ExecMatcher<T1>, T1>(expression, RecordAction, this);

        private void RecordAction(Func<T1, bool> test, Action<T1> action) => _actionSelector.AddTestAndAction(test, action);

        public ExecMatcherAfterElse<T1> Else(Action<T1> action) => new ExecMatcherAfterElse<T1>(_actionSelector, action, _item);

        public ExecMatcherAfterElse<T1> IgnoreElse() => new ExecMatcherAfterElse<T1>(_actionSelector, x => { }, _item);

        public void Exec() => _actionSelector.InvokeMatchedActionUsingDefaultIfRequired(_item);
    }
}