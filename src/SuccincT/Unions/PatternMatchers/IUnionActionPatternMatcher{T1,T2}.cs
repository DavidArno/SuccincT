using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionActionPatternMatcher<T1, T2>
    {
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T1> Case1();

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T2> Case2();

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T> CaseOf<T>();

        IUnionActionPatternMatcherAfterElse Else(Action<Union<T1, T2>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}