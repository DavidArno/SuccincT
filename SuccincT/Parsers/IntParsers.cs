using System;
using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a set of string extension methods for parsing byte, short, int and long
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    [CLSCompliant(false)]
    public static class IntParsers
    {
        /// <summary>
        /// Parses the current string for an 8 bit signed integer and returns an Option{sbyte} with None or the value.
        /// </summary>
        public static Option<sbyte> ParseSignedByte(this string source) => ReflectionBasedParser.Parse<sbyte>(source);

        /// <summary>
        /// Parses the current string for an 8 bit unsigned integer and returns an Option{byte} with None or the value.
        /// </summary>
        public static Option<byte> ParseUnsignedByte(this string source) => ReflectionBasedParser.Parse<byte>(source);

        /// <summary>
        /// Parses the current string for a 16 bit signed integer and returns an Option{short} with None or the value.
        /// </summary>
        public static Option<short> ParseShort(this string source) => ReflectionBasedParser.Parse<short>(source);

        /// <summary>
        /// Parses the current string for a 16 bit unsigned integer and returns an Option{ushort} with None or the value.
        /// </summary>
        public static Option<ushort> ParseUnsignedShort(this string source) => 
            ReflectionBasedParser.Parse<ushort>(source);

        /// <summary>
        /// Parses the current string for a 32 bit signed integer and returns an Option{int} with None or the value.
        /// </summary>
        public static Option<int> ParseInt(this string source) => ReflectionBasedParser.Parse<int>(source);

        /// <summary>
        /// Parses the current string for a 32 bit unsigned integer and returns an Option{uint} with None or the value.
        /// </summary>
        public static Option<uint> ParseUnsignedInt(this string source) => ReflectionBasedParser.Parse<uint>(source);

        /// <summary>
        /// Parses the current string for a 64 bit signed integer and returns an Option{long} with None or the value.
        /// </summary>
        public static Option<long> ParseLong(this string source) => ReflectionBasedParser.Parse<long>(source);

        /// <summary>
        /// Parses the current string for a 64 bit unsigned integer and returns an Option{ulong} with None or the value.
        /// </summary>
        public static Option<ulong> ParseUnsignedLong(this string source) => ReflectionBasedParser.Parse<ulong>(source);
    }
}