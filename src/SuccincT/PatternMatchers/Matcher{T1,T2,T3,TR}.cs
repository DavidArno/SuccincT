using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Unions;
using static SuccincT.Functional.Unit;

namespace SuccincT.PatternMatchers
{
    internal sealed class Matcher<T1, T2, T3, TResult> :
        IMatcher<T1, T2, T3>,
        IActionMatcher<T1, T2, T3>,
        IFuncMatcher<T1, T2, T3, TResult>,
        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>,
        IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>,
        IActionMatcherAfterElse,
        IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>,
        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>,
        IFuncMatcherAfterElse<TResult>
    {
        private readonly MatchFunctionSelector<(T1, T2, T3), EitherTuple<T1, T2, T3>, TResult> _functionSelector;
        private readonly (T1, T2, T3) _item;
        private IList<EitherTuple<T1, T2, T3>> _withValues;
        private Func<(T1, T2, T3), bool> _whereExpression;
        private Func<(T1, T2, T3), TResult> _elseFunction;

        internal Matcher((T1, T2, T3) item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<(T1, T2, T3), EitherTuple<T1, T2, T3>, TResult>(
                x => throw new NoMatchException("No match action exists for value of (" +
                                                $"{_item.Item1}, {_item.Item2}, {_item.Item3}"));
        }

        IFuncMatcher<T1, T2, T3, TR> IMatcher<T1, T2, T3>.To<TR>() => new Matcher<T1, T2, T3, TR>(_item);

        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> IMatcher<T1, T2, T3>.With(Either<T1, Any> value1,
                                                                                Either<T2, Any> value2,
                                                                                Either<T3, Any> value3)
        {
            _withValues = new List<EitherTuple<T1, T2, T3>> { EitherTuple.Create(value1, value2, value3) };
            return this;
        }

        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> IActionMatcher<T1, T2, T3>.With(
            Either<T1, Any> value1,
            Either<T2, Any> value2,
            Either<T3, Any> value3)
        {
            _withValues = new List<EitherTuple<T1, T2, T3>> {EitherTuple.Create(value1, value2, value3)};
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult> IFuncMatcher<T1, T2, T3, TResult>.With(
            Either<T1, Any> value1,
            Either<T2, Any> value2,
            Either<T3, Any> value3)
        {
            _withValues = new List<EitherTuple<T1, T2, T3>> { EitherTuple.Create(value1, value2, value3) };
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> IMatcher<T1, T2, T3>.Where(
            Func<T1, T2, T3, bool> function)
        {
            _whereExpression = x => function(x.Item1, x.Item2, x.Item3);
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3> IActionMatcher<T1, T2, T3>.Where(
            Func<T1, T2, T3, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2, x.Item3);
            return this;
        }

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult> IFuncMatcher<T1, T2, T3, TResult>.
            Where(Func<T1, T2, T3, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2, x.Item3);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2, T3>.Else(Action<T1, T2, T3> action)
        {
            _elseFunction = ActionToFunc(action);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2, T3>.IgnoreElse()
        {
            _elseFunction = x => default(TResult);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, T3, TResult>.Else(Func<T1, T2, T3, TResult> function)
        {
            _elseFunction = x => function(x.Item1, x.Item2, x.Item3);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, T3, TResult>.Else(TResult result)
        {
            _elseFunction = x => result;
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>
            IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>.Or(Either<T1, Any> value1,
                                                                                        Either<T2, Any> value2,
                                                                                        Either<T3, Any> value3)
        {
            _withValues.Add(EitherTuple.Create(value1, value2, value3));
            return this;
        }

        IActionMatcher<T1, T2, T3> IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>.Do(
            Action<T1, T2, T3> action)
        {
            RecordFunction((x, y) => y.Any(v => v.MatchesTuple(x)),
                           _withValues,
                           ActionToFunc(action));
            return this;
        }

        IActionMatcher<T1, T2, T3> IActionWhereHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>.Do(
            Action<T1, T2, T3> action)
        {
            RecordFunction(_whereExpression, ActionToFunc(action));
            return this;
        }

        IFuncMatcher<T1, T2, T3, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>.Do(
            Func<T1, T2, T3, TResult> function)
        {
            RecordFunction((x, y) => y.Any(v => v.MatchesTuple(x)),
                           _withValues,
                           x => function(x.Item1, x.Item2, x.Item3));
            return this;
        }

        IFuncMatcher<T1, T2, T3, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>.Do(
            TResult value)
        {
            RecordFunction((x, y) => y.Any(v => v.MatchesTuple(x)),
                           _withValues,
                           x => value);
            return this;
        }

        IFuncMatcher<T1, T2, T3, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>.Do(
            Func<T1, T2, T3, TResult> function)
        {
            RecordFunction(_whereExpression, x => function(x.Item1, x.Item2, x.Item3));
            return this;
        }

        IFuncMatcher<T1, T2, T3, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, T3, TResult>, T1, T2, T3, TResult>.Do(
            TResult value)
        {
            RecordFunction(_whereExpression, x => value);
            return this;
        }

        void IActionMatcher<T1, T2, T3>.Exec() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        void IActionMatcherAfterElse.Exec()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            Ignore(possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item));
        }

        TResult IFuncMatcher<T1, T2, T3, TResult>.Result() =>
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        TResult IFuncMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            return possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item);
        }

        private void RecordFunction(Func<(T1, T2, T3), IList<EitherTuple<T1, T2, T3>>, bool> test,
                                    IList<EitherTuple<T1, T2, T3>> values,
                                    Func<(T1, T2, T3), TResult> function) => 
            _functionSelector.AddTestAndAction(test, values, null, function);

        private void RecordFunction(Func<(T1, T2, T3), bool> test, Func<(T1, T2, T3), TResult> function) =>
            _functionSelector.AddTestAndAction(null, null, test, function);

        private static Func<(T1, T2, T3), TResult> ActionToFunc(Action<T1, T2, T3> action) =>
            x =>
            {
                action(x.Item1, x.Item2, x.Item3);
                return default(TResult);
            };

        IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>
            IActionWithHandler<IActionMatcher<T1, T2, T3>, T1, T2, T3>.Or(Either<T1, Any> value1,
                                                                          Either<T2, Any> value2,
                                                                          Either<T3, Any> value3)
        {
            _withValues.Add(EitherTuple.Create(value1, value2, value3));
            return this;
        }
    }
}