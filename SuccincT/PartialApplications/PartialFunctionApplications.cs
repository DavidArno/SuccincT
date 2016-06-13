using System;

namespace SuccincT.PartialApplications
{
    /// <summary>
    /// Extension methods for partially applying C# functions (ie, methods that return a value)
    /// </summary>
        // ReSharper disable UnusedMember.Global - Obsolete
    public static class PartialFunctionApplications
    {
        /// <summary>
        /// Partially applies f(p1,p2), via f.Apply(v1), into f'(p2)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> functionToApply, T1 p1) =>
            p2 => functionToApply(p1, p2);

        /// <summary>
        /// Partially applies f(p1,p2,p3), via f.Apply(v1), into f'(p2,p3)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToApply,
                                                                       T1 p1) =>
                                                                           (p2, p3) => functionToApply(p1, p2, p3);

        /// <summary>
        /// Partially applies f(p1,p2,p3), via f.Apply(v1,v2), into f'(p3)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToApply,
                                                                   T1 p1,
                                                                   T2 p2) =>
                                                                       p3 => functionToApply(p1, p2, p3);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4), via f.Apply(v1), into f'(p2,p3,p4)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T2, T3, T4, TResult>
            Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToApply, T1 p1) =>
                (p2, p3, p4) => functionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4), via f.Apply(v1,v2), into f'(p3,p4)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T3, T4, TResult>
            Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToApply, T1 p1, T2 p2) =>
                (p3, p4) => functionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4), via f.Apply(v1,v2,v3), into f'(p4)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T4, TResult>
            Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToApply, T1 p1, T2 p2, T3 p3) =>
                p4 => functionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5), via f.Apply(v1), into f'(p2,p3,p4,p5)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T2, T3, T4, T5, TResult>
            Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToApply, T1 p1) =>
                (p2, p3, p4, p5) => functionToApply(p1, p2, p3, p4, p5);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5), via f.Apply(v1,v2), into f'(p3,p4,p5)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T3, T4, T5, TResult>
            Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToApply,
                                               T1 p1,
                                               T2 p2) =>
                                                   (p3, p4, p5) => functionToApply(p1, p2, p3, p4, p5);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5), via f.Apply(v1,v2,v3), into f'(p4,p5)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T4, T5, TResult>
            Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToApply,
                                               T1 p1,
                                               T2 p2,
                                               T3 p3) =>
                                                   (p4, p5) => functionToApply(p1, p2, p3, p4, p5);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5), via f.Apply(v1,v2,v3,v4), into f'(p5)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T5, TResult>
            Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToApply,
                                               T1 p1,
                                               T2 p2,
                                               T3 p3,
                                               T4 p4) =>
                                                   p5 => functionToApply(p1, p2, p3, p4, p5);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5,p6), via f.Apply(v1), into f'(p2,p3,p4,p5,p6)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T2, T3, T4, T5, T6, TResult>
            Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToApply,
                                                   T1 p1) =>
                                                       (p2, p3, p4, p5, p6) => functionToApply(p1, p2, p3, p4, p5, p6);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5,p6), via f.Apply(v1,v2), into f'(p3,p4,p5,p6)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T3, T4, T5, T6, TResult>
            Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToApply,
                                                   T1 p1,
                                                   T2 p2) =>
                                                       (p3, p4, p5, p6) => functionToApply(p1, p2, p3, p4, p5, p6);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5,p6), via f.Apply(v1,v2,v3), into f'(p4,p5,p6)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T4, T5, T6, TResult>
            Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToApply,
                                                   T1 p1,
                                                   T2 p2,
                                                   T3 p3) =>
                                                       (p4, p5, p6) => functionToApply(p1, p2, p3, p4, p5, p6);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5,p6), via f.Apply(v1,v2,v3,v4), into f'(p5, p6)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T5, T6, TResult>
            Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToApply,
                                                   T1 p1,
                                                   T2 p2,
                                                   T3 p3,
                                                   T4 p4) =>
                                                       (p5, p6) => functionToApply(p1, p2, p3, p4, p5, p6);

        /// <summary>
        /// Partially applies f(p1,p2,p3,p4,p5,p6), via f.Apply(v1,v2,v3,v4,v5), into f'(p6)
        /// </summary>
        [Obsolete("The Apply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely. Please update" +
                  "your code before then to avoid compilation errors.")]
        public static Func<T6, TResult>
            Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToApply,
                                                   T1 p1,
                                                   T2 p2,
                                                   T3 p3,
                                                   T4 p4,
                                                   T5 p5) =>
                                                       p6 => functionToApply(p1, p2, p3, p4, p5, p6);
    }
}