using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3,T4}.Match(). Whilst this is a public
    /// class (as the user needs access to Case1-4(), Else() and Exec()), it has an internal constructor as it's
    /// intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfFourPatternMatcher<T1, T2, T3, T4>
    {
        private readonly Union<T1, T2, T3, T4> _union;

        private readonly MatchActionSelector<T1> _case1ActionSelector =
            new MatchActionSelector<T1>(
                _ => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2> _case2ActionSelector =
            new MatchActionSelector<T2>(
                _ => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly MatchActionSelector<T3> _case3ActionSelector =
            new MatchActionSelector<T3>(
                _ => { throw new NoMatchException("No match action defined for union with Case3 value"); });

        private readonly MatchActionSelector<T4> _case4ActionSelector =
            new MatchActionSelector<T4>(
                _ => { throw new NoMatchException("No match action defined for union with Case4 value"); });

        private readonly Dictionary<Variant, Action> _execActions;

        internal UnionOfFourPatternMatcher(Union<T1, T2, T3, T4> union)
        {
            _union = union;
            _execActions = new Dictionary<Variant, Action>
            {
                {Variant.Case1, () => _case1ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case2)},
                {Variant.Case3, () => _case3ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case3)},
                {Variant.Case4, () => _case4ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case4)}
            };
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T1> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T1>(RecordAction,
                                                                                              this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T2> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T2>(RecordAction,
                                                                                              this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T3> Case3()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T3>(RecordAction,
                                                                                              this);
        }

        public UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T4> Case4()
        {
            return new UnionPatternCaseHandler<UnionOfFourPatternMatcher<T1, T2, T3, T4>, T4>(RecordAction,
                                                                                              this);
        }

        public UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4> Else(Action<Union<T1, T2, T3, T4>> elseAction)
        {
            return new UnionOfFourPatternMatcherAfterElse<T1, T2, T3, T4>(_union,
                                                                          _case1ActionSelector,
                                                                          _case2ActionSelector,
                                                                          _case3ActionSelector,
                                                                          _case4ActionSelector,
                                                                          elseAction);
        }

        public void Exec()
        {
            _execActions[_union.Case]();
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

        private void RecordAction(Func<T4, bool> test, Action<T4> action)
        {
            _case4ActionSelector.AddTestAndAction(test, action);
        }
    }
}