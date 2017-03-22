using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IUnionFuncPatternMatcher<T1, T2, TResult>
    {
        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T1, TResult> Case1();

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T2, TResult> Case2();

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T, TResult> CaseOf<T>();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Union<T1, T2>, TResult> elseAction);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult value);

        TResult Result();
    }
}