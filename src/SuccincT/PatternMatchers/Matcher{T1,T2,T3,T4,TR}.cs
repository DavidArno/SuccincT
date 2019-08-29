using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Unions;

namespace SuccincT.PatternMatchers
{
    internal sealed class Matcher<T1, T2, T3, T4, TResult> :
        IMatcher<T1, T2, T3, T4>,
        IActionMatcher<T1, T2, T3, T4>,
        IFuncMatcher<T1, T2, T3, T4, TResult>,
        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>,
        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>,
        IActionMatcherAfterElse,
        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>,
        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>,
        IFuncMatcherAfterElse<TResult>
    {
        private readonly MatchFunctionSelector<(T1, T2, T3, T4), EitherTuple<T1, T2, T3, T4>, TResult>
            _functionSelector;

        private readonly (T1, T2, T3, T4) _item;
        private IList<EitherTuple<T1, T2, T3, T4>>? _withValues;
        private Func<(T1, T2, T3, T4), bool>? _whereExpression;
        private Func<(T1, T2, T3, T4), TResult>? _elseFunction;

        internal Matcher((T1, T2, T3, T4) item)
        {
            _withValues = null;
            _whereExpression = null;
            _elseFunction = null;

            _item = item;
            _functionSelector = new MatchFunctionSelector<(T1, T2, T3, T4), EitherTuple<T1, T2, T3, T4>, TResult>(
                x => throw new NoMatchException(
                    "No match action exists for value of " +
                    $"({_item.Item1}, {_item.Item2}, {_item.Item3}, {_item.Item4}"));
        }

        IFuncMatcher<T1, T2, T3, T4, TR> IMatcher<T1, T2, T3, T4>.To<TR>() => new Matcher<T1, T2, T3, T4, TR>(_item);

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IMatcher<T1, T2, T3, T4>.With(
            Either<T1, Any> value1,
            Either<T2, Any> value2,
            Either<T3, Any> value3,
            Either<T4, Any> value4)
        {
            _withValues = new List<EitherTuple<T1, T2, T3, T4>> {EitherTuple.Create(value1, value2, value3, value4)};
            return this;
        }

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IActionMatcher<T1, T2, T3, T4>.With(
            Either<T1, Any> value1,
            Either<T2, Any> value2,
            Either<T3, Any> value3,
            Either<T4, Any> value4)
        {
            _withValues = new List<EitherTuple<T1, T2, T3, T4>> {EitherTuple.Create(value1, value2, value3, value4)};
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>
            IFuncMatcher<T1, T2, T3, T4, TResult>.With(
                Either<T1, Any> value1,
                Either<T2, Any> value2,
                Either<T3, Any> value3,
                Either<T4, Any> value4)
        {
            _withValues = new List<EitherTuple<T1, T2, T3, T4>> {EitherTuple.Create(value1, value2, value3, value4)};
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IMatcher<T1, T2, T3, T4>.Where(
            Func<T1, T2, T3, T4, bool> function)
        {
            _whereExpression = x => function(x.Item1, x.Item2, x.Item3, x.Item4);
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IActionMatcher<T1, T2, T3, T4>.Where(
            Func<T1, T2, T3, T4, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2, x.Item3, x.Item4);
            return this;
        }

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>
            IFuncMatcher<T1, T2, T3, T4, TResult>.Where(Func<T1, T2, T3, T4, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2, x.Item3, x.Item4);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2, T3, T4>.Else(Action<T1, T2, T3, T4> action)
        {
            _elseFunction = ActionToFunc(action);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2, T3, T4>.IgnoreElse()
        {
            _elseFunction = x => default!;
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, T3, T4, TResult>.Else(
            Func<T1, T2, T3, T4, TResult> function)
        {
            _elseFunction = x => function(x.Item1, x.Item2, x.Item3, x.Item4);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, T3, T4, TResult>.Else(TResult result)
        {
            _elseFunction = x => result;
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>
            IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Or(
                Either<T1, Any> value1,
                Either<T2, Any> value2,
                Either<T3, Any> value3,
                Either<T4, Any> value4)
        {
            _withValues!.Add(EitherTuple.Create(value1, value2, value3, value4));
            return this;
        }

        IActionMatcher<T1, T2, T3, T4> IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Do(
            Action<T1, T2, T3, T4> action)
        {
            RecordFunction((x, y) => y.Any(v => v.MatchesTuple(x)), _withValues, ActionToFunc(action));
            return this;
        }

        IActionMatcher<T1, T2, T3, T4> IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Do(
            Action<T1, T2, T3, T4> action)
        {
            RecordFunction(_whereExpression!, ActionToFunc(action));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult>
            IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
                Func<T1, T2, T3, T4, TResult> function)
        {
            RecordFunction(
                (x, y) => y.Any(v => v.MatchesTuple(x)),
                _withValues,
                x => function(x.Item1, x.Item2, x.Item3, x.Item4));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult>
            IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
                TResult value)
        {
            RecordFunction((x, y) => y.Any(v => v.MatchesTuple(x)), _withValues, x => value);
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult>
            IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
                Func<T1, T2, T3, T4, TResult> function)
        {
            RecordFunction(_whereExpression!, x => function(x.Item1, x.Item2, x.Item3, x.Item4));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult>
            IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
                TResult value)
        {
            RecordFunction(_whereExpression!, x => value);
            return this;
        }

        void IActionMatcher<T1, T2, T3, T4>.Exec() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        void IActionMatcherAfterElse.Exec()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            _ = possibleResult.HasValue ? possibleResult.Value : _elseFunction!(_item);
        }

        TResult IFuncMatcher<T1, T2, T3, T4, TResult>.Result() =>
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        TResult IFuncMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            return possibleResult.HasValue ? possibleResult.Value : _elseFunction!(_item);
        }

        private void RecordFunction(
            Func<(T1, T2, T3, T4), IList<EitherTuple<T1, T2, T3, T4>>, bool> test,
            IList<EitherTuple<T1, T2, T3, T4>>? values,
            Func<(T1, T2, T3, T4), TResult> function) =>
            _functionSelector.AddTestAndAction(test, values, null, function);

        private void RecordFunction(Func<(T1, T2, T3, T4), bool> test, Func<(T1, T2, T3, T4), TResult> function) =>
            _functionSelector.AddTestAndAction(null, null, test, function);

        private static Func<(T1, T2, T3, T4), TResult> ActionToFunc(Action<T1, T2, T3, T4> action) =>
            x =>
            {
                action(x.Item1, x.Item2, x.Item3, x.Item4);
                return default!;
            };

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>
            IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Or(
                Either<T1, Any> value1,
                Either<T2, Any> value2,
                Either<T3, Any> value3,
                Either<T4, Any> value4)
        {
            _withValues!.Add(EitherTuple.Create(value1, value2, value3, value4));
            return this;
        }
    }
}