using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3}.Match(). Whilst this is a public
    /// class (as the user needs access to Case1-3(), CaseOf(), Else() and Result()), it has an 
    /// internal constructor as it is intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfThreePatternMatcher<T1, T2, T3>
    {
        private readonly Union<T1, T2, T3> _union;

        private readonly MatchActionSelector<T1> _case1ActionSelector =
            new MatchActionSelector<T1>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2> _case2ActionSelector =
            new MatchActionSelector<T2>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly MatchActionSelector<T3> _case3ActionSelector =
            new MatchActionSelector<T3>(
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

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T1> Case1() =>
            new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T1>(RecordAction,
                                                                                    this);

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T2> Case2() =>
            new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T2>(RecordAction,
                                                                                    this);

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T3> Case3() =>
            new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T3>(RecordAction,
                                                                                    this);

        public UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T> CaseOf<T>()
        {
            if (typeof(T) == typeof(T1))
            {
                return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T1>(RecordAction, this)
                    as UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T>;
            }

            if (typeof(T) == typeof(T2))
            {
                return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T2>(RecordAction, this)
                    as UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T>;
            }

            if (typeof(T) == typeof(T3))
            {
                return new UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T3>(RecordAction, this)
                    as UnionPatternCaseHandler<UnionOfThreePatternMatcher<T1, T2, T3>, T>;
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        public UnionOfThreePatternMatcherAfterElse<T1, T2, T3> Else(Action<Union<T1, T2, T3>> elseAction) =>
            new UnionOfThreePatternMatcherAfterElse<T1, T2, T3>(_union,
                                                                _case1ActionSelector,
                                                                _case2ActionSelector,
                                                                _case3ActionSelector,
                                                                elseAction);

        public UnionOfThreePatternMatcherAfterElse<T1, T2, T3> IgnoreElse() =>
            new UnionOfThreePatternMatcherAfterElse<T1, T2, T3>(_union,
                                                                _case1ActionSelector,
                                                                _case2ActionSelector,
                                                                _case3ActionSelector,
                                                                x => { });

        public void Exec() => _resultActions[_union.Case]();

        private void RecordAction(Func<T1, bool> test, Action<T1> action) =>
            _case1ActionSelector.AddTestAndAction(test, action);

        private void RecordAction(Func<T2, bool> test, Action<T2> action) =>
            _case2ActionSelector.AddTestAndAction(test, action);

        private void RecordAction(Func<T3, bool> test, Action<T3> action) =>
            _case3ActionSelector.AddTestAndAction(test, action);
    }
}