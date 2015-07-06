using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1...}.Match{TResult}().CaseN(). Whilst this is a public
    /// class (as the user needs access to Of() and Where()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionPatternCaseHandler<TMatcher, T, TResult>
    {
        private readonly Action<Func<T, bool>, Func<T, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<T, bool>, Func<T, TResult>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForFuncHandler<TMatcher, T, TResult> Of(T value)
        {
            return new WithForFuncHandler<TMatcher, T, TResult>(value, _recorder, _matcher);
        }

        public WhereForFuncHandler<TMatcher, T, TResult> Where(Func<T, bool> expression)
        {
            return new WhereForFuncHandler<TMatcher, T, TResult>(expression, _recorder, _matcher);
        }

        public TMatcher Do(Func<T, TResult> action)
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