using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class MatchExpressionHandler<TMatcher, TValue, TResult>
    {
        private readonly List<TValue> _values;
        private readonly Action<Func<TValue, bool>, Func<TValue, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal MatchExpressionHandler(TValue value,
                                                   Action<Func<TValue, bool>, Func<TValue, TResult>> recorder,
                                                   TMatcher matcher)
        {
            _values = new List<TValue> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public MatchExpressionHandler<TMatcher, TValue, TResult> Or(TValue value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Func<TValue, TResult> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<TValue>.Default.Equals(x, y)), action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(x => _values.Any(y => EqualityComparer<TValue>.Default.Equals(x, y)), v => value);
            return _matcher;
        }
    }
}