using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    public class OptionMatcherUnitExpressionBuilder<T> 
    {
        private readonly OptionMatcherUnit<T> _matcher;
        private readonly List<Func<T, bool>> _expressions = new List<Func<T, bool>>();

        public OptionMatcherUnitExpressionBuilder(OptionMatcherUnit<T> matcher, T value)
        {
            _matcher = matcher;
            _expressions.Add(x => EqualityComparer<T>.Default.Equals(x, value));
        }

        public OptionMatcherUnitExpressionBuilder(OptionMatcherUnit<T> matcher, Func<T, bool> testExpression)
        {
            _matcher = matcher;
            _expressions.Add(testExpression);
        }

        public OptionMatcherUnitExpressionBuilder<T> Or(T value)
        {
            _expressions.Add(x => EqualityComparer<T>.Default.Equals(x, value));
            return this;
        }

        public OptionMatcherUnit<T> Do(Action<T> action)
        {
            _matcher.AddMatchExpressions(_expressions, action);
            return _matcher;
        }
    }
}