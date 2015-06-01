using System;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionPatternCaseWhereHandler<TMatcher, TValue, TReturn>
    {
        private readonly Func<TValue, bool> _expression;
        private readonly Action<Func<TValue, bool>, Func<TValue, TReturn>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseWhereHandler(Func<TValue, bool> expression,
                                              Action<Func<TValue, bool>, Func<TValue, TReturn>> recorder,
                                              TMatcher matcher)
        {
            _expression = expression;
            _recorder = recorder;
            _matcher = matcher;
        }

        public TMatcher Do(Func<TValue, TReturn> action)
        {
            _recorder(_expression, action);
            return _matcher;
        }
    }
}