using System;

namespace SuccincT.PatternMatchers
{
    public interface IMatcher<T1>
    {
        IActionWithHandler<IActionMatcher<T1>, T1> With(T1 value);
        IActionWhereHandler<IActionMatcher<T1>, T1> Where(Func<T1, bool> expression);
        IFuncMatcher<T1, TR> To<TR>();
    }
}