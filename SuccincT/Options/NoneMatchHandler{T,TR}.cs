using System;

namespace SuccincT.Options
{
    /// <summary>
    /// Fluent class created by <code>Option{T}.Match{TResult}.None()</code>. Whilst this is a public
    /// class (as the usser needs access to <code>Do()</code>) but has an intrnal constructor.
    /// </summary>
    public sealed class NoneMatchHandler<T, TResult>
    {
        private readonly Action<Func<TResult>> _recorder;
        private readonly OptionMatcher<T, TResult> _matcher;

        internal NoneMatchHandler(Action<Func<TResult>> recorder, OptionMatcher<T, TResult> matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public OptionMatcher<T, TResult> Do(Func<TResult> action)
        {
            _recorder(action);
            return _matcher;
        }

        public OptionMatcher<T, TResult> Do(TResult value)
        {
            _recorder(() => value);
            return _matcher;
        }
    }
}