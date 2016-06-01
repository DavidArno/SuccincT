using SuccincT.Functional;

namespace SuccincT.PatternMatchers.GeneralMatcher
{
    /// <summary>
    /// Defines a extension method for supplying Match() to general types. Due to the way extension methods are resolved
    /// by the compiler, this is placed "further away" from the calling code (ie with a longer namespace) than the 
    /// specific-type extension methods to ensure they are chosen in preference to this general one.
    /// </summary>
    public static class GeneralMatcherExtensions
    {
        public static IMatcher<T> Match<T>(this T item) => new Matcher<T, Unit>(item);
    }
}