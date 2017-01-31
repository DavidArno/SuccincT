using System;
using System.Collections.Generic;
using SuccincT.Functional;

namespace SuccincT.PatternMatchers
{
    internal struct EitherTuple<T1, T2>
    {
        private Either<T1, Any> _value1;
        private Either<T2, Any> _value2;

        public EitherTuple(T1 value1, T2 value2)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
        }

        public EitherTuple(Any value1, T2 value2)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
        }

        public EitherTuple(T1 value1, Any value2)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
        }

        public EitherTuple(Any value1, Any value2)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
        }

        public bool MatchesTuple(Tuple<T1, T2> tuple) =>
            (!_value1.IsLeft || EqualityComparer<T1>.Default.Equals(_value1.Left, tuple.Item1)) &&
            (!_value2.IsLeft || EqualityComparer<T2>.Default.Equals(_value2.Left, tuple.Item2));
    }
}
