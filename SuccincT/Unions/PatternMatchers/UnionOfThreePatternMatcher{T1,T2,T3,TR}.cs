using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3}.Match{TResult}(). Whilst this is a public
    /// class (as the user needs access to Case1-3(), Else() and Result()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfThreePatternMatcher<T1, T2, T3, TResult>
    {
        private readonly Union<T1, T2, T3> _union;

        private readonly MatchActionSelector<T1, TResult> _case1ActionSelector =
            new MatchActionSelector<T1, TResult>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2, TResult> _case2ActionSelector =
            new MatchActionSelector<T2, TResult>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly MatchActionSelector<T3, TResult> _case3ActionSelector =
            new MatchActionSelector<T3, TResult>(
                x => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly Dictionary<Variant, Func<TResult>> _resultActions;

        internal UnionOfThreePatternMatcher(Union<T1, T2, T3> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TResult>>
            {
                {Variant.Case1, () => _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)},
                {Variant.Case3, () => _case3ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case3)}
            };
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T1, TResult> Case1()
        {
            return
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T1, TResult>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T2, TResult> Case2()
        {
            return
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T2, TResult>(RecordAction,
                                                                                                          this);
        }

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T3, TResult> Case3()
        {
            return
                new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3, TResult>, T3, TResult>(RecordAction,
                                                                                                          this);
        }

        public UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TResult> Else(Func<Union<T1, T2, T3>, TResult> elseAction)
        {
            return new UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TResult>(_union,
                                                                                _case1ActionSelector,
                                                                                _case2ActionSelector,
                                                                                _case3ActionSelector,
                                                                                elseAction);
        }

        public UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TResult> Else(TResult elseValue)
        {
            return new UnionOfThreePatternMatcherAfterElse<T1, T2, T3, TResult>(_union,
                                                                                _case1ActionSelector,
                                                                                _case2ActionSelector,
                                                                                _case3ActionSelector,
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
    }
}