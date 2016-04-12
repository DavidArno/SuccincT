using SuccincT.Options;

namespace SuccinctExamples
{
    public static class CSharp7PatternMatchOfOptionExample
    {
        public static string ValueOrNone<T>(Option<T> option) =>
            option is Some<T>(var value) ? value.ToString() : "None";

        public static int OptionIsEvenNumber(Option<int> option) =>
            option match (
                case Some<int>(var value) when value % 2 == 1 : 1
                case None<int>() : -1
                case * : 0
            );
    }
}
