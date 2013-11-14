using System;

namespace SuccincT.FunctionalComposition
{
    public static class Function
    {
        public static Func<T2, TResult> 
            Compose<T1, T2, TResult>(this Func<T1, T2, TResult> functionToCompose, T1 param1)
        {
            return param2 => functionToCompose(param1, param2);
        }

        public static Func<T2, T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 param1)
        {
            return (param2, param3) => functionToCompose(param1, param2, param3);
        }

        public static Func<T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 param1, T2 param2)
        {
            return param3 => functionToCompose(param1, param2, param3);
        }

        public static Func<T2, T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 param1)
        {
            return (param2, param3, param4) => functionToCompose(param1, param2, param3, param4);
        }

        public static Func<T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 param1, T2 param2)
        {
            return (param3, param4) => functionToCompose(param1, param2, param3, param4);
        }

        public static Func<T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 param1, T2 param2, T3 param3)
        {
            return param4 => functionToCompose(param1, param2, param3, param4);
        }
    }
}
