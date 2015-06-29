using System;

namespace SuccincT.PatternMatchers
{
    public sealed class MatchWhereHandler<TMatcher, TValue, TResult>
    {
        private readonly Func<TValue, bool> _expression;
        private readonly Action<Func<TValue, bool>, Func<TValue, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal MatchWhereHandler(Func<TValue, bool> expression,
                                              Action<Func<TValue, bool>, Func<TValue, TResult>> recorder,
                                              TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Func<TValue, TResult> action)
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