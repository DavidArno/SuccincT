using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionOfTwoPatternMatcher<TUnion, T1, T2, TReturn> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;

        private readonly UnionCaseActionSelector<T1, TReturn> _case1ActionSelector =
            new UnionCaseActionSelector<T1, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2, TReturn> _case2ActionSelector =
            new UnionCaseActionSelector<T2, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly Dictionary<Variant, Func<TReturn>> _resultActions;

        internal UnionOfTwoPatternMatcher(TUnion union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TReturn>>
            {
                { Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1) },
                { Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2) }
            };
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<TUnion, T1, T2, TReturn>, T1, TReturn> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<TUnion, T1, T2, TReturn>, T1, TReturn>(
                RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<TUnion, T1, T2, TReturn>, T2, TReturn> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<TUnion, T1, T2, TReturn>, T2, TReturn>(
                RecordAction, this);
        }

        public UnionOfTwoPatternMatcherAfterElse<TUnion, T1, T2, TReturn> Else(Func<TUnion, TReturn> elseAction)
        {
            return new UnionOfTwoPatternMatcherAfterElse<TUnion, T1, T2, TReturn>(_union,
                                                                                  _case1ActionSelector,
                                                                                  _case2ActionSelector,
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
    }
}