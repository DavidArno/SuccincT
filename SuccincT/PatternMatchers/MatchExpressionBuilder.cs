using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    public class MatchExpressionBuilder<T>
    {
        private readonly ExecMatcher<T> _matcher;
        private readonly List<T> _values;

        public MatchExpressionBuilder(ExecMatcher<T> matcher, T value)
        {
            _matcher = matcher;
            _values = new List<T> { value };
        }

        public MatchExpressionBuilder<T> Or(T value)
        {
            _values.Add(value);
            return this;
        }

        public ExecMatcher<T> Do(Action action)
        {
            _matcher.Case(_values, action);
            return _matcher;
        }
    }
}