using System;
using System.Collections.Generic;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public sealed class ValueOrErrorMatcher
    {
        private readonly ValueOrError _valueOrError;

        private readonly MatchActionSelector<string> _valueActionSelector =
            new MatchActionSelector<string>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        private readonly MatchActionSelector<string> _errorActionSelector =
            new MatchActionSelector<string>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        private readonly Dictionary<bool, Action> _resultActions;

        internal ValueOrErrorMatcher(ValueOrError valueOrError)
        {
            _valueOrError = valueOrError;
            _resultActions = new Dictionary<bool, Action>
            {
                {false, () => _errorActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_valueOrError.Error)},
                {true, () => _valueActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_valueOrError.Value)}
            };
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher, string> Value() => 
            new UnionPatternCaseHandler<ValueOrErrorMatcher, string>(RecordValueAction, this);

        public UnionPatternCaseHandler<ValueOrErrorMatcher, string> Error() => 
            new UnionPatternCaseHandler<ValueOrErrorMatcher, string>(RecordErrorAction, this);

        public UnionOfTwoPatternMatcherAfterElse<string, string> Else(
            Action<ValueOrError> elseAction)
        {
            var union = CreateUnionFromValueOrError(_valueOrError);
            return new UnionOfTwoPatternMatcherAfterElse<string, string>(
                union,
                _valueActionSelector,
                _errorActionSelector,
                x => elseAction(_valueOrError));
        }

        public void Exec() => _resultActions[_valueOrError.HasValue]();

        private void RecordValueAction(Func<string, bool> test, Action<string> action) => 
            _valueActionSelector.AddTestAndAction(test, action);

        private void RecordErrorAction(Func<string, bool> test, Action<string> action) => 
            _errorActionSelector.AddTestAndAction(test, action);

        private static Union<string, string> CreateUnionFromValueOrError(ValueOrError valueOrError) => 
            valueOrError.HasValue
                ? new Union<string, string>(valueOrError.Value, null, Variant.Case1)
                : new Union<string, string>(null, valueOrError.Error, Variant.Case2);
    }
}