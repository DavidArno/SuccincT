using System;

namespace SuccincT.PatternMatchers
{
    public sealed class WhereForFuncHandler<TMatcher, T, TResult>
    {
        private readonly Func<T, bool> _expression;
        private readonly Action<Func<T, bool>, Func<T, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WhereForFuncHandler(Func<T, bool> expression,
                                     Action<Func<T, bool>, Func<T, TResult>> recorder,
                                     TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Func<T, TResult> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(_expression, x => value);
            return _matcher;
        }
    }
}