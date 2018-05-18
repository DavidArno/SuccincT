using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorFuncMatcher<TValue, TError, TResult>
    {
        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TValue, TResult> Value();

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TValue, TError, TResult>, TError, TResult> Error();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<ValueOrError<TValue, TError>, TResult> elseAction);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult value);

        TResult Result();
    }
}