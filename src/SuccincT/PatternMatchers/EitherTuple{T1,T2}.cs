using System.Collections.Generic;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    internal readonly struct EitherTuple<T1, T2>
    {
        private readonly Either<T1, Any> _value1;
        private readonly Either<T2, Any> _value2;

        public EitherTuple(Either<T1, Any> value1, Either<T2, Any> value2)
        {
            _value1 = value1;
            _value2 = value2;
        }

        public bool MatchesTuple((T1, T2) tuple) =>
            (!_value1.IsLeft || EqualityComparer<T1>.Default.Equals(_value1.Left, tuple.Item1)) &&
            (!_value2.IsLeft || EqualityComparer<T2>.Default.Equals(_value2.Left, tuple.Item2));
    }
}
