using System;

namespace SuccincT.PatternMatchers
{
    public sealed class WhereForFuncHandler<TMatcher,T1, T2, T3, T4, TResult>
    {
        private readonly Func<T1, T2, T3, T4, bool> _expression;
        private readonly Action<Func<T1, T2, T3, T4, bool>, Func<T1, T2, T3, T4, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WhereForFuncHandler(Func<T1, T2, T3, T4, bool> expression,
                                     Action<Func<T1, T2, T3, T4, bool>, Func<T1, T2, T3, T4, TResult>> recorder,
                                     TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Func<T1, T2, T3, T4, TResult> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(_expression, (w, x, y, z) => value);
            return _matcher;
        }
    }
}