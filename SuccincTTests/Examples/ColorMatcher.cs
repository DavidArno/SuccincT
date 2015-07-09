using System;
using SuccincT.PatternMatchers.GeneralMatcher;

namespace SuccincTTests.Examples
{
    /*
     * This is an implementaion of the F# color matcher pattern matching example at https://msdn.microsoft.com/en-us/library/dd547125.aspx.
     */

    public static class ColorMatcher
    {
        public enum Color { Red, Green, Blue }

        public static void PrintColorName(Color color)
        {
            color.Match()
                 .With(Color.Red).Do(x => Console.WriteLine("Red"))
                 .With(Color.Green).Do(x => Console.WriteLine("Green"))
                 .With(Color.Blue).Do(x => Console.WriteLine("Blue"))
                 .Exec();
        }
    }
}