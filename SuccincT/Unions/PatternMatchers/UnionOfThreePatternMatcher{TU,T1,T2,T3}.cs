using System;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfThreePatternMatcher<TUnion, T1, T2, T3> where TUnion : Union<T1, T2, T3>
    {
        private readonly TUnion _union;

        private readonly UnionCaseActionSelector<T1> _case1ActionSelector =
            new UnionCaseActionSelector<T1>(
                x => { throw new InvalidOperationException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2> _case2ActionSelector =
            new UnionCaseActionSelector<T2>(
                x => { throw new InvalidOperationException("No match action defined for union with Case2 value"); });

        private readonly UnionCaseActionSelector<T3> _case3ActionSelector =
            new UnionCaseActionSelector<T3>(
                x => { throw new InvalidOperationException("No match action defined for union with Case3 value"); });

        internal UnionOfThreePatternMatcher(TUnion union)
        {
            _union = union;
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3>, T1> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3>, T1>(RecordAction,
                                                                                                   this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3>, T2> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3>, T2>(RecordAction,
                                                                                                   this);
        }

        public UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3> Else(Action<TUnion> elseAction)
        {
            return new UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3>(_union,
                                                                             _case1ActionSelector,
                                                                             _case2ActionSelector,
                                                                             _case3ActionSelector,
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