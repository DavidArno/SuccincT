using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWhereHandler<out TMatcher, out T1, out T2, out T3, out T4, in TResult>
    {
        TMatcher Do(Func<T1, T2, T3, T4, TResult> function);

        TMatcher Do(TResult value);
    }
}