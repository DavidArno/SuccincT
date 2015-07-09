using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForFuncHandler<TMatcher, T1, T2, T3, TResult>
    {
        private readonly List<Tuple<T1, T2, T3>> _values;
        private readonly Action<Func<T1, T2, T3, bool>, Func<T1, T2, T3, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForFuncHandler(Tuple<T1, T2, T3> value,
                                    Action<Func<T1, T2, T3, bool>, Func<T1, T2, T3, TResult>> recorder,
                                    TMatcher matcher)
        {
            _values = new List<Tuple<T1, T2, T3>>
            {
                value
            };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForFuncHandler<TMatcher, T1, T2, T3, TResult> Or(T1 value1, T2 value2, T3 value3)
        {
            _values.Add(Tuple.Create(value1, value2, value3));
            return this;
        }

        public TMatcher Do(Func<T1, T2, T3, TResult> action)
        {
            _recorder((x, y, z) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, x, y, z)), action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder((x, y, z) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, x, y, z)),
                      (v1, v2, v3) => value);
            return _matcher;
        }
    }
}