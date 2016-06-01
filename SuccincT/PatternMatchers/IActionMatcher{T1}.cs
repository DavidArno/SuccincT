using System;

namespace SuccincT.PatternMatchers
{
    public interface IActionMatcher<T1>
    {
        IActionWithHandler<IActionMatcher<T1>, T1> With(T1 value);
        IActionWhereHandler<IActionMatcher<T1>, T1> Where(Func<T1, bool> expression);
        IActionMatcherAfterElse Else(Action<T1> action);
        IActionMatcherAfterElse IgnoreElse();
        void Exec();
    }
}