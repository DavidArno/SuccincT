using System;
using SuccincT.Functional;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides a special-case union of two string values: one representing a value; the other an error. For
    /// use in situations where a string return type is needed, by throwing exceptions isn't desirable.
    /// </summary>
    public sealed class ValueOrError
    {
        private readonly string _value;
        private readonly string _error;

        private ValueOrError(string value, string error)
        {
            _value = value;
            _error = error;
        }

        /// <summary>
        /// Creates a new instance with a value (and no error)
        /// </summary>
        public static ValueOrError WithValue(string value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            return new ValueOrError(value, null);
        }

        /// <summary>
        /// Creates a new instance with an error (and no value)
        /// </summary>
        public static ValueOrError WithError(string error)
        {
            if (error == null) { throw new ArgumentNullException(nameof(error)); }
            return new ValueOrError(null, error);
        }

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IValueOrErrorFuncMatcher<TResult> Match<TResult>() => new ValueOrErrorMatcher<TResult>(this);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IValueOrErrorActionMatcher Match() => new ValueOrErrorMatcher<Unit>(this);

        /// <summary>
        /// True if created via WithValue(), else false.
        /// </summary>
        public bool HasValue => _value != null;

        /// <summary>
        /// The value held (if created by WithValue()). Will throw an InvalidOperationException if created via
        /// WithError().
        /// </summary>
        public string Value
        {
            get
            {
                if (!HasValue) { throw new InvalidOperationException("ValueOrError doesn't contain a value"); }
                return _value;
            }
        }

        /// <summary>
        /// The error held (if created by WithError()). Will throw an InvalidOperationException if created via
        /// WithValue().
        /// </summary>
        public string Error
        {
            get
            {
                if (HasValue) { throw new InvalidOperationException("ValueOrError doesn't contain an error"); }
                return _error;
            }
        }

        public override string ToString() => HasValue ? $"Value of {_value}" : $"Error of {_error}";

        public override bool Equals(object obj)
        {
            return obj is ValueOrError testObject  && testObject._error == _error && testObject._value == _value;
        }

        public override int GetHashCode() => HasValue ? _value.GetHashCode() : _error.GetHashCode();

        public static bool operator ==(ValueOrError a, ValueOrError b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return aObj == null && bObj == null || aObj != null && a.Equals(b);
        }

        public static bool operator !=(ValueOrError a, ValueOrError b) => !(a == b);
    }
}