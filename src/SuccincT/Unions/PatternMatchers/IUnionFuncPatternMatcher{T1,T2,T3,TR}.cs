using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionFuncPatternMatcher<T1, T2, T3, TResult>
    {
        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T1, TResult> Case1();
        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T2, TResult> Case2();
        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T3, TResult> Case3();
        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T, TResult> CaseOf<T>();
        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult elseValue);
        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Union<T1, T2, T3>, TResult> elseFunc);
        TResult Result();
    }
}