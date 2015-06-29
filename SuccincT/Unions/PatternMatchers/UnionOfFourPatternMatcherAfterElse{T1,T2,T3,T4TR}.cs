using System;
using System.Collections.Generic;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3,T4}.Match{TResult}()...Else(). Whilst this is a public
    /// class (as the user needs access to Result()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4, TResult>
    {
        private readonly Union<T1, T2, T3, T4> _union;
        private readonly Dictionary<Variant, Func<Option<TResult>>> _resultActions;
        private readonly Func<Union<T1, T2, T3, T4>, TResult> _elseAction;

        internal UnionOfFourPatternMatcherAfterElse(Union<T1, T2, T3, T4> union,
                                                    MatchActionSelector<T1, TResult> case1ActionSelector,
                                                    MatchActionSelector<T2, TResult> case2ActionSelector,
                                                    MatchActionSelector<T3, TResult> case3ActionSelector,
                                                    MatchActionSelector<T4, TResult> case4ActionSelector,
                                                    Func<Union<T1, T2, T3, T4>, TResult> elseAction)
        {
            _union = union;
            _elseAction = elseAction;
            _resultActions = new Dictionary<Variant, Func<Option<TResult>>>
            {
                {Variant.Case1, () => case1ActionSelector.DetermineResult(_union.Case1)},
                {Variant.Case2, () => case2ActionSelector.DetermineResult(_union.Case2)},
                {Variant.Case3, () => case3ActionSelector.DetermineResult(_union.Case3)},
                {Variant.Case4, () => case4ActionSelector.DetermineResult(_union.Case4)}
            };
        }

        public TResult Result()
        {
            return _resultActions[_union.Case]().Match<TResult>()
                                                .Some().Do(x => x)
                                                .None().Do(() => _elseAction(_union))
                                                .Result();
        }
    }
}