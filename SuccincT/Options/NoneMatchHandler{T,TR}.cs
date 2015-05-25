using System;

namespace SuccincT.Options
{
    public class NoneMatchHandler<T, TReturn>
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