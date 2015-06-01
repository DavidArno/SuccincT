using System;

namespace SuccincT.FunctionalComposition
{
    public static class Function
    {
        public static Func<T2, TResult> 
            Compose<T1, T2, TResult>(this Func<T1, T2, TResult> functionToCompose, T1 p1)
        {
            return p2 => functionToCompose(p1, p2);
        }

        public static Func<T2, T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 p1)
        {
            return (p2, p3) => functionToCompose(p1, p2, p3);
        }

        public static Func<T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 p1, T2 p2)
        {
            return p3 => functionToCompose(p1, p2, p3);
        }

        public static Func<T2, T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1)
        {
            return (p2, p3, p4) => functionToCompose(p1, p2, p3, p4);
        }

        public static Func<T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1, T2 p2)
        {
            return (p3, p4) => functionToCompose(p1, p2, p3, p4);
        }

        public static Func<T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1, T2 p2, T3 p3)
        {
            return p4 => functionToCompose(p1, p2, p3, p4);
        }
    }
}
