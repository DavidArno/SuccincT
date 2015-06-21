using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfTwoPatternMatcher<T1, T2, TReturn>
    {
        private readonly Union<T1, T2> _union;

        private readonly MatchActionSelector<T1, TReturn> _case1ActionSelector =
            new MatchActionSelector<T1, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2, TReturn> _case2ActionSelector =
            new MatchActionSelector<T2, TReturn>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly Dictionary<Variant, Func<TReturn>> _resultActions;

        internal UnionOfTwoPatternMatcher(Union<T1, T2> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TReturn>>
            {
                {Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)}
            };
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TReturn>, T1, TReturn> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TReturn>, T1, TReturn>(RecordAction,
                                                                                                       this);
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TReturn>, T2, TReturn> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TReturn>, T2, TReturn>(RecordAction,
                                                                                                       this);
        }

        public UnionOfTwoPatternMatcherAfterElse<T1, T2, TReturn> Else(Func<Union<T1, T2>, TReturn> elseAction)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T1, T2, TReturn>(_union,
                                                                          _case1ActionSelector,
                                                                          _case2ActionSelector,
                                                                          elseAction);
        }

        public UnionOfTwoPatternMatcherAfterElse<T1, T2, TReturn> Else(TReturn elseValue)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T1, T2, TReturn>(_union,
                                                                          _case1ActionSelector,
                                                                          _case2ActionSelector,
                                                                          x => elseValue);
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