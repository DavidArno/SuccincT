using System;

namespace SuccincT.Options
{
    public class ValueOrError
    {
        private readonly string _value;
        private readonly string _error;

        private ValueOrError(string value, string error)
        {
            _value = value;
            _error = error;
        }

        public static ValueOrError CreateWithValue(string value)
        {
            return new ValueOrError(value, null);
        }

        public static ValueOrError CreateWithError(string error)
        {
            return new ValueOrError(null, error);
        }

        public void MatchAndAction(Action<string> valueAction, Action<string> errorAction)
        {
            if (_value != null)
            {
                valueAction(_value);
            }
            else
            {
                errorAction(_error);
            }
        }

        public T MatchAndResult<T>(Func<string, T> valueFunction, Func<string, T> errorFunction) 
        {
            return _value != null ? valueFunction(_value) : errorFunction(_error);
        }
    }
}
