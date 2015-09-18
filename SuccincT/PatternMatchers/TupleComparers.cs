using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// Defines functions for comparing the items of a tuple with the supplied values. Used by the Exec and Func
    /// tuple pattern matchers.
    /// </summary>
    internal static class TupleComparers
    {
        public static bool TupleEqualsItems<T1, T2>(Tuple<T1, T2> tuple, T1 value1, T2 value2) => 
            EqualityComparer<T1>.Default.Equals(value1, tuple.Item1) && 
            EqualityComparer<T2>.Default.Equals(value2, tuple.Item2);

        public static bool TupleEqualsItems<T1, T2, T3>(Tuple<T1, T2, T3> tuple, T1 value1, T2 value2, T3 value3) => 
            EqualityComparer<T1>.Default.Equals(value1, tuple.Item1) &&
            EqualityComparer<T2>.Default.Equals(value2, tuple.Item2) &&
            EqualityComparer<T3>.Default.Equals(value3, tuple.Item3);

        public static bool TupleEqualsItems<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> tuple,
                                                            T1 value1,
                                                            T2 value2,
                                                            T3 value3,
                                                            T4 value4) => 
            EqualityComparer<T1>.Default.Equals(value1, tuple.Item1) &&
            EqualityComparer<T2>.Default.Equals(value2, tuple.Item2) &&
            EqualityComparer<T3>.Default.Equals(value3, tuple.Item3) &&
            EqualityComparer<T4>.Default.Equals(value4, tuple.Item4);
    }
}