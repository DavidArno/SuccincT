using SuccincT.Options;
using System;

namespace SuccincT.Parsers
{
    /// <summary>
    /// An enum parser that handles enums that are mapped to chars
    /// </summary>
    public static class EnumCharParser
    {
        /// <summary>
        /// Parses an enum that is mapped to a char
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="source">source char</param>
        /// <returns>Option for the parsed enum</returns>
        /// <exception cref="T:System.ArgumentExeption">If T is not of an Enum Type</exception>
        public static Option<T> TryParsEnum<T>(this char source) where T : struct
        {
            var sourceAsInt = (int)source;

            // Since TryParse will return true even for ints that don't
            // correspond to valid enum values, need to check using IsDefined first
            if (!Enum.IsDefined(typeof(T), sourceAsInt))
            {
                return Option<T>.None();
            }

            return sourceAsInt.ToString().TryParseEnum<T>();
        }
    }
}
