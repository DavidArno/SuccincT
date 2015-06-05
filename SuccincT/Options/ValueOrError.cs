using System;

namespace SuccincT.Options
{
    /// <summary>
    /// A class that encapsulates two possible string results: either a string value, or a string describing an error.
    /// </summary>
    public class ValueOrError
    {
        private readonly string _value;
        private readonly string _error;

        private ValueOrError(string value, string error)
        {
            _value = value;
            _error = error;
        }

        public static ValueOrError WithValue(string value)
        {
            return new ValueOrError(value, null);
        }

        public static ValueOrError WithError(string error)
        {
            return new ValueOrError(null, error);
        }

        public bool HasValue => _value != null;

        public string Value
        {
            get
            {
                if (!HasValue) { throw new InvalidOperationException("ValueOrError doesn't contain a value"); }
                return _value;
            }
        }

        public string Error
        {
            get
            {
                if (HasValue) { throw new InvalidOperationException("ValueOrError doesn't contain an error"); }
                return _error;
            }
        }

        public override string ToString()
        {
<<<<<<< HEAD
            return HasValue ? $"Value of {_value}" : $"Error of {_error}";
=======
            return HasValue
                ? string.Format("Value of {0}", _value)
                : string.Format("Error of {0}", _error);
>>>>>>> master
        }

        public ValueOrErrorMatcher<TResult> Match<TResult>()
        {
            return new ValueOrErrorMatcher<TResult>(this);
        }

        public ValueOrErrorMatcher Match()
        {
            return new ValueOrErrorMatcher(this);
        }
    }
}