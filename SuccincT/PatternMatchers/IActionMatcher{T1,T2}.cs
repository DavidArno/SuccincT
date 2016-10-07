using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionMatcher<T1, T2>
    {
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(T1 value1, T2 value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(Any value1, T2 value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(T1 value1, Any value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(Any value1, Any value2);
        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2> Where(Func<T1, T2, bool> expression);
        IActionMatcherAfterElse Else(Action<T1, T2> action);
        IActionMatcherAfterElse IgnoreElse();
        void Exec();
    }
}