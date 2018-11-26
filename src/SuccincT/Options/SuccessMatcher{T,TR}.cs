using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    internal sealed class SuccessMatcher<T, TResult> : IUnionFuncPatternMatcherAfterElse<TResult>,
                                                      ISuccessFuncMatcher<T, TResult>,
                                                      ISuccessFuncMatchHandler<T, TResult>,
                                                      ISuccessActionMatcher<T>,
                                                      ISuccessActionMatchHandler<T>,
                                                      IUnionActionPatternMatcherAfterElse
    {
        private readonly Union<T, bool> _union;
        private readonly Success<T> _success;

        private readonly MatchFunctionSelector<T, T, TResult> _case1FunctionSelector =
            new MatchFunctionSelector<T, T, TResult>(
                x => throw new NoMatchException("No match action defined for Success with failure"));

        private readonly MatchFunctionSelector<bool, bool, TResult> _case2FunctionSelector =
            new MatchFunctionSelector<bool, bool, TResult>(
                x => throw new NoMatchException("No match action defined for Success with no failure"));

        private Func<Success<T>, TResult> _elseAction;

        internal SuccessMatcher(Union<T, bool> union, Success<T> option)
        {
            _union = union;
            _success = option;
        }

        IUnionFuncPatternCaseHandler<ISuccessFuncMatcher<T, TResult>, T, TResult> 
            ISuccessFuncMatcher<T, TResult>.Error() =>
                new UnionPatternCaseHandler<ISuccessFuncMatcher<T, TResult>, T, TResult>(RecordAction, this);

        ISuccessFuncMatchHandler<T, TResult> ISuccessFuncMatcher<T, TResult>.Success() => this;

        IUnionFuncPatternMatcherAfterElse<TResult> ISuccessFuncMatcher<T, TResult>.Else(
            Func<Success<T>, TResult> elseAction)
        {
            _elseAction = elseAction;
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> ISuccessFuncMatcher<T, TResult>.Else(TResult elseValue)
        {
            _elseAction = _ => elseValue;
            return this;
        }

        TResult ISuccessFuncMatcher<T, TResult>.Result() => _union.Case == Variant.Case1
            ? _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)
            : _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2);

        IUnionActionPatternCaseHandler<ISuccessActionMatcher<T>, T> ISuccessActionMatcher<T>.Error() =>
            new UnionPatternCaseHandler<ISuccessActionMatcher<T>, T, TResult>(RecordAction, this);

        ISuccessActionMatchHandler<T> ISuccessActionMatcher<T>.Success() => this;

        IUnionActionPatternMatcherAfterElse ISuccessActionMatcher<T>.Else(Action<Success<T>> elseAction)
        {
            _elseAction = elseAction.ToUnitFunc() as Func<Success<T>, TResult>;
            return this;
        }

        IUnionActionPatternMatcherAfterElse ISuccessActionMatcher<T>.IgnoreElse()
        {
            _elseAction = x => default;
            return this;
        }

        void ISuccessActionMatcher<T>.Exec() =>
            _ = _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)
                : _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result()
        {
            var (hasValue, value) = _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResult(_union.Case1)
                : _case2FunctionSelector.DetermineResult(_union.Case2);

            return hasValue ? value : _elseAction(_success);
        }

        void IUnionActionPatternMatcherAfterElse.Exec()
        {
            var (hasValue, value) = _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResult(_union.Case1)
                : _case2FunctionSelector.DetermineResult(_union.Case2);

            _ = hasValue ? value : _elseAction(_success);
        }

        ISuccessFuncMatcher<T, TResult> ISuccessFuncMatchHandler<T, TResult>.Do(Func<TResult> action)
        {
            RecordAction(action);
            return this;
        }

        ISuccessFuncMatcher<T, TResult> ISuccessFuncMatchHandler<T, TResult>.Do(TResult value)
        {
            RecordAction(() => value);
            return this;
        }

        ISuccessActionMatcher<T> ISuccessActionMatchHandler<T>.Do(Action action)
        {
            RecordAction(action.ToUnitFunc() as Func<TResult>);
            return this;
        }

        private void RecordAction(Func<T, IList<T>, bool> withTest,
                                  Func<T, bool> whereTest,
                                  IList<T> withData,
                                  Func<T, TResult> action) =>
            _case1FunctionSelector.AddTestAndAction(withTest, withData, whereTest, action);

        private void RecordAction(Func<TResult> action) =>
            _case2FunctionSelector.AddTestAndAction((x, y) => true, null, null, x => action());
    }
}