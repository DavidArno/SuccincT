using System;

namespace SuccincT.FunctionalComposition
{
    /// <summary>
    /// Provides a set of Compose functions that can functionally compose
    /// any method with up to three parameters.
    /// </summary>
    /// <remarks>
    /// For a function T4 F(T1 p1, T2 p2, T3 p3), a new - composed - function can
    /// be obtained via Functional.Compose(F, value). The new function has the
    /// signature T4 NewF(T2 p2, T3 p3). Calling that NewF will in turn invoke the
    /// original F, with value being passed as p1.
    /// </remarks>
    public static class Functional
    {
        public static Func<T2, T3> Compose<T1, T2, T3>(Func<T1, T2, T3> functionToCompose, T1 param1)
        {
            return param2 => functionToCompose(param1, param2);
        }

        public static Func<T2, T3, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T1 param1)
        {
            return (param2, param3) => functionToCompose(param1, param2, param3);
        }

        public static Func<T3, T4> Compose<T1, T2, T3, T4>(Func<T1, T2, T3, T4> functionToCompose, T1 param1, T2 param2)
        {
            return param3 => functionToCompose(param1, param2, param3);
        }
    }
}
