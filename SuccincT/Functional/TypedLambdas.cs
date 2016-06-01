using System;
using System.Collections.Generic;

namespace SuccincT.Functional
{
    public static class TypedLambdas
    {
        public static Func<TR> Lambda<TR>(Func<TR> f) => f;

        public static Func<T1, TR> Lambda<T1, TR>(Func<T1, TR> f) => f;

        public static Func<T1, T2, TR> Lambda<T1, T2, TR>(Func<T1, T2, TR> f) => f;

        public static Func<T1, T2, T3, TR> Lambda<T1, T2, T3, TR>(Func<T1, T2, T3, TR> f) => f;

        public static Func<T1, T2, T3, T4, TR> Lambda<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, TR> Lambda<T1, T2, T3, T4, T5, TR>(Func<T1, T2, T3, T4, T5, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, TR> Lambda<T1, T2, T3, T4, T5, T6, TR>(
            Func<T1, T2, T3, T4, T5, T6, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, TR> Lambda<T1, T2, T3, T4, T5, T6, T7, TR>(
            Func<T1, T2, T3, T4, T5, T6, T7, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TR> Lambda<T1, T2, T3, T4, T5, T6, T7, T8, TR>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Lambda<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR> Lambda
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR> f) => f;

        public static Action<T> Lambda<T>(Action<T> f) => f;

        internal static Func<Tuple<object, object>, IList<Tuple<object, object>>, bool> Lambda()
        {
            throw new NotImplementedException();
        }
    }
}
