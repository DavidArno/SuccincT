using System;

namespace SuccincT.PatternMatchers
{
    public sealed class WhereForActionHandler<TMatcher, T1, T2>
    {
        private readonly Func<T1, T2, bool> _expression;
        private readonly Action<Func<T1, T2, bool>, Action<T1, T2>> _recorder;
        private readonly TMatcher _matcher;

        internal WhereForActionHandler(Func<T1, T2, bool> expression,
                                       Action<Func<T1, T2, bool>, Action<T1, T2>> recorder,
                                       TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Action<T1, T2> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }
    }
}