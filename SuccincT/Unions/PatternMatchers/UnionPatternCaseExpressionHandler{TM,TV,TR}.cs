using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Unions.PatternMatchers
{
    public class UnionPatternCaseExpressionHandler<TMatcher, TValue, TReturn>
    {
        private readonly List<TValue> _values;
        private readonly Action<Func<TValue, bool>, Func<TValue, TReturn>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseExpressionHandler(TValue value,
                                                   Action<Func<TValue, bool>, Func<TValue, TReturn>> recorder,
                                                   TMatcher matcher)
        {
            _values = new List<TValue> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public UnionPatternCaseExpressionHandler<TMatcher, TValue, TReturn> Or(TValue value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Func<TValue, TReturn> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<TValue>.Default.Equals(x, y)), action);
            return _matcher;
        }
    }
}