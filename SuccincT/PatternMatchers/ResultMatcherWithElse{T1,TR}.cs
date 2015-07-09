using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcherWithElse<T1, TResult>
    {
        private readonly MatchFunctionSelector<T1, TResult> _selector;
        private readonly Func<T1, TResult> _elseAction;
        private readonly T1 _value;

        internal ResultMatcherWithElse(MatchFunctionSelector<T1, TResult> selector, Func<T1, TResult> elseAction, T1 value)
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