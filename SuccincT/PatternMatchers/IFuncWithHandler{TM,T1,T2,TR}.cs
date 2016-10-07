using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWithHandler<out TMatcher, T1, T2, in TResult>
    {
        IFuncWithHandler<TMatcher, T1, T2, TResult> Or(T1 value1, T2 value2);
        IFuncWithHandler<TMatcher, T1, T2, TResult> Or(T1 value1, Any value2);
        IFuncWithHandler<TMatcher, T1, T2, TResult> Or(Any value1, T2 value2);
        IFuncWithHandler<TMatcher, T1, T2, TResult> Or(Any value1, Any value2);

        TMatcher Do(Func<T1, T2, TResult> function);

        TMatcher Do(TResult value);
    }
}