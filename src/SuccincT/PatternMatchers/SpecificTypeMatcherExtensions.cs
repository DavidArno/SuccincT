using System;
using SuccincT.Functional;
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
        public static IMatcher<T> Match<T>(this Tuple<T> item) => new Matcher<T, Unit>(item.Item1);

        public static IMatcher<T1, T2> Match<T1, T2>(this Tuple<T1, T2> item) =>
            new Matcher<T1, T2, Unit>(item.AsTuple());

        public static IMatcher<T1, T2, T3> Match<T1, T2, T3>(this Tuple<T1, T2, T3> item) =>
            new Matcher<T1, T2, T3, Unit>(item.AsTuple());

        public static IMatcher<T1, T2, T3, T4> Match<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> item) =>
            new Matcher<T1, T2, T3, T4, Unit>(item.AsTuple());

        public static IMatcher<T1, T2> Match<T1, T2>(this (T1, T2) item) =>
            new Matcher<T1, T2, Unit>(item);

        public static IMatcher<T1, T2, T3> Match<T1, T2, T3>(this (T1, T2, T3) item) =>
            new Matcher<T1, T2, T3, Unit>(item);

        public static IMatcher<T1, T2, T3, T4> Match<T1, T2, T3, T4>(this (T1, T2, T3, T4) item) =>
            new Matcher<T1, T2, T3, T4, Unit>(item);

        public static IMatcher<T1, T2> Match<T1, T2>(this ITupleMatchable<T1, T2> item)
        {
            var tuple = item.PropertiesToMatch;
            return new Matcher<T1, T2, Unit>(tuple);
        }

        public static IMatcher<T1, T2, T3> Match<T1, T2, T3>(this ITupleMatchable<T1, T2, T3> item)
        {
            var tuple = item.PropertiesToMatch;
            return new Matcher<T1, T2, T3, Unit>(tuple);
        }

        public static IMatcher<T1, T2, T3, T4> Match<T1, T2, T3, T4>(this ITupleMatchable<T1, T2, T3, T4> item)
        {
            var tuple = item.PropertiesToMatch;
            return new Matcher<T1, T2, T3, T4, Unit>(tuple);
        }

        private static (T1, T2) AsTuple<T1, T2>(this Tuple<T1, T2> value) => (value.Item1, value.Item2);

        private static (T1, T2, T3) AsTuple<T1, T2, T3>(this Tuple<T1, T2, T3> value) => 
            (value.Item1, value.Item2, value.Item3);

        private static (T1, T2, T3, T4) AsTuple<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value) =>
            (value.Item1, value.Item2, value.Item3, value.Item4);
    }
}