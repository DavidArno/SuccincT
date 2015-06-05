using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Unions.PatternMatchers
{
    public sealed class UnionPatternCaseExpressionHandler<TMatcher, TValue>
    {
        private readonly List<TValue> _values;
        private readonly Action<Func<TValue, bool>, Action<TValue>> _recorder;
        private readonly TMatcher _matcher;

        internal UnionPatternCaseExpressionHandler(TValue value,
                                                   Action<Func<TValue, bool>, Action<TValue>> recorder,
                                                   TMatcher matcher)
        {
            _values = new List<TValue> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public UnionPatternCaseExpressionHandler<TMatcher, TValue> Or(TValue value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Action<TValue> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<TValue>.Default.Equals(x, y)), action);
            return _matcher;
        }
    }
}