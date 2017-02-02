using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcher<T1, T2, T3, TResult>
    {
        IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult> With(Either<T1, Any> value1,
                                                                                      Either<T2, Any> value2,
                                                                                      Either<T3, Any> value3);

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult> Where(
            Func<T1, T2, T3, bool> expression);

        IFuncMatcherAfterElse<TResult> Else(Func<T1, T2, T3, TResult> function);
        IFuncMatcherAfterElse<TResult> Else(TResult result);
        TResult Result();
    }
}