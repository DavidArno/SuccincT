using System;
using SuccincT.Functional;

namespace SuccincT.Options
{
    public class ValueOrError<TValue, TError>
    {
        private readonly TValue _value;
        private readonly TError _error;

        protected internal ValueOrError(TValue value, TError error)
        {
            _value = value;
            _error = error;
        }

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IValueOrErrorFuncMatcher<TValue, TError, TResult> Match<TResult>() => new ValueOrErrorMatcher<TValue, TError, TResult>(this);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IValueOrErrorActionMatcher<TValue, TError> Match() => new ValueOrErrorMatcher<TValue, TError, Unit>(this);

        /// <summary>
        /// True if created via WithValue(), else false.
        /// </summary>
        public bool HasValue => _value != null;

        /// <summary>
        /// The value held (if created by WithValue()). Will throw an InvalidOperationException if created via
        /// WithError().
        /// </summary>
        public TValue Value =>
            HasValue ? _value : throw new InvalidOperationException("ValueOrError doesn't contain a value");

        /// <summary>
        /// The error held (if created by WithError()). Will throw an InvalidOperationException if created via
        /// WithValue().
        /// </summary>
        public TError Error =>
            !HasValue ? _error : throw new InvalidOperationException("ValueOrError doesn't contain an error");

        public override string ToString() => HasValue ? $"Value of {_value}" : $"Error of {_error}";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override bool Equals(object obj) =>
            obj is ValueOrError<TValue, TError> testObject && 
            HasValue == testObject.HasValue && 
            (HasValue ? testObject._value.Equals(_value) : testObject._error.Equals(_error));

        public override int GetHashCode() => HasValue ? _value.GetHashCode() : _error.GetHashCode();

        public static bool operator ==(ValueOrError<TValue, TError> a, ValueOrError<TValue, TError> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return aObj == null && bObj == null || aObj != null && a.Equals(b);
        }

        public static bool operator !=(ValueOrError<TValue, TError> a, ValueOrError<TValue, TError> b) => !(a == b);
    }
}
