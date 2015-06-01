using System;
using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a set of string extension functions for parsing byte, short, int and long
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    [CLSCompliant(false)]
    public static class IntParsers
    {
        /// <summary>
        /// Parses the current string for an 8 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the byte value. Otherwise result.Successful is false.</returns>
        public static Option<sbyte> ParseSignedByte(this string source)
        {
            return ReflectionBasedParser.Parse<sbyte>(source);
        }

        /// <summary>
        /// Parses the current string for an 8 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the byte value. Otherwise result.Successful is false.</returns>
        public static Option<byte> ParseUnsignedByte(this string source)
        {
            return ReflectionBasedParser.Parse<byte>(source);
        }

        /// <summary>
        /// Parses the current string for a 16 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<short> ParseShort(this string source)
        {
            return ReflectionBasedParser.Parse<short>(source);
        }

        /// <summary>
        /// Parses the current string for a 16 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<ushort> ParseUnsignedShort(this string source)
        {
            return ReflectionBasedParser.Parse<ushort>(source);
        }

        /// <summary>
        /// Parses the current string for a 32 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<int> ParseInt(this string source)
        {
            return ReflectionBasedParser.Parse<int>(source);
        }

        /// <summary>
        /// Parses the current string for a 32 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<uint> ParseUnsignedInt(this string source)
        {
            return ReflectionBasedParser.Parse<uint>(source);
        }

        /// <summary>
        /// Parses the current string for a 64 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<long> ParseLong(this string source)
        {
            return ReflectionBasedParser.Parse<long>(source);
        }

        /// <summary>
        /// Parses the current string for a 64 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static Option<ulong> ParseUnsignedLong(this string source)
        {
            return ReflectionBasedParser.Parse<ulong>(source);
        }
    }
}
