using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionPatternCaseHandler<TMatcher, TValue, TReturn>
    {
        private readonly Action<Func<TValue, bool>, Func<TValue, TReturn>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<TValue, bool>, Func<TValue, TReturn>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public MatchExpressionHandler<TMatcher, TValue, TReturn> Of(TValue value)
        {
            return new MatchExpressionHandler<TMatcher, TValue, TReturn>(value, _recorder, _matcher);
        }

        public MatchWhereHandler<TMatcher, TValue, TReturn> Where(Func<TValue, bool> expression)
        {
            return new MatchWhereHandler<TMatcher, TValue, TReturn>(expression, _recorder, _matcher);
        }

        public TMatcher Do(Func<TValue, TReturn> action)
        {
            _recorder(x => true, action);
            return _matcher;
        }
    }
}