using SuccincT.Options;
using SuccincT.Parsers;

namespace SuccincTTests.Examples
{
    public static class ValueOrErrorExamples
    {
        public static Option<int> IntParser(ValueOrError data) => 
            data.Match<Option<int>>()
                .Value().Where(s => s.StartsWith("Int:")).Do(s => s[4..].TryParseInt())
                .Else(Option<int>.None())
                .Result();
    }
}