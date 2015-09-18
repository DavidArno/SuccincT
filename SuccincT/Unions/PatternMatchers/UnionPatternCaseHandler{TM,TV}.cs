using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1...}.Match().CaseN(). Whilst this is a public
    /// class (as the user needs access to Of() and Where()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionPatternCaseHandler<TMatcher, TValue>
    {
        private readonly Action<Func<TValue, bool>, Action<TValue>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseHandler(Action<Func<TValue, bool>, Action<TValue>> recorder, TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForActionHandler<TMatcher, TValue> Of(TValue value) => 
            new WithForActionHandler<TMatcher, TValue>(value, _recorder, _matcher);

        public WhereForActionHandler<TMatcher, TValue> Where(Func<TValue, bool> expression) => 
            new WhereForActionHandler<TMatcher, TValue>(expression, _recorder, _matcher);

        public TMatcher Do(Action<TValue> action)
        {
            _recorder(x => true, action);
            return _matcher;
        }
    }
}