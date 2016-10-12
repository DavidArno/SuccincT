namespace SuccincT.PatternMatchers
{
    internal static class EitherTuple
    {
        public static EitherTuple<T1, T2> Create<T1, T2>(T1 value1, T2 value2) => 
            new EitherTuple<T1, T2>(value1, value2);

        public static EitherTuple<T1, T2> Create<T1, T2>(Any value1, T2 value2) =>
            new EitherTuple<T1, T2>(value1, value2);

        public static EitherTuple<T1, T2> Create<T1, T2>(T1 value1, Any value2) =>
            new EitherTuple<T1, T2>(value1, value2);

        public static EitherTuple<T1, T2> Create<T1, T2>(Any value1, Any value2) =>
            new EitherTuple<T1, T2>(value1, value2);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, T2 value2, T3 value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(Any value1, T2 value2, T3 value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, Any value2, T3 value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, T2 value2, Any value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(Any value1, Any value2, T3 value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(Any value1, T2 value2, Any value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(T1 value1, Any value2, Any value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);

        public static EitherTuple<T1, T2, T3> Create<T1, T2, T3>(Any value1, Any value2, Any value3) =>
            new EitherTuple<T1, T2, T3>(value1, value2, value3);
    }
}