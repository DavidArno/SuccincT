using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionPatternCaseHandler<TMatcher, TValue>
    {
        private readonly Action<Func<TValue, bool>, Action<TValue>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<TValue, bool>, Action<TValue>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public MatchExpressionHandler<TMatcher, TValue> Of(TValue value)
        {
            return new MatchExpressionHandler<TMatcher, TValue>(value, _recorder, _matcher);
        }

        public MatchWhereHandler<TMatcher, TValue> Where(Func<TValue, bool> expression)
        {
            return new MatchWhereHandler<TMatcher, TValue>(expression, _recorder, _matcher);
        }

        public TMatcher Do(Action<TValue> action)
        {
            _recorder(x => true, action);
            return _matcher;
        }
    }
}