using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1, T2, T3>
    {
        IActionWithHandler<TMatcher, T1, T2, T3> Or(T1 value1, T2 value2, T3 value3);

        TMatcher Do(Action<T1, T2, T3> action);
    }
}