using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForFuncHandler<TMatcher, T1, T2, TResult>
    {
        private readonly List<Tuple<T1, T2>> _values;
        private readonly Action<Func<T1, T2, bool>, Func<T1, T2, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForFuncHandler(Tuple<T1, T2> value,
                                    Action<Func<T1, T2, bool>, Func<T1, T2, TResult>> recorder,
                                    TMatcher matcher)
        {
            _values = new List<Tuple<T1, T2>>
            {
                value
            };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForFuncHandler<TMatcher, T1, T2, TResult> Or(T1 value1, T2 value2)
        {
            _values.Add(Tuple.Create(value1, value2));
            return this;
        }

        public TMatcher Do(Func<T1, T2, TResult> action)
        {
            _recorder((x, y) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, x, y)), action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder((x, y) => _values.Any(tuple => TupleComparers.TupleEqualsItems(tuple, x, y)), (v1, v2) => value);
            return _matcher;
        }
    }
}