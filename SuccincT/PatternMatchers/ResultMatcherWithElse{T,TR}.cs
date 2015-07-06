using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcherWithElse<T, TResult>
    {
        private readonly MatchFunctionSelector<T, TResult> _selector;
        private readonly Func<T, TResult> _elseAction;
        private readonly T _value;

        internal ResultMatcherWithElse(MatchFunctionSelector<T, TResult> selector, Func<T, TResult> elseAction, T value)
        {
            _selector = selector;
            _elseAction = elseAction;
            _value = value;
        }

        public TResult Result()
        {
            var matchedResult = _selector.DetermineResult(_value);

            return matchedResult.Match<TResult>()
                                .Some().Do(x => x)
                                .None().Do(() => _elseAction(_value))
                                .Result();
        }
    }
}