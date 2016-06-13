using System;

namespace SuccincT.PartialApplications
{
    /// <summary>
    /// Action{T1, T2} equivalant delegate for when T2 is an optional parameter
    /// </summary>
    [Obsolete("The ActionWithOptionalParameter delegates are depreciated. The Action<> and Lambda<> methods should be " +
              "used instead. As of v2.1 of Succinc<T>, they will be removed completely.")]
    public delegate void ActionWithOptionalParameter<in T1, in T2>(T1 a, T2 b = default(T2));

    /// <summary>
    /// Action{T1, T2, T3} equivalant delegate for when T3 is an optional parameter
    /// </summary>
    [Obsolete("The ActionWithOptionalParameter delegates are depreciated. The Action<> and Lambda<> methods should be " +
              "used instead. As of v2.1 of Succinc<T>, they will be removed completely.")]
    public delegate void ActionWithOptionalParameter<in T1, in T2, in T3>(T1 a, T2 b, T3 c = default(T3));

    /// <summary>
    /// Action{T1, T2, T3, T4} equivalant delegate for when T4 is an optional parameter
    /// </summary>
    [Obsolete("The ActionWithOptionalParameter delegates are depreciated. The Action<> and Lambda<> methods should be " +
              "used instead. As of v2.1 of Succinc<T>, they will be removed completely.")]
    public delegate void ActionWithOptionalParameter<in T1, in T2, in T3, in T4>(T1 a, T2 b, T3 c, T4 d = default(T4));

    /// <summary>
    /// Action{T1, T2, T3, T4, T5} equivalant delegate for when T5 is an optional parameter
    /// </summary>
    [Obsolete("The ActionWithOptionalParameter delegates are depreciated. The Action<> and Lambda<> methods should be " +
              "used instead. As of v2.1 of Succinc<T>, they will be removed completely.")]
    public delegate void ActionWithOptionalParameter<in T1, in T2, in T3, in T4, in T5>
        (T1 a, T2 b, T3 c, T4 d, T5 e = default(T5));

    /// <summary>
    /// Extension methods for tail-applying C# actions (void methods)
    /// </summary>
   // ReSharper disable UnusedMember.Global - Obsolete
    public static class TailActionApplications
    {
        /// <summary>
        /// Partially applies a(p1,p2), via a.TailApply(v2), into a'(p1)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely.")]
        public static Action<T1> TailApply<T1, T2>(this Action<T1, T2> actionToApply, T2 p2) => 
            p1 => actionToApply(p1, p2);

        /// <summary>
        /// Partially applies a(p1,p2,p3), via a.TailApply(v3), into a'(p1,p2)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely.")]
        public static Action<T1, T2> TailApply<T1, T2, T3>(this Action<T1, T2, T3> actionToApply, T3 p3) => 
            (p1, p2) => actionToApply(p1, p2, p3);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely.")]
        public static Action<T1, T2, T3> TailApply<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> actionToApply, T4 p4) => 
            (p1, p2, p3) => actionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        [Obsolete("The TailApply methods have moved to the SuccincT.Functional namespace. As of v2.1 of Succinc<T>, " +
                  "they will be deleted from the SuccincT.PartialApplications namespace completely.")]
        public static Action<T1, T2, T3, T4>
            TailApply<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> actionToApply, T5 p5) => 
                (p1, p2, p3, p4) => actionToApply(p1, p2, p3, p4, p5);

        /// <summary>
        /// Partially applies a(p1,p2=default), via a.TailApply(v2), into a'(p1)
        /// </summary>
        [Obsolete("The TailApply methods that take an ActionWithOptionalParameter delegate are depreciated. The " +
                  "Action<> and Lambda<> methods should be used instead to convert the method with an optional tail" +
                  "parameter into an Action<> delegate and the TailApply methods for thosed used. As of v2.1 of " +
                  "Succinc<T>, these methods will be removed completely.")]
        public static Action<T1> TailApply<T1, T2>(this ActionWithOptionalParameter<T1, T2> actionToApply, T2 p2) => 
            p1 => actionToApply(p1, p2);

        /// <summary>
        /// Partially applies a(p1,p2,p3=default), via a.TailApply(v3), into a'(p1,p2)
        /// </summary>
        [Obsolete("The TailApply methods that take an ActionWithOptionalParameter delegate are depreciated. The " +
                  "Action<> and Lambda<> methods should be used instead to convert the method with an optional tail" +
                  "parameter into an Action<> delegate and the TailApply methods for thosed used. As of v2.1 of " +
                  "Succinc<T>, these methods will be removed completely.")]
        public static Action<T1, T2> 
            TailApply<T1, T2, T3>(this ActionWithOptionalParameter<T1, T2, T3> actionToApply, T3 p3) => 
                (p1, p2) => actionToApply(p1, p2, p3);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4=default), via a.TailApply(v4), into a'(p1,p2,p3)
        /// </summary>
        [Obsolete("The TailApply methods that take an ActionWithOptionalParameter delegate are depreciated. The " +
                  "Action<> and Lambda<> methods should be used instead to convert the method with an optional tail" +
                  "parameter into an Action<> delegate and the TailApply methods for thosed used. As of v2.1 of " +
                  "Succinc<T>, these methods will be removed completely.")]
        public static Action<T1, T2, T3>
            TailApply<T1, T2, T3, T4>(this ActionWithOptionalParameter<T1, T2, T3, T4> actionToApply, T4 p4) => 
                (p1, p2, p3) => actionToApply(p1, p2, p3, p4);

        /// <summary>
        /// Partially applies a(p1,p2,p3,p4,p5=default), via a.TailApply(v5), into a'(p1,p2,p3,p4)
        /// </summary>
        [Obsolete("The TailApply methods that take an ActionWithOptionalParameter delegate are depreciated. The " +
                  "Action<> and Lambda<> methods should be used instead to convert the method with an optional tail" +
                  "parameter into an Action<> delegate and the TailApply methods for thosed used. As of v2.1 of " +
                  "Succinc<T>, these methods will be removed completely.")]
        public static Action<T1, T2, T3, T4>
            TailApply<T1, T2, T3, T4, T5>(this ActionWithOptionalParameter<T1, T2, T3, T4, T5> actionToApply, T5 p5) => 
                (p1, p2, p3, p4) => actionToApply(p1, p2, p3, p4, p5);
    }
}