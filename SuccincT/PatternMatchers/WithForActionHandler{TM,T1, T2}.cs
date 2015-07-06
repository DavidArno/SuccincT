using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForActionHandler<TMatcher, T1, T2>
    {
        private readonly List<Tuple<T1, T2>> _values;
        private readonly Action<Func<T1, T2, bool>, Action<T1, T2>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForActionHandler(Tuple<T1, T2> value,
                                      Action<Func<T1, T2, bool>, Action<T1, T2>> recorder,
                                      TMatcher matcher)
        {
            _values = new List<Tuple<T1, T2>> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForActionHandler<TMatcher, T1, T2> Or(T1 value1, T2 value2)
        {
            _values.Add(Tuple.Create(value1, value2));
            return this;
        }

        public TMatcher Do(Action<T1, T2> action)
        {
            _recorder((x, y) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, x, y)), action);
            return _matcher;
        }
    }
}