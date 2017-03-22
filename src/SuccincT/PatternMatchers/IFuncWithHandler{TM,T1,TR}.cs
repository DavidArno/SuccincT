using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWithHandler<out TMatcher, T1, in TResult>
    {
        IFuncWithHandler<TMatcher, T1, TResult> Or(T1 value);

        TMatcher Do(Func<T1, TResult> action);

        TMatcher Do(TResult value);
    }
}