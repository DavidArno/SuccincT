using System;

namespace SuccincT.FunctionalComposition
{
    public static class Extended
    {
        public static Func<T1, TResult> Compose<T1, T2, TResult>(Func<T1, T2, TResult> functionToCompose, T2 param2)
        {
            return param1 => functionToCompose(param1, param2);
        }

        public static Func<T1, T3, TResult> Compose<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> functionToCompose, T2 param2)
        {
            return (param1, param3) => functionToCompose(param1, param2, param3);
        }

        public static Func<T1, T2, TResult> Compose<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> functionToCompose, T3 param3)
        {
            return (param1, param2) => functionToCompose(param1, param2, param3);
        }

        public static Func<T1, T3, T4, TResult> Compose<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> functionToCompose, T2 param2)
        {
            return (param1, param3, param4) => functionToCompose(param1, param2, param3, param4);
        }

        public static Func<T1, T2, T4, TResult> Compose<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> functionToCompose, T3 param3)
        {
            return (param1, param2, param4) => functionToCompose(param1, param2, param3, param4);
        }

        public static Func<T1, T2, T3, TResult> Compose<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> functionToCompose, T4 param4)
        {
            return (param1, param2, param3) => functionToCompose(param1, param2, param3, param4);
        }
    }
}
