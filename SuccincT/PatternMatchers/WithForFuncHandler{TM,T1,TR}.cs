using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForFuncHandler<TMatcher, T1, TResult>
    {
        private readonly List<T1> _values;
        private readonly Action<Func<T1, bool>, Func<T1, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForFuncHandler(T1 value,
                                    Action<Func<T1, bool>, Func<T1, TResult>> recorder,
                                    TMatcher matcher)
        {
            _values = new List<T1> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForFuncHandler<TMatcher, T1, TResult> Or(T1 value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Func<T1, TResult> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T1>.Default.Equals(x, y)), action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T1>.Default.Equals(x, y)), v => value);
            return _matcher;
        }
    }
}