using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionWhereHandler<out TMatcher, out T1, out T2>
    {
        TMatcher Do(Action<T1, T2> action);
    }
}