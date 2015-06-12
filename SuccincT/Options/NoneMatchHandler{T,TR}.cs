using System;

namespace SuccincT.Options
{
    /// <summary>
    /// Fluent class created by <code>Option{T}.Match{TResult}.None()</code>. Whilst this is a public
    /// class (as the usser needs access to <code>Do()</code>) but has an intrnal constructor.
    /// </summary>
    /// <remarks>See [wiki//]</remarks>
    /// <typeparam name="T"/><typeparam name="TReturn"/>
    public sealed class NoneMatchHandler<T, TReturn>
    {
        private readonly Action<Func<TReturn>> _recorder;
        private readonly OptionMatcher<T, TReturn> _matcher;

        internal NoneMatchHandler(Action<Func<TReturn>> recorder, OptionMatcher<T, TReturn> matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public OptionMatcher<T, TReturn> Do(Func<TReturn> action)
        {
            _recorder(action);
            return _matcher;
        }
    }
}