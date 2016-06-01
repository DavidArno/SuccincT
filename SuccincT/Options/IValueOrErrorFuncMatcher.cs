using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorFuncMatcher<TResult>
    {
        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult value);
        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<ValueOrError, TResult> elseAction);
        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult> Error();
        TResult Result();
        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult> Value();
    }
}