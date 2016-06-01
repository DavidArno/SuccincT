using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWhereHandler<out TMatcher, out T1, out T2, out T3, out T4>
    {
        TMatcher Do(Action<T1, T2, T3, T4> action);
    }
}