using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    /// <summary>
    /// Fluent class created by Option{T}.Match{TResult}(). Whilst this is a public class (as the user needs access
    /// to Some(), None() and Else()), it has an internal constructor as it's intended for internal
    /// pattern matching usage only.
    /// </summary>
    public sealed class OptionMatcher<T, TResult>
    {
        private readonly Union<T, None> _union;
        private readonly Option<T> _option;

        private readonly MatchFunctionSelector<T, TResult> _case1FunctionSelector =
            new MatchFunctionSelector<T, TResult>(
                x => { throw new NoMatchException("No match action defined for Option with value"); });

        private readonly MatchFunctionSelector<None, TResult> _case2FunctionSelector =
            new MatchFunctionSelector<None, TResult>(
                x => { throw new NoMatchException("No match action defined for Option with no value"); });

        private readonly Dictionary<Variant, Func<TResult>> _resultActions;

        internal OptionMatcher(Union<T, None> union, Option<T> option)
        {
            _union = union;
            _option = option;
            _resultActions = new Dictionary<Variant, Func<TResult>>
            {
                {Variant.Case1, () => _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)},
                {Variant.Case2, () => _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2)}
            };
        }

        public UnionPatternCaseHandler<OptionMatcher<T, TResult>, T, TResult> Some()
        {
            return new UnionPatternCaseHandler<OptionMatcher<T, TResult>, T, TResult>(RecordAction, this);
        }

        public NoneMatchHandler<T, TResult> None()
        {
            return new NoneMatchHandler<T, TResult>(RecordAction, this);
        }

        public UnionOfTwoPatternMatcherAfterElse<T, None, TResult> Else(Func<Option<T>, TResult> elseAction)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T, None, TResult>(_union,
                                                                           _case1FunctionSelector,
                                                                           _case2FunctionSelector,
                                                                           x => elseAction(_option));
        }

        public UnionOfTwoPatternMatcherAfterElse<T, None, TResult> Else(TResult elseValue)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T, None, TResult>(_union,
                                                                           _case1FunctionSelector,
                                                                           _case2FunctionSelector,
                                                                           x => elseValue);
        }

        public TResult Result()
        {
            return _resultActions[_union.Case]();
        }

        private void RecordAction(Func<T, bool> test, Func<T, TResult> action)
        {
            _case1FunctionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<TResult> action)
        {
            _case2FunctionSelector.AddTestAndAction(x => true, x => action());
        }
    }
}