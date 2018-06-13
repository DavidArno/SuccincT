using System;
using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a string extension method for parsing enum values in an elegant fashion
    /// (avoiding exception throwing and out parameters).
    /// </summary>
    public static class EnumParsers
    {
        /// <summary>
        /// Parses the source string for a value from the specified enum and returns the success/
        /// failure result as an Option instance.
        /// </summary>
        public static Option<T> TryParseEnum<T>(this string source) where T : struct, Enum 
            => Parse<T>(source, false);

        /// <summary>
        /// Parses the source string for a value from the specified enum, ignoring the case of the
        /// enum values, and returns the success/failure result as an Option instance.
        /// </summary>
        public static Option<T> TryParseEnumIgnoringCase<T>(this string source) where T : struct, Enum
            => Parse<T>(source, true);

        private static Option<T> Parse<T>(string source, bool ignoreCase) where T : struct
            => Enum.TryParse(source, ignoreCase, out T value) ? Option<T>.Some(value) : Option<T>.None();
    }
}