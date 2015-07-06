using System;

namespace SuccincT.PatternMatchers
{
    public sealed class WhereForActionHandler<TMatcher, T>
    {
        private readonly Func<T, bool> _expression;
        private readonly Action<Func<T, bool>, Action<T>> _recorder;
        private readonly TMatcher _matcher;

        internal WhereForActionHandler(Func<T, bool> expression,
                                              Action<Func<T, bool>, Action<T>> recorder,
                                              TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Action<T> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }
    }
}