using System;

namespace SuccincT.PartialApplications
{
    /// <summary>
    /// Action{T1, T2} equivalant delegate for when T2 is an optional parameter
    /// </summary>
    public delegate void ActionWithOptionalParam<in T1, in T2>(T1 a, T2 b = default(T2));

    /// <summary>
    /// Action{T1, T2, T3} equivalant delegate for when T3 is an optional parameter
    /// </summary>
    public delegate void ActionWithOptionalParam<in T1, in T2, in T3>(T1 a, T2 b, T3 c = default(T3));

    /// <summary>
    /// Action{T1, T2, T3, T4} equivalant delegate for when T4 is an optional parameter
    /// </summary>
    public delegate void ActionWithOptionalParam<in T1, in T2, in T3, in T4>(T1 a, T2 b, T3 c, T4 d = default(T4));

    /// <summary>
    /// Action{T1, T2, T3, T4, T5} equivalant delegate for when T5 is an optional parameter
    /// </summary>
    public delegate void ActionWithOptionalParam<in T1, in T2, in T3, in T4, in T5>
        (T1 a, T2 b, T3 c, T4 d, T5 e = default(T5));

    /// <summary>
    /// Extension methods for tail-applying C# actions (void methods)
    /// </summary>
    public static class TailActionApplications
    {
        /// <summary>
        /// Partially applies a(p1,p2), via a.TailApply(v2), into a'(p1)
        /// </summary>
        public static Action<T1>
            TailApply<T1, T2>(this Action<T1, T2> actionToApply, T2 p2)
        {
            return p1 => actionToApply(p1, p2);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3), via a.TailApply(v3), into a'(p1,p2)
        /// </summary>
        public static Action<T1, T2>
            TailApply<T1, T2, T3>(this Action<T1, T2, T3> actionToApply, T3 p3)
        {
            return (p1, p2) => actionToApply(p1, p2, p3);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        public static Action<T1, T2, T3>
            TailApply<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> actionToApply, T4 p4)
        {
            return (p1, p2, p3) => actionToApply(p1, p2, p3, p4);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        public static Action<T1, T2, T3, T4>
            TailApply<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> actionToApply, T5 p5)
        {
            return (p1, p2, p3, p4) => actionToApply(p1, p2, p3, p4, p5);
        }

        /// <summary>
        /// Partially applies a(p1,p2=default), via a.TailApply(v2), into a'(p1)
        /// </summary>
        public static Action<T1>
            TailApply<T1, T2>(this ActionWithOptionalParam<T1, T2> actionToApply, T2 p2)
        {
            return p1 => actionToApply(p1, p2);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3=default), via a.TailApply(v3), into a'(p1,p2)
        /// </summary>
        public static Action<T1, T2>
            TailApply<T1, T2, T3>(this ActionWithOptionalParam<T1, T2, T3> actionToApply, T3 p3)
        {
            return (p1, p2) => actionToApply(p1, p2, p3);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4=default), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        public static Action<T1, T2, T3>
            TailApply<T1, T2, T3, T4>(this ActionWithOptionalParam<T1, T2, T3, T4> actionToApply, T4 p4)
        {
            return (p1, p2, p3) => actionToApply(p1, p2, p3, p4);
        }

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4,p5=default), via a.TailApply(v5), into a'(p1,p2,p3,p4)
        /// </summary>
        public static Action<T1, T2, T3, T4>
            TailApply<T1, T2, T3, T4, T5>(this ActionWithOptionalParam<T1, T2, T3, T4, T5> actionToApply, T5 p5)
        {
            return (p1, p2, p3, p4) => actionToApply(p1, p2, p3, p4, p5);
        }
    }
}