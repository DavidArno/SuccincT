using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorActionMatcher
    {
        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Value();

        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Error();

        IUnionActionPatternMatcherAfterElse Else(Action<ValueOrError> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}