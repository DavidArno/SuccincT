using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionOfTwoPatternMatcher<T1, T2, TResult>
    {
        /// <summary>
        /// Fluent class created by Union{T1,T2}.Match{TResult}(). Whilst this is a public
        /// class (as the user needs access to Case1-2(), Else() and Result()), it has an internal constructor as it's
        /// intended for pattern matching internal usage only.
        /// </summary>
        private readonly Union<T1, T2> _union;

        private readonly MatchFunctionSelector<T1, TResult> _case1FunctionSelector =
            new MatchFunctionSelector<T1, TResult>(
                x => { throw new NoMatchException("No match action defined for union with Case1 value"); });

        private readonly MatchFunctionSelector<T2, TResult> _case2FunctionSelector =
            new MatchFunctionSelector<T2, TResult>(
                x => { throw new NoMatchException("No match action defined for union with Case2 value"); });

        private readonly Dictionary<Variant, Func<TResult>> _resultActions;

        internal UnionOfTwoPatternMatcher(Union<T1, T2> union)
        {
            _union = union;
            _resultActions = new Dictionary<Variant, Func<TResult>>
            {
                {Variant.Case1, () => _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)}
            };
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TResult>, T1, TResult> Case1()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TResult>, T1, TResult>(RecordAction,
                                                                                                       this);
        }

        public UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TResult>, T2, TResult> Case2()
        {
            return new UnionPatternCaseHandler<UnionOfTwoPatternMatcher<T1, T2, TResult>, T2, TResult>(RecordAction,
                                                                                                       this);
        }

        public UnionOfTwoPatternMatcherAfterElse<T1, T2, TResult> Else(Func<Union<T1, T2>, TResult> elseAction)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T1, T2, TResult>(_union,
                                                                          _case1FunctionSelector,
                                                                          _case2FunctionSelector,
                                                                          elseAction);
        }

        public UnionOfTwoPatternMatcherAfterElse<T1, T2, TResult> Else(TResult elseValue)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T1, T2, TResult>(_union,
                                                                          _case1FunctionSelector,
                                                                          _case2FunctionSelector,
                                                                          x => elseValue);
        }

        public TResult Result()
        {
            return _resultActions[_union.Case]();
        }

        private void RecordAction(Func<T1, bool> test, Func<T1, TResult> action)
        {
            _case1FunctionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<T2, bool> test, Func<T2, TResult> action)
        {
            _case2FunctionSelector.AddTestAndAction(test, action);
        }
    }
}