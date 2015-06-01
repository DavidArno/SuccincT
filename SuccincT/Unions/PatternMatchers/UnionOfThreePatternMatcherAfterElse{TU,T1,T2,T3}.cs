using System;
using System.Collections.Generic;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3> where TUnion : Union<T1, T2, T3>
    {
        private readonly TUnion _union;
        private readonly Dictionary<Variant, Action> _execActions;
        private readonly Action<TUnion> _elseAction;

        internal UnionOfThreePatternMatcherAfterElse(TUnion union,
                                              UnionCaseActionSelector<T1> case1ActionSelector,
                                              UnionCaseActionSelector<T2> case2ActionSelector,
                                              UnionCaseActionSelector<T3> case3ActionSelector,
                                              Action<TUnion> elseAction)
        {
            _union = union;
            _elseAction = elseAction;
            _execActions = new Dictionary<Variant, Action>
            {
                { Variant.Case1, () => Exec(case1ActionSelector, _union.Case1) },
                { Variant.Case2, () => Exec(case2ActionSelector, _union.Case2) },
                { Variant.Case3, () => Exec(case3ActionSelector, _union.Case3) }
            };
        }

        public void Exec()
        {
            _execActions[_union.Case]();
        }

        private void Exec<T>(UnionCaseActionSelector<T> selector, T value)
        {
            var matchedResult = selector.FindMatchedActionOrNone(value);

            matchedResult.Match()
                         .Some().Do(x => x(value))
                         .None().Do(() => _elseAction(_union))
                         .Exec();
        }
    }
}