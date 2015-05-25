using System;

namespace SuccincT.Options
{
    public class NoneMatchHandler<T>
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