using System;

namespace SuccincT.Unions
{
    public interface IFluentMatcherWithElseDefined<T1, T2, TResult> 
    {
        IFluentMatcherWithElseDefined<T1, T2, TResult> Case(Func<T1, bool> test, TResult resultIfTestPasses);
        IFluentMatcherWithElseDefined<T1, T2, TResult> Case(Func<T2, bool> test, TResult resultIfTestPasses);
        TResult Result();
    }
}