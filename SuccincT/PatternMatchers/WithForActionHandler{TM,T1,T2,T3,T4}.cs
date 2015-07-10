using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForActionHandler<TMatcher, T1, T2, T3, T4>
    {
        private readonly List<Tuple<T1, T2, T3, T4>> _values;
        private readonly Action<Func<T1, T2, T3, T4, bool>, Action<T1, T2, T3, T4>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForActionHandler(Tuple<T1, T2, T3, T4> value,
                                      Action<Func<T1, T2, T3, T4, bool>, Action<T1, T2, T3, T4>> recorder,
                                      TMatcher matcher)
        {
            _values = new List<Tuple<T1, T2, T3, T4>> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForActionHandler<TMatcher, T1, T2, T3, T4> Or(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _values.Add(Tuple.Create(value1, value2, value3, value4));
            return this;
        }

        public TMatcher Do(Action<T1, T2, T3, T4> action)
        {
            _recorder((w, x, y, z) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, w, x, y, z)), action);
            return _matcher;
        }
    }
}