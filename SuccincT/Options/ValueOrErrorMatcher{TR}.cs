using System;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public class ValueOrErrorMatcher<TReturn> 
    {
        private readonly ValueOrError _valueOrError;

        private readonly UnionCaseActionSelector<string, TReturn> _valueActionSelector =
            new UnionCaseActionSelector<string, TReturn>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        private readonly UnionCaseActionSelector<string, TReturn> _errorActionSelector =
            new UnionCaseActionSelector<string, TReturn>(
                x => { throw new NoMatchException("No match action defined for ValueOrError with value"); });

        internal ValueOrErrorMatcher(ValueOrError valueOrError)
        {
            _valueOrError = valueOrError;
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher<TReturn>, string, TReturn> Value()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher<TReturn>, string, TReturn>(RecordValueAction, this);
        }

        public UnionPatternCaseHandler<ValueOrErrorMatcher<TReturn>, string, TReturn> Error()
        {
            return new UnionPatternCaseHandler<ValueOrErrorMatcher<TReturn>, string, TReturn>(RecordErrorAction, this);
        }

        public UnionOfTwoPatternMatcherAfterElse<Union<string, string>, string, string, TReturn> Else(
            Func<ValueOrError, TReturn> elseAction)
        {
            var union = CreateUnionFromValueOrError(_valueOrError);
            return new UnionOfTwoPatternMatcherAfterElse<Union<string, string>, string, string, TReturn>(
                union,
                _valueActionSelector,
                _errorActionSelector,
                x => elseAction(_valueOrError));
        }

        public TReturn Result()
        {
            return _valueOrError.HasValue
                ? _valueActionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Value)
                : _errorActionSelector.DetermineResultUsingDefaultIfRequired(_valueOrError.Error);
        }

        private void RecordValueAction(Func<string, bool> test, Func<string, TReturn> action)
        {
            _valueActionSelector.AddTestAndAction(test, action);
        }

        private void RecordErrorAction(Func<string, bool> test, Func<string, TReturn> action)
        {
            _errorActionSelector.AddTestAndAction(test, action);
        }

        private static Union<string, string> CreateUnionFromValueOrError(ValueOrError valueOrError)
        {
            return valueOrError.HasValue 
                ? new Union<string, string>(valueOrError.Value, null, Variant.Case1)
                : new Union<string, string>(null, valueOrError.Error, Variant.Case2);
        }
    }
}