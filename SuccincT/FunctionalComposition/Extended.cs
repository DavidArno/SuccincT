using System;

namespace SuccincT.FunctionalComposition
{
    public static class Extended
    {
        public static Func<T2, T3> Compose<T1, T2, T3>(Func<T1, T2, T3> functionToCompose, T1 param1)
        {
            return param2 => functionToCompose(param1, param2);
        }

        public static Func<T1, T3> Compose<T1, T2, T3>(Func<T1, T2, T3> functionToCompose, T2 param2)
        {
            return param1 => functionToCompose(param1, param2);
        }

        public static Func<T2, T3, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T1 param1)
        {
            return (param2, param3) => functionToCompose(param1, param2, param3);
        }

        public static Func<T1, T3, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T2 param2)
        {
            return (param1, param3) => functionToCompose(param1, param2, param3);
        }

        public static Func<T1, T2, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T3 param3)
        {
            return (param1, param2) => functionToCompose(param1, param2, param3);
        }

        public static Func<T3, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T1 param1, T2 param2)
        {
            return param3 => functionToCompose(param1, param2, param3);
        }

        public static Func<T2, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T1 param1, T3 param3)
        {
            return param2 => functionToCompose(param1, param2, param3);
        }

        public static Func<T1, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T2 param2, T3 param3)
        {
            return param1 => functionToCompose(param1, param2, param3);
        }
    }
}
