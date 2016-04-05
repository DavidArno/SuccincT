using SuccincT.Options;

namespace SuccinctExamples
{
    public static class CSharp7PatternMatchOfOptionExample
    {

        public static string ValueOrNone<T>(Option<T> option) =>
            option is Some<T>(var value) ? value.ToString() : "None";
    }
}
