using System;
using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a string extension function for parsing enum values in an elegant fashion 
    /// (avoiding exception throwing).
    /// </summary>
    [CLSCompliant(false)]
    public static class EnumParser
    {
        /// <summary>
        /// Parses the source string for a value from the specified enum and returns the success/
        /// failure result as an Option instance.
        /// </summary>
        /// <typeparam name="T">
        /// Please note, this must be a valid enum. It's not possible to constrain T to Enum, the
        /// compiler throws an error if one tries to. An ArgumentException will be thrown if T is
        /// not an enum.
        /// </typeparam>
        public static Option<T> ParseEnum<T>(this string source) where T : struct, IConvertible
        {
            return Parse<T>(source, false);
        }

        /// <summary>
        /// Parses the source string for a value from the specified enum, ignoring the case of the
        /// enum values, and returns the success/failure result as an Option instance.
        /// </summary>
        /// <typeparam name="T">
        /// Please note, this must be a valid enum. It's not possible to constrain T to Enum, the
        /// compiler throws an error if one tries to. An ArgumentException will be thrown if T is
        /// not an enum.
        /// </typeparam>
        public static Option<T> ParseEnumIgnoringCase<T>(this string source) where T : struct, IConvertible
        {
            return Parse<T>(source, true);
        }

        private static Option<T> Parse<T>(string source, bool ignoreCase)
        {
            if (!typeof(T).IsEnum) { throw new ArgumentException("T must be an enumerated type"); }

            try
            {
                var value = (T)Enum.Parse(typeof(T), source, ignoreCase);
                return Option<T>.Some(value);
            }
            catch (ArgumentException)
            {
                return Option<T>.None();
            }
        }
    }
}
