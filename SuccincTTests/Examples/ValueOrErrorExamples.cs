using SuccincT.Options;
using SuccincT.Parsers;

namespace SuccincTTests.Examples
{
    public static class ValueOrErrorExamples
    {
        public static Option<int> IntParser(ValueOrError data)
        {
            return data.Match<Option<int>>()
                       .Value().Where(s => s.StartsWith("Int:")).Do(s => s.Substring(4).ParseInt())
                       .Else(Option<int>.None())
                       .Result();
        }
    }
}