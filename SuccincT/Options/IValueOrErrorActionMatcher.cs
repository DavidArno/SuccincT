using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorActionMatcher
    {
        IUnionActionPatternMatcherAfterElse Else(Action<ValueOrError> elseAction);
        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Error();
        void Exec();
        IUnionActionPatternMatcherAfterElse IgnoreElse();
        IUnionActionPatternCaseHandler<IValueOrErrorActionMatcher, string> Value();
    }
}