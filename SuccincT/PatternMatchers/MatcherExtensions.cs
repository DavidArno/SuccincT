namespace SuccincT.PatternMatchers
{
    public static class MatcherExtensions
    {
        public static ExecMatcher<T> Match<T>(this T item)
        {
            return new ExecMatcher<T>(item);
        }
    }
}