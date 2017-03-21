using System;
//
// This file is partially copied from https://github.com/247Entertainment/E247.Fun/blob/master/E247.Fun/Fun.cs & those
// copied parts are copyright (c) 2016 247Entertainment.
//
namespace SuccincT.Functional
{
    public static class TypedLambdas
    {
        public static Func<T, T> Lambda<T>(Func<T, T> f) => f;
        public static Func<T, T, T> Lambda<T>(Func<T, T, T> f) => f;
        public static Func<T, T, T, T> Lambda<T>(Func<T, T, T, T> f) => f;
        public static Func<T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T, T, T, T, T> f) => f;
        public static Func<T, T, T, T, T, T, T, T, T, T, T> Lambda<T>(Func<T, T, T, T, T, T, T, T, T, T, T> f) => f;

        public static Func<T, TResult> Transform<T, TResult>(Func<T, TResult> f) => f;
        public static Func<T, T, TResult> Transform<T, TResult>(Func<T, T, TResult> f) => f;
        public static Func<T, T, T, TResult> Transform<T, TResult>(Func<T, T, T, TResult> f) => f;
        public static Func<T, T, T, T, TResult> Transform<T, TResult>(Func<T, T, T, T, TResult> f) => f;
        public static Func<T, T, T, T, T, TResult> Transform<T, TResult>(Func<T, T, T, T, T, TResult> f) => f;
        public static Func<T, T, T, T, T, T, TResult> Transform<T, TResult>(Func<T, T, T, T, T, T, TResult> f) => f;

        public static Func<T, T, T, T, T, T, T, TResult> Transform<T, TResult>(
            Func<T, T, T, T, T, T, T, TResult> f) => f;

        public static Func<T, T, T, T, T, T, T, T, TResult> Transform<T, TResult>(
            Func<T, T, T, T, T, T, T, T, TResult> f) => f;

        public static Func<T, T, T, T, T, T, T, T, T, TResult> Transform<T, TResult>(
            Func<T, T, T, T, T, T, T, T, T, TResult> f) => f;

        public static Func<T, T, T, T, T, T, T, T, T, T, TResult> Transform<T, TResult>(
            Func<T, T, T, T, T, T, T, T, T, T, TResult> f) => f;

        public static Func<T1, TResult> Func<T1, TResult>(Func<T1, TResult> f) => f;
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> f) => f;
        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> f) => f;
        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, TResult> Func<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, TResult> Func<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Func<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Func
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> f) => f;

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Func
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> f) => f;

        public static Action Action(Action f) => f;
        public static Action<T> Action<T>(Action<T> f) => f;
        public static Action<T1, T2> Action<T1, T2>(Action<T1, T2> f) => f;
        public static Action<T1, T2, T3> Action<T1, T2, T3>(Action<T1, T2, T3> f) => f;
        public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(Action<T1, T2, T3, T4> f) => f;
        public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> f) => f;

        public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>(
            Action<T1, T2, T3, T4, T5, T6> f) => f;

        public static Action<T1, T2, T3, T4, T5, T6, T7> Action<T1, T2, T3, T4, T5, T6, T7>(
            Action<T1, T2, T3, T4, T5, T6, T7> f) => f;

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Action<T1, T2, T3, T4, T5, T6, T7, T8>(
            Action<T1, T2, T3, T4, T5, T6, T7, T8> f) => f;

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> f) => f;

        public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> f) => f;

        public static Action<T, T> Lambda<T>(Action<T, T> f) => f;
        public static Action<T, T, T> Lambda<T>(Action<T, T, T> f) => f;
        public static Action<T, T, T, T> Lambda<T>(Action<T, T, T, T> f) => f;
        public static Action<T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T> f) => f;
        public static Action<T, T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T, T> f) => f;
        public static Action<T, T, T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T, T, T> f) => f;
        public static Action<T, T, T, T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T, T, T, T> f) => f;
        public static Action<T, T, T, T, T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T, T, T, T, T> f) => f;
        public static Action<T, T, T, T, T, T, T, T, T, T> Lambda<T>(Action<T, T, T, T, T, T, T, T, T, T> f) => f;
    }
}
