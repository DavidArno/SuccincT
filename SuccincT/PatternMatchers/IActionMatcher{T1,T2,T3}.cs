using System;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    public interface IActionMatcher<T1, T2, T3>
    {
        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> With(Either<T1, Any> value1,
                                                                        Either<T2, Any> value2,
                                                                        Either<T3, Any> value3);

        IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> Where(Func<T1, T2, T3, bool> expression);

        IActionMatcherAfterElse Else(Action<T1, T2, T3> action);
        IActionMatcherAfterElse IgnoreElse();
        void Exec();
    }
}