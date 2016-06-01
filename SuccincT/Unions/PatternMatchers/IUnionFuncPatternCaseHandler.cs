using System;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionFuncPatternCaseHandler<out TMatcher, T, in TResult>
    {
        TMatcher Do(TResult value);
        TMatcher Do(Func<T, TResult> action);
        IFuncWithHandler<TMatcher, T, TResult> Of(T value);
        IFuncWhereHandler<TMatcher, T, TResult> Where(Func<T, bool> expression);
    }
}