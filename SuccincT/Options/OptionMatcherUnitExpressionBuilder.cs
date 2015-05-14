using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    public class OptionMatcherUnitExpressionBuilder<T> 
    {
        private readonly OptionMatcherUnit<T> _matcher;
        private readonly List<T> _values = new List<T>();

        public OptionMatcherUnitExpressionBuilder(OptionMatcherUnit<T> matcher, T value)
        {
            _matcher = matcher;
            _values.Add(value);
        }

        public OptionMatcherUnitExpressionBuilder<T> Or(T value)
        {
            _values.Add(value);
            return this;
        }

        public OptionMatcherUnit<T> Do(Action<T> action)
        {
            _matcher.Some(_values, action);
            return _matcher;
        }
    }
}