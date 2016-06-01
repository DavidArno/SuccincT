using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IOptionFuncMatcher<T, TResult>
    {
        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult elseValue);
        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Option<T>, TResult> elseAction);
        INoneFuncMatchHandler<T, TResult> None();
        TResult Result();
        IUnionFuncPatternCaseHandler<IOptionFuncMatcher<T, TResult>, T, TResult> Some();
    }
}