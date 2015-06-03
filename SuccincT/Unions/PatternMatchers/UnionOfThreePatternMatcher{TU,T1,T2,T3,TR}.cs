using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn> where TUnion : Union<T1, T2, T3>
    {
        private readonly TUnion _union;

        private readonly UnionCaseActionSelector<T1, TReturn> _case1ActionSelector =
            new UnionCaseActionSelector<T1, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2, TReturn> _case2ActionSelector =
            new UnionCaseActionSelector<T2, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly UnionCaseActionSelector<T3, TReturn> _case3ActionSelector =
            new UnionCaseActionSelector<T3, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly Dictionary<Variant, Func<TReturn>> _resultActions;

        internal UnionOfThreePatternMatcher(TUnion union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TReturn>>
            {
                { Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1) },
                { Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2) },
                { Variant.Case3, () => _case3ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case3) }
            };
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T1, TReturn> Case1()
        {
            return 
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T1, TReturn>(RecordAction,
                                                                                                           this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T2, TReturn> Case2()
        {
            return
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T2, TReturn>(RecordAction,
                                                                                                           this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T3, TReturn> Case3()
        {
            return
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<TUnion, T1, T2, T3, TReturn>, T3, TReturn>(RecordAction,
                                                                                                           this);
        }

        public UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3, TReturn> Else(Func<TUnion, TReturn> elseAction)
        {
            return new UnionOfThreePatternMatcherAfterElse<TUnion, T1, T2, T3, TReturn>(_union,
                                                                                 _case1ActionSelector,
                                                                                 _case2ActionSelector,
                                                                                 _case3ActionSelector,
                                                                                 elseAction);
        }

        public TReturn Result()
        {
            return _resultActions[_union.Case]();
        }

        private void RecordAction(Func<T1, bool> test, Func<T1, TReturn> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T2, bool> test, Func<T2, TReturn> action)
        {
            _case2ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T3, bool> test, Func<T3, TReturn> action)
        {
            _case3ActionSelector.AddTestAndAction(test, action);
        }
    }
}