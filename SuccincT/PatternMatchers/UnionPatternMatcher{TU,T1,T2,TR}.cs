using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public class UnionPatternMatcher<TUnion, T1, T2, TReturn> where TUnion : Union<T1, T2>
    {
        private readonly TUnion _union;

        private readonly UnionCaseActionSelector<T1, TReturn> _case1ActionSelector =
            new UnionCaseActionSelector<T1, TReturn>(
                x => { throw new InvalidOperationException("No match action defined for union with Case1 value"); });

        private readonly UnionCaseActionSelector<T2, TReturn> _case2ActionSelector =
            new UnionCaseActionSelector<T2, TReturn>(
                x => { throw new InvalidOperationException("No match action defined for union with Case2 value"); });

        internal UnionPatternMatcher(TUnion union)
        {
            _union = union;
        }

        public UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2, TReturn>, T1, TReturn> Case1()
        {
            return new UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2, TReturn>, T1, TReturn>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2, TReturn>, T2, TReturn> Case2()
        {
            return new UnionPatternCaseHandler<UnionPatternMatcher<TUnion, T1, T2, TReturn>, T2, TReturn>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternMatcherAfterElse<TUnion, T1, T2, TReturn> Else(Func<TUnion, TReturn> elseAction)
        {
            return new UnionPatternMatcherAfterElse<TUnion, T1, T2, TReturn>(_union,
                                                                             _case1ActionSelector,
                                                                             _case2ActionSelector,
                                                                             elseAction);
        }

        public TReturn Result()
        {
            return _union.Case == Variant.Case1 
                ? _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1) 
                : _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2);
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