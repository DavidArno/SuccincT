using System;
using System.Collections.Generic;
using SuccincT.Options;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3, TReturn> where TUnion : Union<T1, T2, T3>
    {
        private readonly TUnion _union;
        private readonly Dictionary<Variant, Func<Option<TReturn>>> _resultActions;
        private readonly Func<TUnion, TReturn> _elseAction;

        internal UnionOfThreePatternMatcherAfterElse(TUnion union,
                                              UnionCaseActionSelector<T1, TReturn> case1ActionSelector,
                                              UnionCaseActionSelector<T2, TReturn> case2ActionSelector,
                                              UnionCaseActionSelector<T3, TReturn> case3ActionSelector,
                                              Func<TUnion, TReturn> elseAction)
        {
            _union = union;
            _elseAction = elseAction;
            _resultActions = new Dictionary<Variant, Func<Option<TReturn>>>
            {
                { Variant.Case1, () => case1ActionSelector.DetermineResult(_union.Case1) },
                { Variant.Case2, () => case2ActionSelector.DetermineResult(_union.Case2) },
                { Variant.Case3, () => case3ActionSelector.DetermineResult(_union.Case3) }
            };
        }

        public TReturn Result()
        {
            return _resultActions[_union.Case]().Match<TReturn>()
                                .Some().Do(x => x)
                                .None().Do(() => _elseAction(_union))
                                .Result();
        }
    }
}