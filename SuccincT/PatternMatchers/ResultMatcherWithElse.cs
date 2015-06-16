using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcherWithElse<T, TValue>
    {
        private readonly MatchActionSelector<T, TValue> _selector;
        private readonly Func<T, TValue> _elseAction;
        private readonly T _value;

        internal ResultMatcherWithElse(MatchActionSelector<T, TValue> selector, Func<T, TValue> elseAction, T value)
        {
            _selector = selector;
            _elseAction = elseAction;
            _value = value;
        }

        public TValue Result()
        {
            var matchedResult = _selector.DetermineResult(_value);

            return matchedResult.Match<TValue>()
                                .Some().Do(x => x)
                                .None().Do(() => _elseAction(_value))
                                .Result();
        }
    }
}