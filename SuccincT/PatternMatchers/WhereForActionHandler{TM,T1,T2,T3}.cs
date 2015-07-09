using System;

namespace SuccincT.PatternMatchers
{
    public sealed class WhereForActionHandler<TMatcher, T1, T2, T3>
    {
        private readonly Func<T1, T2, T3, bool> _expression;
        private readonly Action<Func<T1, T2, T3, bool>, Action<T1, T2, T3>> _recorder;
        private readonly TMatcher _matcher;

        internal WhereForActionHandler(Func<T1, T2, T3, bool> expression,
                                       Action<Func<T1, T2, T3, bool>, Action<T1, T2, T3>> recorder,
                                       TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Action<T1, T2, T3> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }
    }
}