using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, T2, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> With(T1 value1, T2 value2);
        IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> Where(Func<T1, T2, bool> expression);
        IFuncMatcherAfterElse<TResult> Else(Func<T1, T2, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult result);
        TResult Result();
    }
}