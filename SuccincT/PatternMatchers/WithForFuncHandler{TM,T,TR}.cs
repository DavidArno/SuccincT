using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public sealed class WithForFuncHandler<TMatcher, T, TResult>
    {
        private readonly List<T> _values;
        private readonly Action<Func<T, bool>, Func<T, TResult>> _recorder;
        private readonly TMatcher _matcher;

        internal WithForFuncHandler(T value,
                                    Action<Func<T, bool>, Func<T, TResult>> recorder,
                                    TMatcher matcher)
        {
            _values = new List<T> { value };
            _recorder = recorder;
            _matcher = matcher;
        }

        public WithForFuncHandler<TMatcher, T, TResult> Or(T value)
        {
            _values.Add(value);
            return this;
        }

        public TMatcher Do(Func<T, TResult> action)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T>.Default.Equals(x, y)), action);
            return _matcher;
        }

        public TMatcher Do(TResult value)
        {
            _recorder(x => _values.Any(y => EqualityComparer<T>.Default.Equals(x, y)), v => value);
            return _matcher;
        }
    }
}