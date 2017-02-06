using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, T2, T3, T4, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> With(Either<T1, Any> value1,
                                                                                              Either<T2, Any> value2,
                                                                                              Either<T3, Any> value3,
                                                                                              Either<T4, Any> value4);

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> Where(
            Func<T1, T2, T3, T4, bool> expression);

        IFuncMatcherAfterElse<TResult> Else(Func<T1, T2, T3, T4, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult result);
        TResult Result();
    }
}