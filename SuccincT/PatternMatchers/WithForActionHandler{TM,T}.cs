using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForActionHandler<TMatcher, T>
    {
        private readonly List<T> _values;
        private readonly Action<Func<T, bool>, Action<T>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForActionHandler(T value,
                                      Action<Func<T, bool>, Action<T>> recorder,
                                      TMatcher matcher)
        {
            _values = new List<T> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForActionHandler<TMatcher, T> Or(T value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Action<T> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T>.Default.Equals(x, y)), action);
            return _matcher;
        }
    }
}