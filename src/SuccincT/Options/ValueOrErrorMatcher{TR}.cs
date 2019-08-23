using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.PatternMatchers;
using SuccincT.Unions.PatternMatchers;
using static SuccincT.Functional.Unit;

namespace SuccincT.Options
{
    internal sealed class ValueOrErrorMatcher<TResult> : 
        IUnionFuncPatternMatcherAfterElse<TResult>, 
        IValueOrErrorFuncMatcher<TResult>, 
        IValueOrErrorActionMatcher, 
        IUnionActionPatternMatcherAfterElse
       
    {
        private readonly ValueOrError _valueOrError;

        private readonly MatchFunctionSelector<string, string, TResult> _valueFunctionSelector =
            new MatchFunctionSelector<string, string, TResult>(
                x => throw new NoMatchException("No match action defined for ValueOrError with value"));

        private readonly MatchFunctionSelector<string, string, TResult> _errorFunctionSelector =
            new MatchFunctionSelector<string, string, TResult>(
                x => throw new NoMatchException("No match action defined for ValueOrError with value"));

        private Func<ValueOrError, TResult> _elseAction;

        internal ValueOrErrorMatcher(ValueOrError valueOrError)
        {
            _elseAction = _ => default!;
            _valueOrError = valueOrError;
        }

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult>
            IValueOrErrorFuncMatcher<TResult>.Value() =>
                new UnionPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult>(RecordValueAction, this);

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult>
            IValueOrErrorFuncMatcher<TResult>.Error() =>
                new UnionPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult>(RecordErrorAction, this);

        IUnionFuncPatternMatcherAfterElse<TResult> IValueOrErrorFuncMatcher<TResult>.Else(TResult value)
        {
            _elseAction = _ => value;
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IValueOrErrorFuncMatcher<TResult>.Else(
            Func<ValueOrError, TResult> elseAction)
        {
            _elseAction = elseAction;
            return this;
        }

        TResult IValueOrErrorFuncMatcher<TResult>.Result() => 
            _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error);

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> IValueOrErrorActionMatcher.Value() =>
                new UnionPatternCaseHandler<IValueOrErrorActionMatcher, string, TResult>(RecordValueAction, this);

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> IValueOrErrorActionMatcher.Error() =>
                new UnionPatternCaseHandler<IValueOrErrorActionMatcher, string, TResult>(RecordErrorAction, this);

        IUnionActionPatternMatcherAfterElse IValueOrErrorActionMatcher.Else(Action<ValueOrError> elseAction)
        {
            _elseAction = (elseAction.ToUnitFunc() as Func<ValueOrError, TResult>)!;
            return this;
        }

        IUnionActionPatternMatcherAfterElse IValueOrErrorActionMatcher.IgnoreElse()
        {
            _elseAction = x => default!;
            return this;
        }

        void IValueOrErrorActionMatcher.Exec() => 
            _ = _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResult(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResult(_valueOrError.Error);

            return possibleResult.HasValue ? possibleResult.Value : _elseAction(_valueOrError);
        }

        void IUnionActionPatternMatcherAfterElse.Exec()
        {
            var possibleResult = _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResult(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResult(_valueOrError.Error);

            _ = possibleResult.HasValue ? possibleResult.Value : _elseAction(_valueOrError);
        }

        private void RecordValueAction(Func<string, IList<string>, bool> withTest,
                                       Func<string, bool> whereTest,
                                       IList<string> withValues,
                                       Func<string, TResult> action) => 
            _valueFunctionSelector.AddTestAndAction(withTest, withValues, whereTest, action);

        private void RecordErrorAction(Func<string, IList<string>, bool> withTest,
                                       Func<string, bool> whereTest,
                                       IList<string> withValues,
                                       Func<string, TResult> action) => 
            _errorFunctionSelector.AddTestAndAction(withTest, withValues, whereTest, action);
    }
}