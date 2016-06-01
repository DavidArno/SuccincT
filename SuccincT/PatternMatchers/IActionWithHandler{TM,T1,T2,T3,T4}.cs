using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1, T2, T3, T4>
    {
        IActionWithHandler<TMatcher, T1, T2, T3, T4> Or(T1 value1, T2 value2, T3 value3, T4 value4);

        TMatcher Do(Action<T1, T2, T3, T4> action);
    }
}