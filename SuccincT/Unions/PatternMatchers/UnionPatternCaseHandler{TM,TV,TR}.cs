using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1...}.Match{TResult}().CaseN(). Whilst this is a public
    /// class (as the user needs access to Of() and Where()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionPatternCaseHandler<TMatcher, TValue, TResult>
    {
        private readonly Action<Func<TValue, bool>, Func<TValue, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<TValue, bool>, Func<TValue, TResult>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public MatchExpressionHandler<TMatcher, TValue, TResult> Of(TValue value)
        {
            return new MatchExpressionHandler<TMatcher, TValue, TResult>(value, _recorder, _matcher);
        }

        public MatchWhereHandler<TMatcher, TValue, TResult> Where(Func<TValue, bool> expression)
        {
            return new MatchWhereHandler<TMatcher, TValue, TResult>(expression, _recorder, _matcher);
        }

        public TMatcher Do(Func<TValue, TResult> action)
        {
            _recorder(x => true, action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(x => true, x => value);
            return _matcher;
        }
    }
}