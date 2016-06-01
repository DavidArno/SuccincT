using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult> With(T1 value);
        IFuncWhereHandler<IFuncMatcher<T1, TResult>, T1, TResult> Where(Func<T1, bool> expression);
        IFuncMatcherAfterElse<TResult> Else(Func<T1, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult value);
        TResult Result();
    }
}