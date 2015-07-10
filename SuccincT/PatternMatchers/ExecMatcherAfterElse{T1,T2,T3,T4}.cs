using System;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// Fluent class created by an invocation of Else() when handling a pattern definition for T1, T2 and T3 that ends 
    /// in Exec(). Whilst this is a public class (as the user needs access to Exec()), it has an internal constructor as
    /// it's intended for pattern matching internal usage only.
    /// </summary>
    public sealed class ExecMatcherAfterElse<T1, T2, T3, T4>
    {
        private readonly MatchActionSelector<T1, T2, T3, T4> _selector;
        private readonly Action<T1, T2, T3, T4> _elseAction;
        private readonly Tuple<T1, T2, T3, T4> _value;

        internal ExecMatcherAfterElse(MatchActionSelector<T1, T2, T3, T4> selector,
                                      Action<T1, T2, T3, T4> elseAction,
                                      Tuple<T1, T2, T3, T4> value)
        {
            _selector = selector;
            _elseAction = elseAction;
            _value = value;
        }

        public void Exec()
        {
            var matchedResult = _selector.FindMatchedActionOrNone(_value.Item1, _value.Item2, _value.Item3, _value.Item4);

            matchedResult.Match()
                         .Some().Do(x => x(_value.Item1, _value.Item2, _value.Item3, _value.Item4))
                         .None().Do(() => _elseAction(_value.Item1, _value.Item2, _value.Item3, _value.Item4))
                         .Exec();
        }
    }
}