using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public sealed class ValueOrErrorMatcher<TResult>
    {
        private readonly ValueOrError _valueOrError;

        private readonly MatchFunctionSelector<string, TResult> _valueFunctionSelector =
            new MatchFunctionSelector<string, TResult>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        private readonly MatchFunctionSelector<string, TResult> _errorFunctionSelector =
            new MatchFunctionSelector<string, TResult>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        private readonly Dictionary<bool, Func<TResult>> _resultActions;

        internal ValueOrErrorMatcher(ValueOrError valueOrError)
        {
            _valueOrError = valueOrError;
            _resultActions = new Dictionary<bool, Func<TResult>>
            {
                {false, () => _errorFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error)},
                {true, () => _valueFunctionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)}
            };
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher<TResult>, string, TResult> Value()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher<TResult>, string, TResult>(RecordValueAction, this);
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher<TResult>, string, TResult> Error()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher<TResult>, string, TResult>(RecordErrorAction, this);
        }

        public UnionOfTwoPatternMatcherAfterElse<string, string, TResult> Else(TResult value)
        {
            var union = CreateUnionFromValueOrError(_valueOrError);
            return new UnionOfTwoPatternMatcherAfterElse<string, string, TResult>(union,
                                                                                  _valueFunctionSelector,
                                                                                  _errorFunctionSelector,
                                                                                  _ => value);
        }

        public UnionOfTwoPatternMatcherAfterElse<string, string, TResult> Else(
            Func<ValueOrError, TResult> elseAction)
        {
            var union = CreateUnionFromValueOrError(_valueOrError);
            return new UnionOfTwoPatternMatcherAfterElse<string, string, TResult>(
                union,
                _valueFunctionSelector,
                _errorFunctionSelector,
                x => elseAction(_valueOrError));
        }

        public TResult Result()
        {
            return _resultActions[_valueOrError.HasValue]();
        }

        private void RecordValueAction(Func<string, bool> test, Func<string, TResult> action)
        {
            _valueFunctionSelector.AddTestAndAction(test, action);
        }

        private void RecordErrorAction(Func<string, bool> test, Func<string, TResult> action)
        {
            _errorFunctionSelector.AddTestAndAction(test, action);
        }

        private static Union<string, string> CreateUnionFromValueOrError(ValueOrError valueOrError)
        {
            return valueOrError.HasValue
                       ? new Union<string, string>(valueOrError.Value, null, Variant.Case1)
                       : new Union<string, string>(null, valueOrError.Error, Variant.Case2);
        }
    }
}