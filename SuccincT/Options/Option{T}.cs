using System;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides an optional value of type T. Modelled on F# options. Either contains a T value, or None.
    /// </summary>
    public class Option<T>
    {
        private readonly T _value;
        private readonly bool _containsValue;

        internal Option(T value)
        {
            _value = value;
            _containsValue = true;
        }

        internal Option() { _containsValue = false; }

        public bool HasValue { get { return _containsValue; } }
        public T Value 
        { 
            get
            {
                if (!_containsValue) { throw new InvalidOperationException("Option contains no value."); }
                return _value;
            }
        }

        public void MatchAndAction(Action<T> valueAction, Action noneAction)
        {
            if (_containsValue)
            {
                valueAction(_value);
            }
            else
            {
                noneAction();
            }
        }

        public TResult MatchAndResult<TResult>(Func<T, TResult> valueFunction, Func<TResult> noneFunction) 
        {
            return _containsValue ? valueFunction(_value) : noneFunction();
        }
    }
}
