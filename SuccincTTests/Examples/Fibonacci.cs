using SuccincT.PatternMatchers.GeneralMatcher;

namespace SuccincTTests.Examples
{
    public static class Fibonacci
    {
        public static int Fib(int n)
        {
            return n.Match().To<int>()
                    .With(0).Or(1).Do(x => x)
                    .Else(x => Fib(x - 1) + Fib(x - 2))
                    .Result();
        }
    }
}