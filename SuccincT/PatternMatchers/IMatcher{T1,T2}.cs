using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IMatcher<T1,T2>
    {
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> With(Either<T1, Any> value1, Either<T2, Any> value2);
        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2> Where(Func<T1, T2, bool> function);
        IFuncMatcher<T1, T2, TR> To<TR>();
    }
}