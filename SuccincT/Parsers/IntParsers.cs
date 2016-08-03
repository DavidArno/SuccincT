using System;
using SuccincT.Options;

#pragma warning disable CS3021 // CLSCompliant attribute not needed for dotnetcore version
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
        public static Option<sbyte> TryParseSignedByte(this string source) => ReflectionBasedParser.Parse<sbyte>(source);

        /// <summary>
        /// Parses the current string for an 8 bit unsigned integer and returns an Option{byte} with None or the value.
        /// </summary>
        public static Option<byte> TryParseUnsignedByte(this string source) => ReflectionBasedParser.Parse<byte>(source);

        /// <summary>
        /// Parses the current string for a 16 bit signed integer and returns an Option{short} with None or the value.
        /// </summary>
        public static Option<short> TryParseShort(this string source) => ReflectionBasedParser.Parse<short>(source);

        /// <summary>
        /// Parses the current string for a 16 bit unsigned integer and returns an Option{ushort} with None or the value.
        /// </summary>
        public static Option<ushort> TryParseUnsignedShort(this string source) => 
            ReflectionBasedParser.Parse<ushort>(source);

        /// <summary>
        /// Parses the current string for a 32 bit signed integer and returns an Option{int} with None or the value.
        /// </summary>
        public static Option<int> TryParseInt(this string source) => ReflectionBasedParser.Parse<int>(source);

        /// <summary>
        /// Parses the current string for a 32 bit unsigned integer and returns an Option{uint} with None or the value.
        /// </summary>
        public static Option<uint> TryParseUnsignedInt(this string source) => ReflectionBasedParser.Parse<uint>(source);

        /// <summary>
        /// Parses the current string for a 64 bit signed integer and returns an Option{long} with None or the value.
        /// </summary>
        public static Option<long> TryParseLong(this string source) => ReflectionBasedParser.Parse<long>(source);

        /// <summary>
        /// Parses the current string for a 64 bit unsigned integer and returns an Option{ulong} with None or the value.
        /// </summary>
        public static Option<ulong> TryParseUnsignedLong(this string source) => ReflectionBasedParser.Parse<ulong>(source);
    }
}