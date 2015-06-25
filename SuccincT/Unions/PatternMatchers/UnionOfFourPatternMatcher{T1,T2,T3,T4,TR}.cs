using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3,T4}.Match{TResult}(). Whilst this is a public
    /// class (as the user needs access to Case1-4(), Else() and Result()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>
    {
        private readonly Union<T1, T2, T3, T4> _union;

        private readonly MatchActionSelector<T1, TResult> _case1ActionSelector =
            new MatchActionSelector<T1, TResult>(
                _ => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2, TResult> _case2ActionSelector =
            new MatchActionSelector<T2, TResult>(
                _ => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly MatchActionSelector<T3, TResult> _case3ActionSelector =
            new MatchActionSelector<T3, TResult>(
                _ => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly MatchActionSelector<T4, TResult> _case4ActionSelector =
            new MatchActionSelector<T4, TResult>(
                _ => { throw new NoMatchException("No match action defined for union with Case4 value"); });

        private readonly Dictionary<Variant, Func<TResult>> _resultActions;

        internal UnionOfFourPatternMatcher(Union<T1, T2, T3, T4> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TResult>>
            {
                {Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)},
                {Variant.Case3, () => _case3ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case3)},
                {Variant.Case4, () => _case4ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case4)}
            };
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T1, TResult> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T1, TResult>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T2, TResult> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T2, TResult>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T3, TResult> Case3()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T3, TResult>
                (RecordAction, this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T4, TResult> Case4()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4, TResult>, T4, TResult>
                (RecordAction, this);
        }

        public UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4, TResult> Else(
            Func<Union<T1, T2, T3, T4>, TResult> elseAction)
        {
            return new UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4, TResult>(_union,
                                                                                   _case1ActionSelector,
                                                                                   _case2ActionSelector,
                                                                                   _case3ActionSelector,
                                                                                   _case4ActionSelector,
                                                                                   elseAction);
        }

        public UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4, TResult> Else(TResult elseValue)
        {
            return new UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4, TResult>(_union,
                                                                                   _case1ActionSelector,
                                                                                   _case2ActionSelector,
                                                                                   _case3ActionSelector,
                                                                                   _case4ActionSelector,
                                                                                   x => elseValue);
        }

        public TResult Result()
        {
            return _resultActions[_union.Case]();
        }

        private void RecordAction(Func<T1, bool> test, Func<T1, TResult> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T2, bool> test, Func<T2, TResult> action)
        {
            _case2ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T3, bool> test, Func<T3, TResult> action)
        {
            _case3ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T4, bool> test, Func<T4, TResult> action)
        {
            _case4ActionSelector.AddTestAndAction(test, action);
        }
    }
}