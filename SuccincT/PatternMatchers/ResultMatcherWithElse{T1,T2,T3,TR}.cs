using System;

namespace SuccincT.PatternMatchers
{
    public sealed class ResultMatcherWithElse<T1, T2, T3, TResult>
    {
        private readonly MatchFunctionSelector<T1, T2, T3, TResult> _selector;
        private readonly Func<T1, T2, T3, TResult> _elseAction;
        private readonly Tuple<T1, T2, T3> _value;

        internal ResultMatcherWithElse(MatchFunctionSelector<T1, T2, T3, TResult> selector,
                                       Func<T1, T2, T3, TResult> elseAction,
                                       Tuple<T1, T2, T3> value)
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
                                .None().Do(() => _elseAction(_value.Item1, _value.Item2, _value.Item3))
                                .Result();
        }
    }
}