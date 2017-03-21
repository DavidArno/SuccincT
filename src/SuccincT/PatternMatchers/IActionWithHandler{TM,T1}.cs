using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWithHandler<out TMatcher, T1>
    {
        IActionWithHandler<TMatcher, T1> Or(T1 value);

        TMatcher Do(Action<T1> action);
    }
}