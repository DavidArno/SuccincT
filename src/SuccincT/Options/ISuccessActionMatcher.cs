using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface ISuccessActionMatcher<T>
    {
        IUnionActionPatternCaseHandler<ISuccessActionMatcher<T>, T> Error();

        ISuccessActionMatchHandler<T> Success();

        IUnionActionPatternMatcherAfterElse Else(Action<Success<T>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}