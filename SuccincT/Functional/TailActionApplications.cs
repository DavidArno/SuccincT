using System;

namespace SuccincT.Functional
{
    /// <summary>
    /// Extension methods for tail-applying C# actions (void methods)
    /// </summary>
    public static class TailActionApplications
    {
        /// <summary>
        /// Partially applies a(p1,p2), via a.TailApply(v2), into a'(p1)
        /// </summary>
        public static Action<T1> TailApply<T1, T2>(this Action<T1, T2> actionToApply, T2 p2) => 
            p1 => actionToApply(p1, p2);

        /// <summary>
        /// Partially applies a(p1,p2,p3), via a.TailApply(v3), into a'(p1,p2)
        /// </summary>
        public static Action<T1, T2> TailApply<T1, T2, T3>(this Action<T1, T2, T3> actionToApply, T3 p3) => 
            (p1, p2) => actionToApply(p1, p2, p3);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        public static Action<T1, T2, T3> TailApply<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> actionToApply, T4 p4) => 
            (p1, p2, p3) => actionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        public static Action<T1, T2, T3, T4>
            TailApply<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> actionToApply, T5 p5) => 
                (p1, p2, p3, p4) => actionToApply(p1, p2, p3, p4, p5);
    }
}