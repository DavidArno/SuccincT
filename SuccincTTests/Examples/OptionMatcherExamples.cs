using System;
using SuccincT.Options;

namespace SuccincTTests.Examples
{
    public static class OptionMatcherExamples
    {
        public static void PrintOption(Option<int> data)
        {
            data.Match()
                .Some(x => Console.WriteLine(x))
                .None(() => { })
                .Exec();
        }

        public static void OptionMatcher(Option<int> data)
        {
            data.Match()
                .Some(1).Or(2).Or(3).Do(x => Console.WriteLine(x))
                .Some(i => Console.WriteLine("{0} isn't 1, 2 or 3!", i))
                .None(() => { })
                .Exec();
        }

        public static string NumberNamer(Option<int> data)
        {
            return data.Match<string>()
                       .Some().Of(1).Do(x => "One")
                       .Some().Of(2).Do(x => "Two")
                       .Some().Of(3).Do(x => "Three")
                       .Some().Of(4).Do(x => "Four")
                       .Some().Do(x => x.ToString())
                       .None().Do(() => "None")
                       .Result();
        }

        public static void SinglePositiveOddDigitPrinter(Option<int> data)
        {
            data.Match()
                .Some(0, x => Console.WriteLine("0 isn't positive or negative"))
                .When(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => Console.WriteLine(x))
                .When(x => x > 9).Do(i => Console.WriteLine("{0} isn't 1 digit", i))
                .When(x => x < 0, i => Console.WriteLine("{0} isn't positive", i))
                .Some(x => Console.WriteLine("{0} isn't odd", x))
                .None(() => Console.WriteLine("There was no value"))
                .Exec();
        }

        public static string SinglePositiveOddDigitReporter(Option<int> data)
        {
            return data.Match<string>()
                .Some().Of(0).Do(x => "0 isn't positive or negative")
                .Some().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
                .Some().Where(x => x > 9).Do(x => string.Format("{0} isn't 1 digit", x))
                .Some().Where(x => x < 0).Do(i => string.Format("{0} isn't positive", i))
                .Some().Do(x => string.Format("{0} isn't odd", x))
                .None().Do(() => string.Format("There was no value"))
                .Result();
        }
    }
}
