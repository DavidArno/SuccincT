using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public class UnionPatternMatcher<TUnion, T1, T2> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;

        private readonly UnionCaseActionSelector<T1> _case1ActionSelector =
            new UnionCaseActionSelector<T1>(
                x => { throw new InvalidOperationException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2> _case2ActionSelector =
            new UnionCaseActionSelector<T2>(
                x => { throw new InvalidOperationException("No match action defined for union with Case2 value"); });

        internal UnionPatternMatcher(TUnion union)
        {
            _union = union;
        }

        public UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2>, T1> Case1()
        {
            return new UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2>, T1>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2>, T2> Case2()
        {
            return new UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2>, T2>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternMatcherAfterElse<TUnion, T1, T2> Else(Action<TUnion> elseAction)
        {
            return new UnionPatternMatcherAfterElse<TUnion, T1, T2>(_union,
                                                                             _case1ActionSelector,
                                                                             _case2ActionSelector,
                                                                             elseAction);
        }

        public void Exec()
        {
            if (_union.Case == Variant.Case1)
            {
                _case1ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case1);
            }
            else
            {
                _case2ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case2);
            }
        }

        private void RecordAction(Func<T1, bool> test, Action<T1> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T2, bool> test, Action<T2> action)
        {
            _case2ActionSelector.AddTestAndAction(test, action);
        }
    }
}