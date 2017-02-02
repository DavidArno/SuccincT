using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IMatcher<T1, T2, T3, T4>
    {
        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> With(Either<T1, Any> value1,
                                                                                Either<T2, Any> value2,
                                                                                Either<T3, Any> value3,
                                                                                Either<T4, Any> value4);

        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> Where(Func<T1, T2, T3, T4, bool> function);
        IFuncMatcher<T1, T2, T3, T4, TR> To<TR>();
    }
}