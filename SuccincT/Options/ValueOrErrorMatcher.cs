using System;
using SuccincT.PatternMatchers;
using SuccincT.Unions;

namespace SuccincT.Options
{
    public class ValueOrErrorMatcher
    {
        private readonly ValueOrError _valueOrError;

        private readonly UnionCaseActionSelector<string> _valueActionSelector =
            new UnionCaseActionSelector<string>(
                x => { throw new InvalidOperationException("No match action defined for ValueOrError with value"); });

        private readonly UnionCaseActionSelector<string> _errorActionSelector =
            new UnionCaseActionSelector<string>(
                x => { throw new InvalidOperationException("No match action defined for ValueOrError with value"); });

        internal ValueOrErrorMatcher(ValueOrError valueOrError)
        {
            _valueOrError = valueOrError;
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher, string> Value()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher, string>(RecordValueAction, this);
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher, string> Error()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher, string>(RecordErrorAction, this);
        }

        public UnionPatternMatcherAfterElse<Union<string, string>, string, string> Else(
            Action<ValueOrError> elseAction)
        {
            var union = CreateUnionFromValueOrError(_valueOrError);
            return new UnionPatternMatcherAfterElse<Union<string, string>, string, string>(
                union,
                _valueActionSelector,
                _errorActionSelector,
                x => elseAction(_valueOrError));
        }

        public void Exec()
        {
            if (_valueOrError.HasValue)
            {
                _valueActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_valueOrError.Value);
            }
            else
            {
                _errorActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_valueOrError.Error);
            }
        }

        private void RecordValueAction(Func<string, bool> test, Action<string> action)
        {
            _valueActionSelector.AddTestAndAction(test, action);
        }

        private void RecordErrorAction(Func<string, bool> test, Action<string> action)
        {
            _errorActionSelector.AddTestAndAction(test, action);
        }

        private Union<string, string> CreateUnionFromValueOrError(ValueOrError valueOrError)
        {
            return valueOrError.HasValue 
                ? new Union<string, string>(valueOrError.Value, null, Variant.Case1)
                : new Union<string, string>(null, valueOrError.Error, Variant.Case2);
        }
    }
}