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
        public static Option<sbyte> TryParseSignedByte(this string source) =>
            sbyte.TryParse(source, out var result) ? Option<sbyte>.Some(result) : Option<sbyte>.None();

        /// <summary>
        /// Parses the current string for an 8 bit unsigned integer and returns an Option{byte} with None or the value.
        /// </summary>
        public static Option<byte> TryParseUnsignedByte(this string source) =>
            byte.TryParse(source, out var result) ? Option<byte>.Some(result) : Option<byte>.None();

        /// <summary>
        /// Parses the current string for a 16 bit signed integer and returns an Option{short} with None or the value.
        /// </summary>
        public static Option<short> TryParseShort(this string source) =>
            short.TryParse(source, out var result) ? Option<short>.Some(result) : Option<short>.None();

        /// <summary>
        /// Parses the current string for a 16 bit unsigned integer and returns an Option{ushort} with None or the value.
        /// </summary>
        public static Option<ushort> TryParseUnsignedShort(this string source) =>
            ushort.TryParse(source, out var result) ? Option<ushort>.Some(result) : Option<ushort>.None();

        /// <summary>
        /// Parses the current string for a 32 bit signed integer and returns an Option{int} with None or the value.
        /// </summary>
        public static Option<int> TryParseInt(this string source) =>
            int.TryParse(source, out var result) ? Option<int>.Some(result) : Option<int>.None();

        /// <summary>
        /// Parses the current string for a 32 bit unsigned integer and returns an Option{uint} with None or the value.
        /// </summary>
        public static Option<uint> TryParseUnsignedInt(this string source) =>
            uint.TryParse(source, out var result) ? Option<uint>.Some(result) : Option<uint>.None();

        /// <summary>
        /// Parses the current string for a 64 bit signed integer and returns an Option{long} with None or the value.
        /// </summary>
        public static Option<long> TryParseLong(this string source) =>
            long.TryParse(source, out var result) ? Option<long>.Some(result) : Option<long>.None();

        /// <summary>
        /// Parses the current string for a 64 bit unsigned integer and returns an Option{ulong} with None or the value.
        /// </summary>
        public static Option<ulong> TryParseUnsignedLong(this string source) =>
            ulong.TryParse(source, out var result) ? Option<ulong>.Some(result) : Option<ulong>.None();
    }
}