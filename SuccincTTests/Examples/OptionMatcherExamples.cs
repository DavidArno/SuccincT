using SuccincT.Options;
using static System.Console;

namespace SuccincTTests.Examples
{
    public static class OptionMatcherExamples
    {
        public static void PrintOption(Option<int> data) => 
            data.Match()
                .Some().Do(WriteLine)
                .None().Do(() => { })
                .Exec();

        public static void OptionMatcher(Option<int> data) => 
            data.Match()
                .Some().Of(1).Or(2).Or(3).Do(WriteLine)
                .Some().Do(i => WriteLine("{0} isn't 1, 2 or 3!", i))
                .None().Do(() => { })
                .Exec();

        public static string NumberNamer(Option<int> data)
        {
            var names = new[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            return data.Match<string>()
                       .Some().Where(i => i >= 1 && i <= 9).Do(i => names[i - 1])
                       .Some().Do(x => x.ToString())
                       .None().Do("None")
                       .Result();
        }

        public static void SinglePositiveOddDigitPrinter(Option<int> data) => 
            data.Match()
                .Some().Of(0).Do(x => WriteLine("0 isn't positive or negative"))
                .Some().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(WriteLine)
                .Some().Where(x => x > 9).Do(i => WriteLine("{0} isn't 1 digit", i))
                .Some().Where(x => x < 0).Do(i => WriteLine("{0} isn't positive", i))
                .Some().Do(x => WriteLine("{0} isn't odd", x))
                .Else(o => WriteLine("There was no value"))
                .Exec();

        public static string SinglePositiveOddDigitReporter(Option<int> data) => 
            data.Match<string>()
                .Some().Of(0).Do(x => "0 isn't positive or negative")
                .Some().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
                .Some().Where(x => x > 9).Do(x => $"{x} isn't 1 digit")
                .Some().Where(x => x < 0).Do(i => $"{i} isn't positive")
                .Some().Do(x => $"{x} isn't odd")
                .None().Do("There was no value")
                .Result();
    }
}