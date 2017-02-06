using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1, T2, T3>
    {
        IActionWithHandler<TMatcher, T1, T2, T3> Or(Either<T1, Any> value1,
                                                    Either<T2, Any> value2,
                                                    Either<T3, Any> value3);

        TMatcher Do(Action<T1, T2, T3> action);
    }
}