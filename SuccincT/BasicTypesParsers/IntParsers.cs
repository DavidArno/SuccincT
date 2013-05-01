using SuccincT.SuccessTypes;

namespace SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of string extension functions for parsing byte, short, int and long
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    public static class IntParsers
    {
        /// <summary>
        /// Parses the current string for an 8 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the byte value. Otherwise result.Successful is false.</returns>
        public static ISuccess<sbyte> ParseSignedByte(this string source)
        {
            return ReflectionBasedParser.Parse<sbyte>(source);
        }

        /// <summary>
        /// Parses the current string for an 8 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the byte value. Otherwise result.Successful is false.</returns>
        public static ISuccess<byte> ParseUnsignedByte(this string source)
        {
            return ReflectionBasedParser.Parse<byte>(source);
        }

        /// <summary>
        /// Parses the current string for a 16 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<short> ParseShort(this string source)
        {
            return ReflectionBasedParser.Parse<short>(source);
        }

        /// <summary>
        /// Parses the current string for a 16 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<ushort> ParseUnsignedShort(this string source)
        {
            return ReflectionBasedParser.Parse<ushort>(source);
        }

        /// <summary>
        /// Parses the current string for a 32 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<int> ParseInt(this string source)
        {
            return ReflectionBasedParser.Parse<int>(source);
        }

        /// <summary>
        /// Parses the current string for a 32 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<uint> ParseUnsignedInt(this string source)
        {
            return ReflectionBasedParser.Parse<uint>(source);
        }

        /// <summary>
        /// Parses the current string for a 64 bit signed integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<long> ParseLong(this string source)
        {
            return ReflectionBasedParser.Parse<long>(source);
        }

        /// <summary>
        /// Parses the current string for a 64 bit unsigned integer. 
        /// </summary>
        /// <returns>If successful, the int value. Otherwise result.Successful is false.</returns>
        public static ISuccess<ulong> ParseUnsignedLong(this string source)
        {
            return ReflectionBasedParser.Parse<ulong>(source);
        }
    }
}
