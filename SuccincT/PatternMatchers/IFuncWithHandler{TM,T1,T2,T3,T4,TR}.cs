using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWithHandler<out TMatcher, T1, T2, T3, T4, in TResult>
    {
        IFuncWithHandler<TMatcher, T1, T2, T3, T4, TResult> Or(T1 value1, T2 value2, T3 value3, T4 value4);

        TMatcher Do(Func<T1, T2, T3, T4, TResult> function);

        TMatcher Do(TResult value);
    }
}