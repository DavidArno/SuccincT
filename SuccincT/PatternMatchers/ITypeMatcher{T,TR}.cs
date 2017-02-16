using System;

namespace SuccincT.PatternMatchers
{
    public interface ITypeMatcher<T, TResult>
    {
        ITypeMatcherCaseHandler<ITypeMatcher<T, TResult>, TCaseType, TResult> CaseOf<TCaseType>() where TCaseType : T;

        ITypeMatcherFuncPatternMatcherAfterElse<TResult> Else(Func<T, TResult> elseAction);

        ITypeMatcherFuncPatternMatcherAfterElse<TResult> Else(TResult value);

        TResult Result();
    }
}