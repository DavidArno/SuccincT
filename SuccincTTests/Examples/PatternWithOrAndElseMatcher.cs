using System;
using SuccincT.PatternMatchers;

namespace SuccincTTests.Examples
{
    public static class PatternWithOrAndElseMatcher
    {
        public static void Filter123(int x)
        {
            x.Match()
             .With(1).Or(2).Or(3).Do(() => Console.WriteLine("Found 1, 2, or 3!"))
             .Else(var1 => Console.WriteLine("{0}", var1))
             .Exec();
        }
    }
}
