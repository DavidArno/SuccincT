using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1, T2>
    {
        IActionWithHandler<TMatcher, T1, T2> Or(T1 value1, T2 value2);
        IActionWithHandler<TMatcher, T1, T2> Or(T1 value1, Any value2);
        IActionWithHandler<TMatcher, T1, T2> Or(Any value1, T2 value2);
        IActionWithHandler<TMatcher, T1, T2> Or(Any value1, Any value2);

        TMatcher Do(Action<T1, T2> action);
    }
}