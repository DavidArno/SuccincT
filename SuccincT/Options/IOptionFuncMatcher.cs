using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IOptionFuncMatcher<T, TResult>
    {
        IUnionFuncPatternCaseHandler<IOptionFuncMatcher<T, TResult>, T, TResult> Some();
        INoneFuncMatchHandler<T, TResult> None();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult elseValue);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Option<T>, TResult> elseAction);

        TResult Result();
    }
}