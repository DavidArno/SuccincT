using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1, T2>
    {
        IActionWithHandler<TMatcher, T1, T2> Or(Either<T1, Any> value1, Either<T2, Any> value2);

        TMatcher Do(Action<T1, T2> action);
    }
}