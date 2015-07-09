using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForActionHandler<TMatcher, T1>
    {
        private readonly List<T1> _values;
        private readonly Action<Func<T1, bool>, Action<T1>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForActionHandler(T1 value,
                                      Action<Func<T1, bool>, Action<T1>> recorder,
                                      TMatcher matcher)
        {
            _values = new List<T1> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForActionHandler<TMatcher, T1> Or(T1 value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Action<T1> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T1>.Default.Equals(x, y)), action);
            return _matcher;
        }
    }
}