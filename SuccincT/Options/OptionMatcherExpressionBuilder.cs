using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    public class OptionMatcherExpressionBuilder<T, TReturn>
    {
        private readonly OptionMatcher2<T, TReturn> _matcher;
        private readonly List<Func<T, bool>> _expressions = new List<Func<T, bool>>();

        public OptionMatcherExpressionBuilder(OptionMatcher2<T, TReturn> matcher, T value)
        {
            _matcher = matcher;
            _expressions.Add(x => EqualityComparer<T>.Default.Equals(x, value));
        }

        public OptionMatcherExpressionBuilder(OptionMatcher2<T, TReturn> matcher,
                                              Func<T, bool> testExpression)
        {
            _matcher = matcher;
            _expressions.Add(testExpression);
        }

        public OptionMatcherExpressionBuilder<T, TReturn> Or(T value)
        {
            _expressions.Add(x => EqualityComparer<T>.Default.Equals(x, value));
            return this;
        }

        public OptionMatcher2<T, TReturn> Do(Func<T, TReturn> action)
        {
            _matcher.AddMatchExpressions(_expressions, action);
            return _matcher;
        }

        public OptionMatcher2<T, TReturn> Do(Action<T> action)
        {
            _matcher.AddMatchExpressions(_expressions, x => { action(x); return default(TReturn); });
            return _matcher;
        }
    }
}