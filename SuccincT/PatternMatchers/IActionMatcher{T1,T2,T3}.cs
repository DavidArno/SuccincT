using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionMatcher<T1, T2, T3>
    {
        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> With(T1 value1, T2 value2, T3 value3);
        IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> Where(Func<T1, T2, T3, bool> expression);
        IActionMatcherAfterElse Else(Action<T1, T2, T3> action);
        IActionMatcherAfterElse IgnoreElse();
        void Exec();
    }
}