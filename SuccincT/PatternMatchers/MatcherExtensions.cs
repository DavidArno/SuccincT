using System;

namespace SuccincT.PatternMatchers
{
    public static class MatcherExtensions
    {
        public static ExecMatcher<T> Match<T>(this Tuple<T> item) { return new ExecMatcher<T>(item.Item1); }

        public static ExecMatcher<T1, T2> Match<T1, T2>(this Tuple<T1, T2> item)
        {
            return new ExecMatcher<T1, T2>(item.Item1, item.Item2);
        }

        public static ExecMatcher<T> Match<T>(this T item) { return new ExecMatcher<T>(item); }
    }
}