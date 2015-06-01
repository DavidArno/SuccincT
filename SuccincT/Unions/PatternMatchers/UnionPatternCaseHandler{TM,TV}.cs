using System;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionPatternCaseHandler<TMatcher, TValue>
    {
        private readonly Action<Func<TValue, bool>, Action<TValue>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<TValue, bool>, Action<TValue>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public UnionPatternCaseExpressionHandler<TMatcher, TValue> Of(TValue value)
        {
            return new UnionPatternCaseExpressionHandler<TMatcher, TValue>(value, _recorder, _matcher);
        }

        public UnionPatternCaseWhereHandler<TMatcher, TValue> Where(Func<TValue, bool> expression)
        {
            return new UnionPatternCaseWhereHandler<TMatcher, TValue>(expression, _recorder, _matcher);
        }

        public TMatcher Do(Action<TValue> action)
        {
            _recorder(x => true, action);
            return _matcher;
        }
    }
}