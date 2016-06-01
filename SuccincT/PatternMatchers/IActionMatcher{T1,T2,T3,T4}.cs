using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionMatcher<T1, T2, T3, T4>
    {
        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> With(T1 value1, T2 value2, T3 value3, T4 value4);
        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> Where(Func<T1, T2, T3, T4, bool> expression);
        IActionMatcherAfterElse Else(Action<T1, T2, T3, T4> action);
        IActionMatcherAfterElse IgnoreElse();
        void Exec();
    }
}