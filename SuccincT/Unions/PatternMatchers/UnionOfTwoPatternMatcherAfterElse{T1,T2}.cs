using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfTwoPatternMatcherAfterElse<T1, T2>
    {
        private readonly Union<T1, T2> _union;
        private readonly Dictionary<Variant, Action> _execActions;
        private readonly Action<Union<T1, T2>> _elseAction;

        internal UnionOfTwoPatternMatcherAfterElse(Union<T1, T2> union,
                                              MatchActionSelector<T1> case1ActionSelector,
                                              MatchActionSelector<T2> case2ActionSelector,
                                              Action<Union<T1, T2>> elseAction)
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

        private void Exec<T>(MatchActionSelector<T> selector, T value)
        {
            var matchedResult = selector.FindMatchedActionOrNone(value);

            matchedResult.Match()
                         .Some().Do(x => x(value))
                         .None().Do(() => _elseAction(_union))
                         .Exec();
        }
    }
}