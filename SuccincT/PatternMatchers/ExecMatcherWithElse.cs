using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ExecMatcherWithElse<T>
    {
        private readonly MatchActionSelector<T> _selector;
        private readonly Action<T> _elseAction;
        private readonly T _value;

        internal ExecMatcherWithElse(MatchActionSelector<T> selector, Action<T> elseAction, T value)
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