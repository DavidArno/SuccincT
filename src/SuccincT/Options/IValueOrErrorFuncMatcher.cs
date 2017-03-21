using System;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public interface IValueOrErrorFuncMatcher<TResult>
    {
        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult> Value();

        IUnionFuncPatternCaseHandler<IValueOrErrorFuncMatcher<TResult>, string, TResult> Error();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<ValueOrError, TResult> elseAction);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult value);

        TResult Result();
    }
}