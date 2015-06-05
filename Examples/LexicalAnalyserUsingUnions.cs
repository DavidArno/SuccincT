using System.Collections.Generic;
using System.Linq;
using SuccincT.Parsers;
using SuccincT.Unions;

namespace SuccinctExamples
{
    /// <summary>
    /// An example that uses the Union sealed class to provide tokens from a lexical analyser (lexer). By using the union, the tokens
    /// can be "precompiled" to their native types, without using a messy value object with redundant fields.
    /// </summary>
    /// <remarks>
    /// To avoid distracting from the use of the Union sealed class, the lexer is kept simple: it splits the input on whitespace
    /// to obtain the raw text of the tokens and then yields a union value for each split item. This clearly isn't
    /// a practical way of implementing a true lexer, but should serve this example.
    /// </remarks>
    public static class LexicalAnalyserUsingUnions
    {
        public static IEnumerable<Union<string, long, bool, double>> GenerateTokens(string input)
        {
            var rawTokens = input.Split(null);
            return from rawToken in rawTokens where rawToken != string.Empty select DetermineTokenType(rawToken);
        }

        private static Union<string, long, bool, double> DetermineTokenType(string rawToken)
        {
            var possibleLong = rawToken.ParseLong();
            return possibleLong.HasValue ?
                new Union<string, long, bool, double>(possibleLong.Value) :
                DetermineIfBoolDoubleOrString(rawToken);
        }

        private static Union<string, long, bool, double> DetermineIfBoolDoubleOrString(string rawToken)
        {
            var possibleBool = rawToken.ParseBoolean();
            return possibleBool.HasValue ?
                new Union<string, long, bool, double>(possibleBool.Value) :
                DetermineIfDoubleOrString(rawToken);
        }

        private static Union<string, long, bool, double> DetermineIfDoubleOrString(string rawToken)
        {
            var possibleDouble = rawToken.ParseDouble();
            return possibleDouble.HasValue ?
                new Union<string, long, bool, double>(possibleDouble.Value) :
                new Union<string, long, bool, double>(rawToken);
        }
    }
}