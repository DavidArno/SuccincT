using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWhereHandler<out TMatcher, out T1, out T2, out T3>
    {
        TMatcher Do(Action<T1, T2, T3> action);
    }
}