using System;

namespace SuccincT.PatternMatchers
{
    public interface ITypeMatcherCaseHandler<out TMatcher, out T, in TResult>
    {
        TMatcher Do(TResult value);
        TMatcher Do(Func<T, TResult> func);
        IFuncWhereHandler<TMatcher, T, TResult> Where(Func<T, bool> expression);
    }
}


