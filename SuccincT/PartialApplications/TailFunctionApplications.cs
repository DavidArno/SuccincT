using System;

namespace SuccincT.PartialApplications
{
    /// <summary>
    /// Extension methods for tail-applying C# functions (ie, methods that return a value)
    /// </summary>
    // ReSharper disable UnusedMember.Global - Obsolete
    public static class TailFunctionApplications
    {
        /// <summary>
        /// Composes f(p1,p2), via f.TailCompose(v2), into f'(p1)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, TResult> 
            TailApply<T1, T2, TResult>(this Func<T1, T2, TResult> functionToCompose, T2 p2) => 
                p1 => functionToCompose(p1, p2);

        /// <summary>
        /// Composes f(p1,p2,p3), via f.TailCompose(v3), into f'(p1,p2)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, TResult>
            TailApply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T3 p3) => 
                (p1, p2) => functionToCompose(p1, p2, p3);

        /// <summary>
        /// Composes f(p1,p2,p3,p4), via f.TailCompose(v4), into f'(p1,p2,p3)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, TResult>
            TailApply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T4 p4) => 
                (p1, p2, p3) => functionToCompose(p1, p2, p3, p4);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5), via f.TailCompose(v5), into f'(p1,p2,p3,p4)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, TResult>
            TailApply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToCompose,
                                                   T5 p5) => 
                (p1, p2, p3, p4) => functionToCompose(p1, p2, p3, p4, p5);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.TailCompose(v6), into f'(p1,p2,p3,p4)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, T5, TResult>
            TailApply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                       T6 p6) => 
            (p1, p2, p3, p4, p5) => functionToCompose(p1, p2, p3, p4, p5, p6);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6,p7), via f.TailCompose(v7), into f'(p1,p2,p3,p4,p5,p6)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, T5, T6, TResult>
            TailApply<T1, T2, T3, T4, T5, T6, T7, TResult>(
                this Func<T1, T2, T3, T4, T5, T6, T7, TResult> functionToCompose, T7 p7) => 
                    (p1, p2, p3, p4, p5, p6) => functionToCompose(p1, p2, p3, p4, p5, p6, p7);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6,p7,p8), via f.TailCompose(v8), into f'(p1,p2,p3,p4,p5,p6)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult>
            TailApply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
                this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> functionToCompose, T8 p8) => 
                    (p1, p2, p3, p4, p5, p6, p7) => functionToCompose(p1, p2, p3, p4, p5, p6, p7, p8);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6,p7,p8,p9), via f.TailCompose(v9), into f'(p1,p2,p3,p4,p5,p6,p7,p8)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
            TailApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
                this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> functionToCompose, T9 p9) => 
                    (p1, p2, p3, p4, p5, p6, p7, p8) => functionToCompose(p1, p2, p3, p4, p5, p6, p7, p8, p9);

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6,p7,p8,p9,p10), via f.TailCompose(v10), into f'(p1,p2,p3,p4,p5,p6,p7,p8,p9)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
            TailApply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
                this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> functionToCompose, T10 p10) => 
                    (p1, p2, p3, p4, p5, p6, p7, p8, p9) => functionToCompose(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
    }
}