using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// Defines a set of string extension functions for parsing float, double and decimal
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    public static class FloatParsers
    {
        /// <summary>
        /// Parses the current string for a 32 bit float value.
        /// </summary>
        /// <returns>If successful, the float value. Otherwise result.Successful is false.</returns>
        public static Option<float> ParseFloat(this string source)
        {
            return ReflectionBasedParser.Parse<float>(source);
        }

        /// <summary>
        /// Parses the current string for a 64 bit float value.
        /// </summary>
        /// <returns>If successful, the double value. Otherwise result.Successful is false.</returns>
        public static Option<double> ParseDouble(this string source)
        {
            return ReflectionBasedParser.Parse<double>(source);
        }

        /// <summary>
        /// Parses the current string for a 128 bit float value.
        /// </summary>
        /// <returns>If successful, the double value. Otherwise result.Successful is false.</returns>
        public static Option<decimal> ParseDecimal(this string source)
        {
            return ReflectionBasedParser.Parse<decimal>(source);
        }
    }
}