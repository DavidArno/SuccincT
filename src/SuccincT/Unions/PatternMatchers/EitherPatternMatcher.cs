using System;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class EitherPatternMatcher<TLeft, TRight, TResult> :
        IEitherFuncPatternMatcher<TLeft, TRight, TResult>,
        IEitherActionPatternMatcher<TLeft, TRight>,
        IUnionFuncPatternMatcherAfterElse<TResult>,
        IUnionActionPatternMatcherAfterElse
    {
        private readonly Either<TLeft, TRight> _either;

        private readonly MatchSelectorsForEither<TLeft, TRight, TResult> _selector =
            MatchSelectorsCreator.CreateEitherSelectors<TLeft, TRight, TResult>();

        internal EitherPatternMatcher(Either<TLeft, TRight> either) => _either = either;

        IUnionFuncPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TLeft, TResult>
            IEitherFuncPatternMatcher<TLeft, TRight, TResult>.Left()
        {
            return new UnionPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TLeft, TResult>(
                _selector.RecordAction,
                this);
        }

        IUnionFuncPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TRight, TResult>
            IEitherFuncPatternMatcher<TLeft, TRight, TResult>.Right()
        {
            return new UnionPatternCaseHandler<IEitherFuncPatternMatcher<TLeft, TRight, TResult>, TRight, TResult>(
                _selector.RecordAction,
                this);
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IEitherFuncPatternMatcher<TLeft, TRight, TResult>.Else(
            Func<Either<TLeft, TRight>, TResult> elseFunc)
        {
            _selector.RecordElseFunction(elseFunc);
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IEitherFuncPatternMatcher<TLeft, TRight, TResult>.Else(
            TResult elseValue)
        {
            _selector.RecordElseFunction(Func((Either<TLeft, TRight> _) => elseValue));
            return this;
        }

        TResult IEitherFuncPatternMatcher<TLeft, TRight, TResult>.Result() => _selector.ResultNoElse(_either);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result() => _selector.ResultUsingElse(_either);

        IUnionActionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TLeft>
            IEitherActionPatternMatcher<TLeft, TRight>.Left()
        {
            return new UnionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TLeft, Unit>(
                _selector.RecordAction,
                this);
        }

        IUnionActionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TRight>
            IEitherActionPatternMatcher<TLeft, TRight>.Right()
        {
            return new UnionPatternCaseHandler<IEitherActionPatternMatcher<TLeft, TRight>, TRight, Unit>(
                _selector.RecordAction,
                this);
        }

        IUnionActionPatternMatcherAfterElse IEitherActionPatternMatcher<TLeft, TRight>.Else(
            Action<Either<TLeft, TRight>> elseAction)
        {
            _selector.RecordElseAction(elseAction);
            return this;
        }

        IUnionActionPatternMatcherAfterElse IEitherActionPatternMatcher<TLeft, TRight>.IgnoreElse()
        {
            _selector.RecordElseAction(Action((Either<TLeft, TRight> _) => { }));
            return this;
        }

        void IEitherActionPatternMatcher<TLeft, TRight>.Exec() => _selector.ExecNoElse(_either);

        void IUnionActionPatternMatcherAfterElse.Exec() => _selector.ExecUsingElse(_either);
    }
}