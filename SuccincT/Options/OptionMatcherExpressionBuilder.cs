using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    public class OptionMatcherExpressionBuilder<T, TReturn> 
    {
        private readonly OptionMatcher<T, TReturn> _matcher;
        private readonly List<T> _values = new List<T>();

        public OptionMatcherExpressionBuilder(OptionMatcher<T, TReturn> matcher, T value)
        {
            _matcher = matcher;
            _values.Add(value);
        }

        public OptionMatcherExpressionBuilder<T, TReturn> Or(T value)
        {
            _values.Add(value);
            return this;
        }

        public OptionMatcher<T, TReturn> Do(Func<T, TReturn> action)
        {
            _matcher.Some(_values, action);
            return _matcher;
        }
    }
}