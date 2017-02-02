using System;
using System.Collections.Generic;
using SuccincT.Functional;

namespace SuccincT.PatternMatchers
{
    internal struct EitherTuple<T1, T2, T3>
    {
        private Either<T1, Any> _value1;
        private Either<T2, Any> _value2;
        private Either<T3, Any> _value3;

        public EitherTuple(T1 value1, T2 value2, T3 value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(Any value1, T2 value2, T3 value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(T1 value1, Any value2, T3 value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(T1 value1, T2 value2, Any value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(Any value1, Any value2, T3 value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(Any value1, T2 value2, Any value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(T1 value1, Any value2, Any value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public EitherTuple(Any value1, Any value2, Any value3)
        {
            _value1 = new Either<T1, Any>(value1);
            _value2 = new Either<T2, Any>(value2);
            _value3 = new Either<T3, Any>(value3);
        }

        public bool MatchesTuple(Tuple<T1, T2, T3> tuple) =>
            (!_value1.IsLeft || EqualityComparer<T1>.Default.Equals(_value1.Left, tuple.Item1)) &&
            (!_value2.IsLeft || EqualityComparer<T2>.Default.Equals(_value2.Left, tuple.Item2)) &&
            (!_value3.IsLeft || EqualityComparer<T3>.Default.Equals(_value3.Left, tuple.Item3));
    }
}
