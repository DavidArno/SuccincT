using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a string extension method for parsing boolean
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    public static class BooleanParser
    {
        /// <summary>
        /// Parses the current string for a true/false value and returns an Option{bool} of the result.
        /// </summary>
        public static Option<bool> ParseBoolean(this string source) => ReflectionBasedParser.Parse<bool>(source);
    }
}