using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public class UnionPatternMatcherAfterElse<TUnion, T1, T2> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;
        private readonly UnionCaseActionSelector<T1> _case1ActionSelector;
        private readonly UnionCaseActionSelector<T2> _case2ActionSelector;
        private readonly Action<TUnion> _elseAction;

        internal UnionPatternMatcherAfterElse(TUnion union,
                                              UnionCaseActionSelector<T1> case1ActionSelector,
                                              UnionCaseActionSelector<T2> case2ActionSelector,
                                              Action<TUnion> elseAction)
        {
            _union = union;
            _case1ActionSelector = case1ActionSelector;
            _case2ActionSelector = case2ActionSelector;
            _elseAction = elseAction;
        }

        public void Exec()
        {
            if (_union.Case == Variant.Case1)
            {
                Exec(_case1ActionSelector, _union.Case1);
            }
            else
            {
                Exec(_case2ActionSelector, _union.Case2);
            }
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