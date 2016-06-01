using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;

namespace SuccincT.Options
{
    internal sealed class OptionMatcher<T, TResult> : IUnionFuncPatternMatcherAfterElse<TResult>,
                                                      IOptionFuncMatcher<T, TResult>,
                                                      INoneFuncMatchHandler<T, TResult>,
                                                      IOptionActionMatcher<T>,
                                                      INoneActionMatchHandler<T>,
                                                      IUnionActionPatternMatcherAfterElse
    {
        private readonly Union<T, None> _union;
        private readonly Option<T> _option;

        private readonly MatchFunctionSelector<T, TResult> _case1FunctionSelector =
            new MatchFunctionSelector<T, TResult>(
                x => { throw new NoMatchException("No match action defined for Option with value"); });

        private readonly MatchFunctionSelector<None, TResult> _case2FunctionSelector =
            new MatchFunctionSelector<None, TResult>(
                x => { throw new NoMatchException("No match action defined for Option with no value"); });

        private Func<Option<T>, TResult> _elseAction;

        internal OptionMatcher(Union<T, None> union, Option<T> option)
        {
            _union = union;
            _option = option;
        }

        IUnionFuncPatternCaseHandler<IOptionFuncMatcher<T, TResult>, T, TResult> IOptionFuncMatcher<T, TResult>.Some()
            =>
                new UnionPatternCaseHandler<IOptionFuncMatcher<T, TResult>, T, TResult>(RecordAction, this);

        INoneFuncMatchHandler<T, TResult> IOptionFuncMatcher<T, TResult>.None() => this;

        IUnionFuncPatternMatcherAfterElse<TResult> IOptionFuncMatcher<T, TResult>.Else(
            Func<Option<T>, TResult> elseAction)
        {
            _elseAction = elseAction;
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IOptionFuncMatcher<T, TResult>.Else(TResult elseValue)
        {
            _elseAction = _ => elseValue;
            return this;
        }

        TResult IOptionFuncMatcher<T, TResult>.Result()
        {
            return _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)
                : _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2);
        }

        IUnionActionPatternCaseHandler<IOptionActionMatcher<T>, T> IOptionActionMatcher<T>.Some() =>
            new UnionPatternCaseHandler<IOptionActionMatcher<T>, T, TResult>(RecordAction, this);

        INoneActionMatchHandler<T> IOptionActionMatcher<T>.None() => this;

        IUnionActionPatternMatcherAfterElse IOptionActionMatcher<T>.Else(Action<Option<T>> elseAction)
        {
            _elseAction = elseAction.ToUnitFunc() as Func<Option<T>, TResult>;
            return this;
        }

        IUnionActionPatternMatcherAfterElse IOptionActionMatcher<T>.IgnoreElse()
        {
            _elseAction = x => default(TResult);
            return this;
        }

        void IOptionActionMatcher<T>.Exec()
        {
            Ignore(_union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1)
                : _case2FunctionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2));
        }

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResult(_union.Case1)
                : _case2FunctionSelector.DetermineResult(_union.Case2);

            return possibleResult.HasValue ? possibleResult.Value : _elseAction(_option);
        }

        void IUnionActionPatternMatcherAfterElse.Exec()
        {
            var possibleResult = _union.Case == Variant.Case1
                ? _case1FunctionSelector.DetermineResult(_union.Case1)
                : _case2FunctionSelector.DetermineResult(_union.Case2);

            Ignore(possibleResult.HasValue ? possibleResult.Value : _elseAction(_option));
        }

        IOptionFuncMatcher<T, TResult> INoneFuncMatchHandler<T, TResult>.Do(Func<TResult> action)
        {
            RecordAction(action);
            return this;
        }

        IOptionFuncMatcher<T, TResult> INoneFuncMatchHandler<T, TResult>.Do(TResult value)
        {
            RecordAction(() => value);
            return this;
        }

        IOptionActionMatcher<T> INoneActionMatchHandler<T>.Do(Action action)
        {
            RecordAction(action.ToUnitFunc() as Func<TResult>);
            return this;
        }

        private void RecordAction(Func<T, IList<T>, bool> withTest,
                                  Func<T, bool> whereTest,
                                  IList<T> withData,
                                  Func<T, TResult> action)
        {
            _case1FunctionSelector.AddTestAndAction(withTest, withData, whereTest, action);
        }

        private void RecordAction(Func<TResult> action) =>
            _case2FunctionSelector.AddTestAndAction((x, y) => true, null, null, x => action());
    }
}