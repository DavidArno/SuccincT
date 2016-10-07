using System;

namespace SuccincT.PatternMatchers
{
    public interface IMatcher<T1,T2>
    {
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(T1 value1, T2 value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(Any value1, T2 value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(T1 value1, Any value2);
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(Any value1, Any value2);
        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2> Where(Func<T1, T2, bool> function);
        IFuncMatcher<T1, T2, TR> To<TR>();
    }
}