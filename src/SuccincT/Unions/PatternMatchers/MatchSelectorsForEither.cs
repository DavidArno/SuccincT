using SuccincT.Functional;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using System;
using System.Collections.Generic;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class MatchSelectorsForEither<TLeft, TRight, TResult>
    {
        private readonly MatchFunctionSelector<TLeft, TLeft, TResult> _leftSelector;
        private readonly MatchFunctionSelector<TRight, TRight, TResult> _rightSelector;
        private Func<Either<TLeft, TRight>, TResult>? _elseFunction;

        internal MatchSelectorsForEither()
        {
            _leftSelector =
                new MatchFunctionSelector<TLeft, TLeft, TResult>(
                    x => throw new NoMatchException("No match action defined for either with Left value"));
            _rightSelector =
                new MatchFunctionSelector<TRight, TRight, TResult>(
                    x => throw new NoMatchException("No match action defined for either with Right value"));
        }

        internal void RecordAction<T>(Func<T, IList<T>, bool>? withTest,
                                      Func<T, bool>? whereTest,
                                      IList<T>? withData,
                                      Func<T, TResult> action) => 
            Selector<T>().AddTestAndAction(withTest, withData, whereTest, action);

        internal void RecordAction<T>(Func<T, IList<T>, bool>? withTest,
                                      Func<T, bool>? whereTest,
                                      IList<T>? withData,
                                      Func<T, Unit> action) => 
            Selector<T>().AddTestAndAction(withTest, withData, whereTest, action.ToFuncOf<T, TResult>());

        internal void RecordElseFunction(Func<Either<TLeft, TRight>, TResult> elseFunction) =>
            _elseFunction = elseFunction;

        internal void RecordElseAction(Action<Either<TLeft, TRight>> elseAction) =>
            _elseFunction = elseAction.ToUnitFunc() as Func<Either<TLeft, TRight>, TResult>;

        internal TResult ResultNoElse(Either<TLeft, TRight> either) =>
            DetermineResultUsingDefaultIfRequired(either);

        internal TResult ResultUsingElse(Either<TLeft, TRight> either)
        {
            var possibleResult = DetermineResult(either);
            return possibleResult.HasValue ? possibleResult.Value : ElseFunction(either);
        }

        internal void ExecNoElse(Either<TLeft, TRight> either) =>
            DetermineResultUsingDefaultIfRequired(either);

        internal void ExecUsingElse(Either<TLeft, TRight> either)
        {
            var possibleResult = DetermineResult(either);
            _ = possibleResult.HasValue ? possibleResult.Value : ElseFunction(either);
        }

        private MatchFunctionSelector<T, T, TResult> Selector<T>()
            => typeof(T) switch {
                var t when t == typeof(TLeft) => (_leftSelector as MatchFunctionSelector<T, T, TResult>)!,
                _ => (_rightSelector as MatchFunctionSelector<T, T, TResult>)!
            };

        private TResult DetermineResultUsingDefaultIfRequired(Either<TLeft, TRight> either)
            => either.IsLeft
                ? _leftSelector.DetermineResultUsingDefaultIfRequired(either.Left)
                : _rightSelector.DetermineResultUsingDefaultIfRequired(either.Right);


        private Option<TResult> DetermineResult(Either<TLeft, TRight> either)
            => either.IsLeft
                ? _leftSelector.DetermineResult(either.Left)
                : _rightSelector.DetermineResult(either.Right);

        private TResult ElseFunction(Either<TLeft, TRight> either) => _elseFunction!(either);
    }
}