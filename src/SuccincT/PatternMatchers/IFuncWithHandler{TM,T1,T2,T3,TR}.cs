using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IFuncWithHandler<out TMatcher, T1, T2, T3, in TResult>
    {
        IFuncWithHandler<TMatcher, T1, T2, T3, TResult> Or(
            Either<T1, Any> value1,
            Either<T2, Any> value2,
            Either<T3, Any> value3);

        TMatcher Do(Func<T1, T2, T3, TResult> function);

        TMatcher Do(TResult value);
    }
}