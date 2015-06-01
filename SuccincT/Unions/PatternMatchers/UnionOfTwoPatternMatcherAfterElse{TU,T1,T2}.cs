using System;
using System.Collections.Generic;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfTwoPatternMatcherAfterElse<TUnion, T1, T2> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;
        private readonly Dictionary<Variant, Action> _execActions;
        private readonly Action<TUnion> _elseAction;

        internal UnionOfTwoPatternMatcherAfterElse(TUnion union,
                                              UnionCaseActionSelector<T1> case1ActionSelector,
                                              UnionCaseActionSelector<T2> case2ActionSelector,
                                              Action<TUnion> elseAction)
        {
            _union = union;
            _elseAction = elseAction;
            _execActions = new Dictionary<Variant, Action>
            {
                { Variant.Case1, () => Exec(case1ActionSelector, _union.Case1) },
                { Variant.Case2, () => Exec(case2ActionSelector, _union.Case2) }
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