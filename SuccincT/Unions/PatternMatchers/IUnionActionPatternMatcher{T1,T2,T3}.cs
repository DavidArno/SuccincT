using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionActionPatternMatcher<T1, T2, T3>
    {
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T1> Case1();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T2> Case2();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T3> Case3();
        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T> CaseOf<T>();
        IUnionActionPatternMatcherAfterElse Else(Action<Union<T1, T2, T3>> elseAction);
        IUnionActionPatternMatcherAfterElse IgnoreElse();
        void Exec();
    }
}