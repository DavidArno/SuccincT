using System;

namespace SuccincT.Options
{
    public class ValueOrError : ValueOrError<string, string>
    {
        protected internal ValueOrError(string value, string error) : base(value, error)
        {
        }

        /// <summary>
        /// Creates a new instance with a value (and no error)
        /// </summary>
        public static ValueOrError<TValue, TError> WithValue<TValue, TError>(TValue value) =>
            value != null
                ? new ValueOrError<TValue, TError>(value, default)
                : throw new ArgumentNullException(nameof(value));
        /// <summary>
        /// Creates a new instance with a value (and no error)
        /// </summary>
        public static ValueOrError<TValue, string> WithValue<TValue>(TValue value) =>
            value != null
                ? new ValueOrError<TValue, string>(value, default)
                : throw new ArgumentNullException(nameof(value));
        /// <summary>
        /// Creates a new instance with a value (and no error)
        /// </summary>
        public static ValueOrError<string, string> WithValue(string value) =>
            value != null
                ? new ValueOrError<string, string>(value, default)
                : throw new ArgumentNullException(nameof(value));

        /// <summary>
        /// Creates a new instance with an error (and no value)
        /// </summary>
        public static ValueOrError<TValue, TError> WithError<TValue, TError>(TError error) =>
            error != null
                ? new ValueOrError<TValue, TError>(default, error)
                : throw new ArgumentNullException(nameof(error));
        /// <summary>
        /// Creates a new instance with an error (and no value)
        /// </summary>
        public static ValueOrError<string, TError> WithError<TError>(TError error) =>
            error != null
                ? new ValueOrError<string, TError>(default, error)
                : throw new ArgumentNullException(nameof(error));
        /// <summary>
        /// Creates a new instance with an error (and no value)
        /// </summary>
        public static ValueOrError<string, string> WithError(string error) =>
            error != null
                ? new ValueOrError<string, string>(default, error)
                : throw new ArgumentNullException(nameof(error));
    }
}