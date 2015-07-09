using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// Fluent class created by an invocation of Else() when handling a pattern definition for T that ends in Exec().
    /// Whilst this is a public class (as the user needs access to Exec()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class ExecMatcherAfterElse<T1>
    {
        private readonly MatchActionSelector<T1> _selector;
        private readonly Action<T1> _elseAction;
        private readonly T1 _value;

        internal ExecMatcherAfterElse(MatchActionSelector<T1> selector, Action<T1> elseAction, T1 value)
        {
            _selector = selector;
            _elseAction = elseAction;
            _value = value;
        }

        public void Exec()
        {
            var matchedResult = _selector.FindMatchedActionOrNone(_value);

            matchedResult.Match()
                         .Some().Do(x => x(_value))
                         .None().Do(() => _elseAction(_value))
                         .Exec();
        }
    }
}