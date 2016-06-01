using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWhereHandler<out TMatcher, out T1, out T2, in TResult>
    {
        TMatcher Do(Func<T1, T2, TResult> function);

        TMatcher Do(TResult value);
    }
}