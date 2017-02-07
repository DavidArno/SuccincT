using System;
using System.Collections.Generic;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    internal struct EitherTuple<T1, T2, T3, T4>
    {
        private readonly Either<T1, Any> _value1;
        private readonly Either<T2, Any> _value2;
        private readonly Either<T3, Any> _value3;
        private readonly Either<T4, Any> _value4;

        internal EitherTuple(Either<T1, Any> value1,
                             Either<T2, Any> value2,
                             Either<T3, Any> value3,
                             Either<T4, Any> value4)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            _value4 = value4;
        }

        public bool MatchesTuple((T1, T2, T3, T4) tuple) =>
            (!_value1.IsLeft || EqualityComparer<T1>.Default.Equals(_value1.Left, tuple.Item1)) &&
            (!_value2.IsLeft || EqualityComparer<T2>.Default.Equals(_value2.Left, tuple.Item2)) &&
            (!_value3.IsLeft || EqualityComparer<T3>.Default.Equals(_value3.Left, tuple.Item3)) &&
            (!_value4.IsLeft || EqualityComparer<T4>.Default.Equals(_value4.Left, tuple.Item4));
    }
}
