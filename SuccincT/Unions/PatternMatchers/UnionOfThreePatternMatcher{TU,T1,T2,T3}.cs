using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfThreePatternMatcher<T1, T2, T3>
    {
        private readonly Union<T1, T2, T3> _union;

        private readonly UnionCaseActionSelector<T1> _case1ActionSelector =
            new UnionCaseActionSelector<T1>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2> _case2ActionSelector =
            new UnionCaseActionSelector<T2>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly UnionCaseActionSelector<T3> _case3ActionSelector =
            new UnionCaseActionSelector<T3>(
                x => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly Dictionary<Variant, Action> _resultActions;

        internal UnionOfThreePatternMatcher(Union<T1, T2, T3> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Action>
            {
                {Variant.Case1, () => _case1ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case2)},
                {Variant.Case3, () => _case3ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case3)}
            };
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T1> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T1>(RecordAction,
                                                                                           this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T2> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T2>(RecordAction,
                                                                                           this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T3> Case3()
        {
            return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T3>(RecordAction,
                                                                                           this);
        }

        public UnionOfThreePatternMatcherAfterElse<T1, T2, T3> Else(Action<Union<T1, T2, T3>> elseAction)
        {
            return new UnionOfThreePatternMatcherAfterElse<T1, T2, T3>(_union,
                                                                       _case1ActionSelector,
                                                                       _case2ActionSelector,
                                                                       _case3ActionSelector,
                                                                       elseAction);
        }

        public void Exec()
        {
            _resultActions[_union.Case]();
        }

        private void RecordAction(Func<T1, bool> test, Action<T1> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T2, bool> test, Action<T2> action)
        {
            _case2ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T3, bool> test, Action<T3> action)
        {
            _case3ActionSelector.AddTestAndAction(test, action);
        }
    }
}