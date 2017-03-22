using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWhereHandler<out TMatcher, out T1, in TResult>
    {
        TMatcher Do(Func<T1, TResult> action);

        TMatcher Do(TResult value);
    }
}