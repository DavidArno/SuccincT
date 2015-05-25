using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public class UnionPatternMatcherAfterElse<TUnion, T1, T2, TReturn> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;
        private readonly UnionCaseActionSelector<T1, TReturn> _case1ActionSelector;
        private readonly UnionCaseActionSelector<T2, TReturn> _case2ActionSelector;
        private readonly Func<TUnion, TReturn> _elseAction;

        internal UnionPatternMatcherAfterElse(TUnion union,
                                              UnionCaseActionSelector<T1, TReturn> case1ActionSelector,
                                              UnionCaseActionSelector<T2, TReturn> case2ActionSelector,
                                              Func<TUnion, TReturn> elseAction)
        {
            _union = union;
            _case1ActionSelector = case1ActionSelector;
            _case2ActionSelector = case2ActionSelector;
            _elseAction = elseAction;
        }

        public TReturn Result()
        {
            var matchedResult = _union.Case == Variant.Case1 
                ? _case1ActionSelector.DetermineResult(_union.Case1) 
                : _case2ActionSelector.DetermineResult(_union.Case2);

            return matchedResult.Match<TReturn>()
                                .Some().Do(x => x)
                                .None().Do(() => _elseAction(_union))
                                .Result();
        }
    }
}