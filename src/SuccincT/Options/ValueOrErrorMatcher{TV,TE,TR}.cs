using SuccincT.Functional;
using SuccincT.PatternMatchers;
using SuccincT.Unions.PatternMatchers;
using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    internal sealed class ValueOrErrorMatcher<TValue, TError, TResult> : IUnionFuncPatternMatcherAfterElse<TResult>,
                                                                         IValueOrErrorFuncMatcher<TValue, 
                                                                                                  TError,
                                                                                                  TResult>,
                                                                         IValueOrErrorActionMatcher<TValue, TError>,
                                                                         IUnionActionPatternMatcherAfterElse
    {
        private readonly ValueOrError<TValue, TError> _valueOrError;

        private readonly MatchFunctionSelector<TValue, TValue, TResult> _valueFunctionSelector =
            new MatchFunctionSelector<TValue, TValue, TResult>(
                x => throw new NoMatchException("No match action defined for ValueOrError with value"));

        private readonly MatchFunctionSelector<TError, TError, TResult> _errorFunctionSelector =
            new MatchFunctionSelector<TError, TError, TResult>(
                x => throw new NoMatchException("No match action defined for ValueOrError with value"));

        private Func<ValueOrError<TValue, TError>, TResult> _elseAction;

        internal ValueOrErrorMatcher(ValueOrError<TValue, TError> valueOrError) => _valueOrError = valueOrError;

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TValue, TResult>
            IValueOrErrorFuncMatcher<TValue, TError, TResult>.Value()
        {
            return new UnionPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TValue, TResult>(
                RecordValueAction,
                this);
        }

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TError, TResult>
            IValueOrErrorFuncMatcher<TValue, TError, TResult>.Error()
        {
            return new UnionPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TError, TResult>(
                RecordErrorAction,
                this);
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IValueOrErrorFuncMatcher<TValue, TError, TResult>.Else(TResult value)
        {
            _elseAction = _ => value;
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IValueOrErrorFuncMatcher<TValue, TError, TResult>.Else(
            Func<ValueOrError<TValue, TError>, TResult> elseAction)
        {
            _elseAction = elseAction;
            return this;
        }

        TResult IValueOrErrorFuncMatcher<TValue, TError, TResult>.Result()
        {
            return _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error);
        }

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TValue>
            IValueOrErrorActionMatcher<TValue, TError>.Value()
        {
            return new UnionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TValue, TResult>(
                RecordValueAction,
                this);
        }


        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TError>
            IValueOrErrorActionMatcher<TValue, TError>.Error()
        {
            return new UnionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TError, TResult>(
                RecordErrorAction,
                this);
        }

        IUnionActionPatternMatcherAfterElse IValueOrErrorActionMatcher<TValue, TError>.Else(
            Action<ValueOrError<TValue, TError>> elseAction)
        {
            _elseAction = elseAction.ToUnitFunc() as Func<ValueOrError<TValue, TError>, TResult>;
            return this;
        }

        IUnionActionPatternMatcherAfterElse IValueOrErrorActionMatcher<TValue, TError>.IgnoreElse()
        {
            _elseAction = x => default;
            return this;
        }

        void IValueOrErrorActionMatcher<TValue, TError>.Exec()
        {
            _ = _valueOrError.HasValue
                ? _valueFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)
                : _errorFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error);
        }

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

        private void RecordValueAction(Func<TValue, IList<TValue>, bool> withTest,
                                       Func<TValue, bool> whereTest,
                                       IList<TValue> withValues,
                                       Func<TValue, TResult> action)
            => _valueFunctionSelector.AddTestAndAction(withTest, withValues, whereTest, action);

        private void RecordErrorAction(Func<TError, IList<TError>, bool> withTest,
                                       Func<TError, bool> whereTest,
                                       IList<TError> withValues,
                                       Func<TError, TResult> action)
            => _errorFunctionSelector.AddTestAndAction(withTest, withValues, whereTest, action);
    }
}