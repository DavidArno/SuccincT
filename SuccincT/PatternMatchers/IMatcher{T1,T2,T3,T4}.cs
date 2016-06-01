using System;

namespace SuccincT.PatternMatchers
{
    public interface IMatcher<T1, T2, T3, T4>
    {
        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> With(T1 value1,
                                                                                T2 value2,
                                                                                T3 value3,
                                                                                T4 value4);

        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> Where(Func<T1, T2, T3, T4, bool> function);
        IFuncMatcher<T1, T2, T3, T4, TR> To<TR>();
    }
}