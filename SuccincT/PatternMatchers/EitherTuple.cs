using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    internal static class EitherTuple
    {
        public static EitherTuple<T1, T2> Create<T1, T2>(Either<T1, Any> value1,
                                                                 Either<T2, Any> value2) =>
            new EitherTuple<T1, T2>(value1, value2);


        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(Either<T1, Any> value1,
                                                                 Either<T2, Any> value2,
                                                                 Either<T3, Any> value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(Either<T1, Any> value1,
                                                                         Either<T2, Any> value2,
                                                                         Either<T3, Any> value3,
                                                                         Either<T4, Any> value4) =>
            new EitherTuple<T1, T2, T3, T4>(value1, value2, value3, value4);
    }
}