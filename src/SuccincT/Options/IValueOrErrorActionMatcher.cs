using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorActionMatcher<TValue, TError>
    {
        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TValue> Value();

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher<TValue, TError>, TError> Error();

        IUnionActionPatternMatcherAfterElse Else(Action<ValueOrError<TValue, TError>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}