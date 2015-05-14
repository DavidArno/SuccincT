using SuccincT.Options;

namespace SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Class wrapper around the reflection-based internal Parse method used by the float and int parsers.
    /// </summary>
    internal static class ReflectionBasedParser 
    {
        // Assumes TParseType has a TryParse(string, out TParseType) method and invokes it via
        // reflection. Then constructs a Success<TParseType> result from the results of that invoke.
        internal static Option<TParseType> Parse<TParseType>(string source)
        {
            var value = default(TParseType);
            var arguments = new object[] { source, value };
            var argumentTypes = new[] { typeof(string), typeof(TParseType).MakeByRefType() };
            var success = (bool)typeof(TParseType).GetMethod("TryParse", argumentTypes).Invoke(null, arguments);

            return success ? Option<TParseType>.Some((TParseType)arguments[1]) : Option<TParseType>.None();
        }
    }
}