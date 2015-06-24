using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>
    {
        private readonly Union<T1, T2, T3, T4> _union;

        private readonly MatchActionSelector<T1, TReturn> _case1ActionSelector =
            new MatchActionSelector<T1, TReturn>(
                _ => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2, TReturn> _case2ActionSelector =
            new MatchActionSelector<T2, TReturn>(
                _ => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly MatchActionSelector<T3, TReturn> _case3ActionSelector =
            new MatchActionSelector<T3, TReturn>(
                _ => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly MatchActionSelector<T4, TReturn> _case4ActionSelector =
            new MatchActionSelector<T4, TReturn>(
                _ => { throw new NoMatchException("No match action defined for union with Case4 value"); });

        private readonly Dictionary<Variant, Func<TReturn>> _resultActions;

        internal UnionOfFourPatternMatcher(Union<T1, T2, T3, T4> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TReturn>>
            {
                {Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)},
                {Variant.Case3, () => _case3ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case3)},
                {Variant.Case4, () => _case4ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case4)}
            };
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T1, TReturn> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T1, TReturn>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T2, TReturn> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T2, TReturn>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T3, TReturn> Case3()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T3, TReturn>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T4, TReturn> Case4()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TReturn>, T4, TReturn>
                (RecordAction, this);
        }

        //public UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TReturn> Else(Func<Union<T1, T2, T3>, TReturn> elseAction)
        //{
        //    return new UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TReturn>(_union,
        //                                                                        _case1ActionSelector,
        //                                                                        _case2ActionSelector,
        //                                                                        _case3ActionSelector,
        //                                                                        elseAction);
        //}

        //public UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TReturn> Else(TReturn elseValue)
        //{
        //    return new UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TReturn>(_union,
        //                                                                        _case1ActionSelector,
        //                                                                        _case2ActionSelector,
        //                                                                        _case3ActionSelector,
        //                                                                        x => elseValue);
        //}

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

        private void RecordAction(Func<T4, bool> test, Func<T4, TReturn> action)
        {
            _case4ActionSelector.AddTestAndAction(test, action);
        }
    }
}