using SuccincT.SuccessTypes;

namespace SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a string extension function for parsing boolean
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    public static class BooleanParser
    {
        /// <summary>
        /// Parses the current string for a true/false value.
        /// </summary>
        /// <returns>If successful, the boolean value. Otherwise result.Successful is false.</returns>
        public static ISuccess<bool> ParseBoolean(this string source)
        {
            return ReflectionBasedParser.Parse<bool>(source);
        }
    }
}
