using SuccincT.Options;

namespace SuccincT.Parsers
{
    /// <summary>
    /// A reflection-based internal Parse method used by the float and int parsers. This class is a trade-off between
    /// meeting DRY needs and readable code. Rather than having lots of methods that each uses a type's TryParse
    /// method, reflection is used to invoke TryParse for multiple types.
    /// </summary>
    internal static class ReflectionBasedParser
    {
        // Assumes TParseType has a TryParse(string, out TParseType) method and invokes it via
        // reflection. Then constructs a Success<TParseType> result from the results of that invoke.
        internal static Option<T> Parse<T>(string source)
        {
            var value = default(T);
            var arguments = new object[] { source, value };
            var argumentTypes = new[] { typeof(string), typeof(T).MakeByRefType() };
            var success = (bool)typeof(T).GetMethod("TryParse", argumentTypes).Invoke(null, arguments);

            return success ? Option<T>.Some((T)arguments[1]) : Option<T>.None();
        }
    }
}