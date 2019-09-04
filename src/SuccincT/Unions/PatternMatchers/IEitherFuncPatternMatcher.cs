using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IEitherFuncPatternMatcher<TLeft, TRight, TResult>
    {
        IUnionFuncPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TLeft, TResult> Left();

        IUnionFuncPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TRight, TResult> Right();

        IUnionFuncPatternMatcherAfterElse<TResult> Else(Func<Either<TLeft, TRight>, TResult> elseAction);

        IUnionFuncPatternMatcherAfterElse<TResult> Else(TResult value);

        TResult Result();
    }
}