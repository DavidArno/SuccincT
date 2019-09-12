using System;
using SuccincT.Functional;
using System.Runtime.InteropServices.ComTypes;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Options
{
    public readonly struct ValueOrError<TValue, TError>
    {
        private readonly TValue _value;
        private readonly TError _error;

        private ValueOrError(TValue value, TError error, bool hasValue)
            => (_value, _error, HasValue) = (value, error, hasValue);

        /// <summary>
        /// Creates a new instance with a value (and no error)
        /// </summary>
        public static ValueOrError<TValue, TError> WithValue(TValue value)
            => new ValueOrError<TValue, TError>(value, default!, true);

        /// <summary>
        /// Creates a new instance with an error (and no value)
        /// </summary>
        public static ValueOrError<TValue, TError> WithError(TError error)
            => new ValueOrError<TValue, TError>(default!, error, false);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IValueOrErrorFuncMatcher<TValue, TError, TResult> Match<TResult>()
            => new ValueOrErrorMatcher<TValue, TError, TResult>(this);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IValueOrErrorActionMatcher<TValue, TError> Match()
            => new ValueOrErrorMatcher<TValue, TError, Unit>(this);

        /// <summary>
        /// True if created via WithValue(), else false.
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// The value held (if created by WithValue()). Will throw an InvalidOperationException if created via
        /// WithError().
        /// </summary>
        public TValue Value
            => HasValue ? _value : throw new InvalidOperationException("ValueOrError doesn't contain a value");

        /// <summary>
        /// The error held (if created by WithError()). Will throw an InvalidOperationException if created via
        /// WithValue().
        /// </summary>
        public TError Error
            => !HasValue ? _error : throw new InvalidOperationException("ValueOrError doesn't contain an error");

        public void Deconstruct(out ValueOrErrorState valueOrError, out TValue value, out TError error)
            => (valueOrError, value, error) =
                (HasValue ? ValueOrErrorState.Value : ValueOrErrorState.Error, _value, _error);

        public override string ToString() => HasValue ? $"Value of {_value}" : $"Error of {_error}";

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case ValueOrError<TValue, TError> other: return other.HasValue == HasValue && ValueOrErrorsEqual(other);
                case null: return HasValue && _value == null || !HasValue && _error == null;
            }

            return false;
        }

        private bool ValueOrErrorsEqual(in ValueOrError<TValue, TError> other)
        {
            return HasValue
                ? _value == null && other.HasValue && other.Value == null ||
                  _value != null && _value.Equals(other.Value)
                : _error == null && !other.HasValue && other.Error == null ||
                  _error != null && _error.Equals(other.Error);
        }

        public override int GetHashCode() => HasValue ? GetItemHashCode(_value) : GetItemHashCode(_error);

        public static bool operator ==(ValueOrError<TValue, TError> a, ValueOrError<TValue, TError> b) => a.Equals(b);

        public static bool operator !=(ValueOrError<TValue, TError> a, ValueOrError<TValue, TError> b) => !a.Equals(b);
    }
}