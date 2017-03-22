using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionActionPatternMatcher<T1, T2, T3, T4>
    {
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T1> Case1();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T2> Case2();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T3> Case3();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T4> Case4();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T> CaseOf<T>();
        IUnionActionPatternMatcherAfterElse Else(Action<Union<T1, T2, T3, T4>> elseAction);
        IUnionActionPatternMatcherAfterElse IgnoreElse();
        void Exec();
    }
}