using System;

namespace SuccincT.Options
{
    /// <summary>
    /// Fluent class created by <code>Option{T}.Match().None()</code>. Whilst this is a public
    /// class (as the user needs access to Do()), it has an internal constructor as it's intended for internal
    /// pattern matching usage only.
    /// </summary>
    public sealed class NoneMatchHandler<T>
    {
        private readonly Action<Action> _recorder;
        private readonly OptionMatcher<T> _matcher;

        internal NoneMatchHandler(Action<Action> recorder, OptionMatcher<T> matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public OptionMatcher<T> Do(Action action)
        {
            _recorder(action);
            return _matcher;
        }
    }
}