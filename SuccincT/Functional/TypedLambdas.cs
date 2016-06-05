using System;

namespace SuccincT.Functional
{
    public static class TypedLambdas
    {
        public static Func<T1, TResult> Lambda<T1, TResult>(Func<T1, TResult> f) => f;

        public static Func<T1, T2, TResult> Lambda<T1, T2, TResult>(Func<T1, T2, TResult> f) => f;

        public static Func<T1, T2, T3, TResult> Lambda<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> f) => f;

        public static Func<T1, T2, T3, T4, TResult> Lambda<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> f)
            => f;

        public static Func<T1, T2, T3, T4, T5, TResult> Lambda<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, TResult> Lambda<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Lambda<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Lambda<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Lambda
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Lambda
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> f) => f;

        public static Action Lambda(Action f) => f;

        public static Action<T> Lambda<T>(Action<T> f) => f;

        public static Action<T1, T2> Lambda<T1, T2>(Action<T1, T2> f) => f;

        public static Action<T1, T2, T3> Lambda<T1, T2, T3>(Action<T1, T2, T3> f) => f;

        public static Action<T1, T2, T3, T4> Lambda<T1, T2, T3, T4>(Action<T1, T2, T3, T4> f) => f;
    }
}
