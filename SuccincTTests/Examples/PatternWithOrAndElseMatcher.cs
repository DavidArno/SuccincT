using System;
using SuccincT.PatternMatchers;

namespace SuccincTTests.Examples
{
    public static class PatternWithOrAndElseMatcher
    {
        public static void Filter123(int x)
        {
            x.Match()
             .With(1).Or(2).Or(3).Do(i => Console.WriteLine("Found 1, 2, or 3!"))
             .Else(Console.WriteLine)
             .Exec();
        }
    }
}