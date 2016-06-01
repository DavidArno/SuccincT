using System;

namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, T2, T3, T4, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> With(T1 value1,
                                                                                              T2 value2,
                                                                                              T3 value3,
                                                                                              T4 value4);

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> Where(
            Func<T1, T2, T3, T4, bool> expression);

        IFuncMatcherAfterElse<TResult> Else(Func<T1, T2, T3, T4, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult result);
        TResult Result();
    }
}