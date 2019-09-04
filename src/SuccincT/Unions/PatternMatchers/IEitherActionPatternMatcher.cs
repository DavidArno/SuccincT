using System;

namespace SuccincT.Unions.PatternMatchers
{
    public interface IEitherActionPatternMatcher<TLeft, TRight>
    {
        IUnionActionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TLeft> Left();

        IUnionActionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TRight> Right();

        IUnionActionPatternMatcherAfterElse Else(Action<Either<TLeft, TRight>> elseAction);

        IUnionActionPatternMatcherAfterElse IgnoreElse();

        void Exec();
    }
}