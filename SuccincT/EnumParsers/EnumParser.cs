using System;

using SuccincT.SuccessTypes;

namespace SuccincT.EnumParsers
{
    /// <summary>
    /// Defines a string extension function for parsing enum values in an elegant fashion 
    /// (avoiding exception throwing).
    /// </summary>
    public static class EnumParser
    {
        /// <summary>
        /// Parses the source string for a value from the specified enum and returns the success/
        /// failure result as an ISuccess instance.
        /// </summary>
        /// <typeparam name="T">
        /// Please note, this must be a valid enum. It's not possible to constrain T to Enum, the
        /// compiler throws an error if one tries to. An ArgumentException will be thrown if T is
        /// not an enum.
        /// </typeparam>
        public static ISuccess<T> ParseEnum<T>(this string source) where T : struct, IConvertible
        {
            return Parse<T>(source, false);
        }

        /// <summary>
        /// Parses the source string for a value from the specified enum, ignoring the case of the
        /// enum values, and returns the success/failure result as an ISuccess instance.
        /// </summary>
        /// <typeparam name="T">
        /// Please note, this must be a valid enum. It's not possible to constrain T to Enum, the
        /// compiler throws an error if one tries to. An ArgumentException will be thrown if T is
        /// not an enum.
        /// </typeparam>
        public static ISuccess<T> ParseEnumIgnoringCase<T>(this string source) where T : struct, IConvertible
        {
            return Parse<T>(source, true);
        }

        private static ISuccess<T> Parse<T>(string source, bool ignoreCase)
        {
            if (!typeof(T).IsEnum) { throw new ArgumentException("T must be an enumerated type"); }

            try
            {
                var value = (T)Enum.Parse(typeof(T), source, ignoreCase);
                return new Success<T> { Successful = true, Value = value };
            }
            catch (ArgumentException)
            {
                return new Success<T>
                {
                    Successful = false,
                    FailureReason = string.Format("{0} is not a valid value for {1}", source, typeof(T).Name)
                };
            }
        }
    }
}
