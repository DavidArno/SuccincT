using System;
using System.Collections.Generic;
using SuccincT.Options;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3}.Match{TResult}()...Else(). Whilst this is a public
    /// class (as the user needs access to Result()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TResult>
    {
        private readonly Union<T1, T2, T3> _union;
        private readonly Dictionary<Variant, Func<Option<TResult>>> _resultActions;
        private readonly Func<Union<T1, T2, T3>, TResult> _elseAction;

        internal UnionOfThreePatternMatcherAfterElse(Union<T1, T2, T3> union,
                                              MatchFunctionSelector<T1, TResult> case1FunctionSelector,
                                              MatchFunctionSelector<T2, TResult> case2FunctionSelector,
                                              MatchFunctionSelector<T3, TResult> case3FunctionSelector,
                                              Func<Union<T1, T2, T3>, TResult> elseAction)
        {
            _union = union;
            _elseAction = elseAction;
            _resultActions = new Dictionary<Variant, Func<Option<TResult>>>
            {
                { Variant.Case1, () => case1FunctionSelector.DetermineResult(_union.Case1) },
                { Variant.Case2, () => case2FunctionSelector.DetermineResult(_union.Case2) },
                { Variant.Case3, () => case3FunctionSelector.DetermineResult(_union.Case3) }
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