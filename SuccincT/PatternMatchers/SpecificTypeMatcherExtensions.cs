using System;
using SuccincT.Tuples;

namespace SuccincT.PatternMatchers
{
    /// <summary>
    /// Defines extension methods for supplying Match() to specific types. Due to the way extension methods are resolved
    /// by the compiler, these are placed "closer" to the calling code (ie with a shorter namespace) than the general 
    /// type extension method to ensure these are chosen in preference to the general one.
    /// </summary>
    public static class SpecificTypeMatcherExtensions
    {
        public static ExecMatcher<T> Match<T>(this Tuple<T> item) { return new ExecMatcher<T>(item.Item1); }

        public static ExecMatcher<T1, T2> Match<T1, T2>(this Tuple<T1, T2> item)
        {
            return new ExecMatcher<T1, T2>(item.Item1, item.Item2);
        }

        public static ExecMatcher<T1, T2> Match<T1, T2>(this ITupleMatchable<T1, T2> item)
        {
            var tuple = item.PropertiesToMatch;
            return new ExecMatcher<T1, T2>(tuple.Item1, tuple.Item2);
        }

        public static ExecMatcher<T1, T2, T3> Match<T1, T2, T3>(this ITupleMatchable<T1, T2, T3> item)
        {
            var tuple = item.PropertiesToMatch;
            return new ExecMatcher<T1, T2, T3>(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static ExecMatcher<T1, T2, T3, T4> Match<T1, T2, T3, T4>(this ITupleMatchable<T1, T2, T3, T4> item)
        {
            var tuple = item.PropertiesToMatch;
            return new ExecMatcher<T1, T2, T3, T4>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static ExecMatcher<T1, T2, T3> Match<T1, T2, T3>(this Tuple<T1, T2, T3> item)
        {
            return new ExecMatcher<T1, T2, T3>(item.Item1, item.Item2, item.Item3);
        }

        public static ExecMatcher<T1, T2, T3, T4> Match<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> item)
        {
            return new ExecMatcher<T1, T2, T3, T4>(item.Item1, item.Item2, item.Item3, item.Item4);
        }
    }
}