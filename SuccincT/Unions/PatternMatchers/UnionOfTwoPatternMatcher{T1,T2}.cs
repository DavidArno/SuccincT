using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2}.Match(). Whilst this is a public
    /// class (as the user needs access to Case1-2(), CaseOf(), Else() and Result()), it has an 
    /// internal constructor as it is intended for pattern matching internal usage only.
    /// </summary>
    public sealed class UnionOfTwoPatternMatcher<T1, T2>
    {
        private readonly Union<T1, T2> _union;

        private readonly MatchActionSelector<T1> _case1ActionSelector =
            new MatchActionSelector<T1>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchActionSelector<T2> _case2ActionSelector =
            new MatchActionSelector<T2>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly Dictionary<Variant, Action> _resultActions;

        internal UnionOfTwoPatternMatcher(Union<T1, T2> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Action>
            {
                {Variant.Case1, () => _case1ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case2)}
            };
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T1> Case1() =>
            new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T1>(RecordAction,
                                                                              this);

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T2> Case2() =>
            new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T2>(RecordAction,
                                                                              this);

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T> CaseOf<T>()
        {
            if (typeof(T) == typeof(T1))
            {
                return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T1>(RecordAction, this)
                    as UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T>;
            }

            if (typeof(T) == typeof(T2))
            {
                return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T2>(RecordAction, this)
                    as UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2>, T>;
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        public UnionOfTwoPatternMatcherAfterElse<T1, T2> Else(Action<Union<T1, T2>> elseAction) =>
            new UnionOfTwoPatternMatcherAfterElse<T1, T2>(_union,
                                                          _case1ActionSelector,
                                                          _case2ActionSelector,
                                                          elseAction);

        public UnionOfTwoPatternMatcherAfterElse<T1, T2> IgnoreElse() =>
            new UnionOfTwoPatternMatcherAfterElse<T1, T2>(_union,
                                                          _case1ActionSelector,
                                                          _case2ActionSelector,
                                                          x => { });

        public void Exec() => _resultActions[_union.Case]();

        private void RecordAction(Func<T1, bool> test, Action<T1> action) =>
            _case1ActionSelector.AddTestAndAction(test, action);

        private void RecordAction(Func<T2, bool> test, Action<T2> action) =>
            _case2ActionSelector.AddTestAndAction(test, action);
    }
}