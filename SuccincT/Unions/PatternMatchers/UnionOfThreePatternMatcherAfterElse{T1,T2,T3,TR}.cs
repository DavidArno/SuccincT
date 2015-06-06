using System;
using System.Collections.Generic;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TReturn>
    {
        private readonly Union<T1, T2, T3> _union;
        private readonly Dictionary<Variant, Func<Option<TReturn>>> _resultActions;
        private readonly Func<Union<T1, T2, T3>, TReturn> _elseAction;

        internal UnionOfThreePatternMatcherAfterElse(Union<T1, T2, T3> union,
                                              MatchActionSelector<T1, TReturn> case1ActionSelector,
                                              MatchActionSelector<T2, TReturn> case2ActionSelector,
                                              MatchActionSelector<T3, TReturn> case3ActionSelector,
                                              Func<Union<T1, T2, T3>, TReturn> elseAction)
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