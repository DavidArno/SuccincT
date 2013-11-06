using System;

namespace SuccincT.Unions
{
    public interface IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult>
    {
        IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult> Case(Func<T1, bool> test, TResult resultIfTestPasses);
        IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult> Case(Func<T2, bool> test, TResult resultIfTestPasses);
        IFluentMatcherWithElseDefined<T1, T2, TResult> Else(TResult resultIfTestPasses);
        TResult Result();
    }
}