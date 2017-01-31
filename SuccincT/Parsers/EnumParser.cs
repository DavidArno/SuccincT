﻿using System;
using SuccincT.Options;

#pragma warning disable CS3021 // CLSCompliant attribute not needed for dotnetcore version
namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a string extension method for parsing enum values in an elegant fashion
    /// (avoiding exception throwing and out parameters).
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
        public static Option<T> TryParseEnum<T>(this string source) where T : struct, IConvertible =>
            Parse<T>(source, false);

        /// <summary>
        /// Parses the source string for a value from the specified enum, ignoring the case of the
        /// enum values, and returns the success/failure result as an Option instance.
        /// </summary>
        /// <typeparam name="T">
        /// Please note, this must be a valid enum. It's not possible to constrain T to Enum, the
        /// compiler throws an error if one tries to. An ArgumentException will be thrown if T is
        /// not an enum.
        /// </typeparam>
        public static Option<T> TryParseEnumIgnoringCase<T>(this string source) where T : struct, IConvertible =>
            Parse<T>(source, true);

        private static Option<T> Parse<T>(string source, bool ignoreCase) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum()) { throw new ArgumentException("T must be an enumerated type"); }

            // ReSharper disable once RedundantAssignment - R# can't spot the use of it in Some() below
            var value = default(T);
            var success = Enum.TryParse(source, ignoreCase, out value);
            return success ? Option<T>.Some(value) : Option<T>.None();
        }
    }
}