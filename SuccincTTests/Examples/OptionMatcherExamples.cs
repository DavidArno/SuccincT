using System;
using SuccincT.Options;

namespace SuccincTTests.Examples
{
    public static class OptionMatcherExamples
    {
        public static void PrintOption(Option<int> data)
        {
            data.MatchAndExec()
                .Some(Console.WriteLine)
                .None(() => { })
                .Exec();
        }

        public static void OptionMatcher(Option<int> data)
        {
            data.MatchAndExec()
                .Some(1).Or(2).Or(3).Do(Console.WriteLine)
                .Some(i => Console.WriteLine("{0} isn't 1, 2 or 3!", i))
                .None(() => { })
                .Exec();
        }

        public static string NumberNamer(Option<int> data)
        {
            return data.Match<string>()
                       .Some(1).Do(x => "One")
                       .Some(2).Do(x => "Two")
                       .Some(3).Do(x => "Three")
                       .Some(4).Do(x => "Four")
                       .Some(x => x.ToString())
                       .None(() => "None")
                       .Result();
        }
    }
}
