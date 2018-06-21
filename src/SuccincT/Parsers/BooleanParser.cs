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
        /// Parses the current string for a true/false value and returns success/
        /// failure result as an Option instance.
        /// </summary>
        public static Option<bool> TryParseBoolean(this string source)
            => bool.TryParse(source, out var result) ? result : Option<bool>.None();
    }
}