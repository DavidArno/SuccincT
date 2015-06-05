using System;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionPatternCaseWhereHandler<TMatcher, TValue>
    {
        private readonly Func<TValue, bool> _expression;
        private readonly Action<Func<TValue, bool>, Action<TValue>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseWhereHandler(Func<TValue, bool> expression,
                                              Action<Func<TValue, bool>, Action<TValue>> recorder,
                                              TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Action<TValue> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }
    }
}