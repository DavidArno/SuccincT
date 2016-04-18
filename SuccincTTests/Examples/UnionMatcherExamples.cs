using SuccincT.Unions;
using static System.Console;

namespace SuccincTTests.Examples
{
    public static class UnionMatcherExamples
    {
        public static string BoolOrIntMatcherExample(Union<int, bool> value) => 
            value.Match<string>()
                 .Case1().Do(i => $"int={i}")
                 .Case2().Do(b => $"bool={b}")
                 .Result();

        public static string YesNoOrIntMatcherExample(Union<int, bool> value) => 
            value.Match<string>()
                 .Case1().Do(i => $"int={i}")
                 .Case2().Of(true).Do("Yes")
                 .Case2().Of(false).Do("No")
                 .Result();

        public static string YesNo123OrOtherIntMatcherExample(Union<int, bool> value) => 
            value.Match<string>()
                 .Case1().Of(1).Or(2).Or(3).Do(i => $"{i} in range 1-3")
                 .Case1().Do(i => $"int={i}")
                 .Case2().Of(true).Do("Yes")
                 .Case2().Of(false).Do("No")
                 .Result();

        public static string SinglePositiveOddDigitAndTrueReporter(Union<int, bool> value) => 
            value.Match<string>()
                 .Case1().Of(0).Do(x => "0 isn't positive or negative")
                 .Case1().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
                 .Case1().Where(x => x > 9).Do(x => $"{x} isn't 1 digit")
                 .Case1().Where(x => x < 0).Do(i => $"{i} isn't positive")
                 .Case1().Do(x => $"{x} isn't odd")
                 .Case2().Of(true).Do("Found true")
                 .Case2().Do(b => $"{b} isn't true or single odd digit.")
                 .Result();

        public static void SinglePositiveOddDigitAndTruePrinter(Union<int, bool> value) => 
            value.Match()
                 .CaseOf<int>().Of(0).Do(x => WriteLine("0 isn't positive or negative"))
                 .CaseOf<int>().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(WriteLine)
                 .CaseOf<int>().Where(x => x > 9).Do(x => WriteLine("{0} isn't 1 digit", x))
                 .CaseOf<int>().Where(x => x < 0).Do(i => WriteLine("{0} isn't positive", i))
                 .CaseOf<int>().Do(x => WriteLine("{0} isn't odd", x))
                 .CaseOf<bool>().Of(true).Do(b => WriteLine("Found true"))
                 .Else(o => WriteLine("{0} isn't true or single odd digit.", o.Case2))
                 .Exec();

        public static string ElseExample(Union<int, bool> value) => 
            value.Match<string>()
                 .Case1().Of(0).Do("0")
                 .Case2().Of(false).Do("false")
                 .Else("??")
                 .Result();
    }
}