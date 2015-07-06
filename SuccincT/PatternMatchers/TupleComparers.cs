using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal static class TupleComparers
    {
        public static bool TupleEqualsItems<T1, T2>(Tuple<T1, T2> tuple, T1 value1, T2 value2)
        {
            return EqualityComparer<T1>.Default.Equals(value1, tuple.Item1) &&
                   EqualityComparer<T2>.Default.Equals(value2, tuple.Item2);
        }
    }
}