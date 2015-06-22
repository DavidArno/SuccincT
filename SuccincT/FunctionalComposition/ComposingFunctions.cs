using System;

namespace SuccincT.FunctionalComposition
{
    public static class ComposingFunctions
    {
        /// <summary>
        /// Composes f(p1,p2), via f.Compose(v1), into f'(p2)
        /// </summary>
        public static Func<T2, TResult>
            Compose<T1, T2, TResult>(this Func<T1, T2, TResult> functionToCompose, T1 p1)
        {
            return p2 => functionToCompose(p1, p2);
        }

        /// <summary>
        /// Composes f(p1,p2,p3), via f.Compose(v1), into f'(p2,p3)
        /// </summary>
        public static Func<T2, T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 p1)
        {
            return (p2, p3) => functionToCompose(p1, p2, p3);
        }

        /// <summary>
        /// Composes f(p1,p2,p3), via f.Compose(v1,v2), into f'(p3)
        /// </summary>
        public static Func<T3, TResult>
            Compose<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToCompose, T1 p1, T2 p2)
        {
            return p3 => functionToCompose(p1, p2, p3);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4), via f.Compose(v1), into f'(p2,p3,p4)
        /// </summary>
        public static Func<T2, T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1)
        {
            return (p2, p3, p4) => functionToCompose(p1, p2, p3, p4);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4), via f.Compose(v1,v2), into f'(p3,p4)
        /// </summary>
        public static Func<T3, T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1, T2 p2)
        {
            return (p3, p4) => functionToCompose(p1, p2, p3, p4);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4), via f.Compose(v1,v2,v3), into f'(p4)
        /// </summary>
        public static Func<T4, TResult>
            Compose<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToCompose, T1 p1, T2 p2, T3 p3)
        {
            return p4 => functionToCompose(p1, p2, p3, p4);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5), via f.Compose(v1), into f'(p2,p3,p4,p5)
        /// </summary>
        public static Func<T2, T3, T4, T5, TResult>
            Compose<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToCompose,
                                                 T1 p1)
        {
            return (p2, p3, p4, p5) => functionToCompose(p1, p2, p3, p4, p5);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5), via f.Compose(v1,v2), into f'(p3,p4,p5)
        /// </summary>
        public static Func<T3, T4, T5, TResult>
            Compose<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToCompose,
                                                 T1 p1,
                                                 T2 p2)
        {
            return (p3, p4, p5) => functionToCompose(p1, p2, p3, p4, p5);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5), via f.Compose(v1,v2,v3), into f'(p4,p5)
        /// </summary>
        public static Func<T4, T5, TResult>
            Compose<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToCompose,
                                                 T1 p1,
                                                 T2 p2,
                                                 T3 p3)
        {
            return (p4, p5) => functionToCompose(p1, p2, p3, p4, p5);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5), via f.Compose(v1,v2,v3,v4), into f'(p5)
        /// </summary>
        public static Func<T5, TResult>
            Compose<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToCompose,
                                                 T1 p1,
                                                 T2 p2,
                                                 T3 p3,
                                                 T4 p4)
        {
            return (p5) => functionToCompose(p1, p2, p3, p4, p5);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.Compose(v1), into f'(p2,p3,p4,p5,p6)
        /// </summary>
        public static Func<T2, T3, T4, T5, T6, TResult>
            Compose<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                     T1 p1)
        {
            return (p2, p3, p4, p5, p6) => functionToCompose(p1, p2, p3, p4, p5, p6);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.Compose(v1,v2), into f'(p3,p4,p5,p6)
        /// </summary>
        public static Func<T3, T4, T5, T6, TResult>
            Compose<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                     T1 p1,
                                                     T2 p2)
        {
            return (p3, p4, p5, p6) => functionToCompose(p1, p2, p3, p4, p5, p6);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.Compose(v1,v2,v3), into f'(p4,p5,p6)
        /// </summary>
        public static Func<T4, T5, T6, TResult>
            Compose<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                     T1 p1,
                                                     T2 p2,
                                                     T3 p3)
        {
            return (p4, p5, p6) => functionToCompose(p1, p2, p3, p4, p5, p6);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.Compose(v1,v2,v3,v4), into f'(p5, p6)
        /// </summary>
        public static Func<T5, T6, TResult>
            Compose<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                     T1 p1,
                                                     T2 p2,
                                                     T3 p3,
                                                     T4 p4)
        {
            return (p5, p6) => functionToCompose(p1, p2, p3, p4, p5, p6);
        }

        /// <summary>
        /// Composes f(p1,p2,p3,p4,p5,p6), via f.Compose(v1,v2,v3,v4,v5), into f'(p6)
        /// </summary>
        public static Func<T6, TResult>
            Compose<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToCompose,
                                                     T1 p1,
                                                     T2 p2,
                                                     T3 p3,
                                                     T4 p4,
                                                     T5 p5)
        {
            return (p6) => functionToCompose(p1, p2, p3, p4, p5, p6);
        }
    }
}