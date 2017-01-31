using System;
using System.Collections.Generic;
using System.Linq;
using static SuccincT.Functional.Unit;

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
        private readonly MatchFunctionSelector<Tuple<T1, T2, T3, T4>, Tuple<T1, T2, T3, T4>, TResult> _functionSelector;
        private readonly Tuple<T1, T2, T3, T4> _item;
        private IList<Tuple<T1, T2, T3, T4>> _withValues;
        private Func<Tuple<T1, T2, T3, T4>, bool> _whereExpression;
        private Func<Tuple<T1, T2, T3, T4>, TResult> _elseFunction;

        internal Matcher(Tuple<T1, T2, T3, T4> item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<Tuple<T1, T2, T3, T4>, Tuple<T1, T2, T3, T4>, TResult>(x =>
            {
                throw new NoMatchException(
                    $"No match action exists for value of ({_item.Item1}, {_item.Item2}, {_item.Item3}, {_item.Item4}");
            });
        }

        IFuncMatcher<T1, T2, T3, T4, TR> IMatcher<T1, T2, T3, T4>.To<TR>() => new Matcher<T1, T2, T3, T4, TR>(_item);

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IMatcher<T1, T2, T3, T4>.With(
            T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _withValues = new List<Tuple<T1, T2, T3, T4>> { Tuple.Create(value1, value2, value3, value4) };
            return this;
        }

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4> IActionMatcher<T1, T2, T3, T4>.With(
            T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _withValues = new List<Tuple<T1, T2, T3, T4>> { Tuple.Create(value1, value2, value3, value4) };
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>
            IFuncMatcher<T1, T2, T3, T4, TResult>.With(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _withValues = new List<Tuple<T1, T2, T3, T4>> { Tuple.Create(value1, value2, value3, value4) };
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

        IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult> IFuncMatcher<T1, T2, T3, T4, TResult>.
            Where(Func<T1, T2, T3, T4, bool> expression)
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
            _elseFunction = x => default(TResult);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, T3, T4, TResult>.Else(Func<T1, T2, T3, T4, TResult> function)
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
                T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _withValues.Add(Tuple.Create(value1, value2, value3, value4));
            return this;
        }

        IActionMatcher<T1, T2, T3, T4> IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Do(
            Action<T1, T2, T3, T4> action)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2, T3, T4>>.Default.Equals(x, v)),
                           _withValues,
                           ActionToFunc(action));
            return this;
        }

        IActionMatcher<T1, T2, T3, T4> IActionWhereHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Do(
            Action<T1, T2, T3, T4> action)
        {
            RecordFunction(_whereExpression, ActionToFunc(action));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
            Func<T1, T2, T3, T4, TResult> function)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2, T3, T4>>.Default.Equals(x, v)),
                           _withValues,
                           x => function(x.Item1, x.Item2, x.Item3, x.Item4));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
            TResult value)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2, T3, T4>>.Default.Equals(x, v)),
                           _withValues,
                           x => value);
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
            Func<T1, T2, T3, T4, TResult> function)
        {
            RecordFunction(_whereExpression, x => function(x.Item1, x.Item2, x.Item3, x.Item4));
            return this;
        }

        IFuncMatcher<T1, T2, T3, T4, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, T3, T4, TResult>, T1, T2, T3, T4, TResult>.Do(
            TResult value)
        {
            RecordFunction(_whereExpression, x => value);
            return this;
        }

        void IActionMatcher<T1, T2, T3, T4>.Exec() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        void IActionMatcherAfterElse.Exec()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            Ignore(possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item));
        }

        TResult IFuncMatcher<T1, T2, T3, T4, TResult>.Result() =>
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        TResult IFuncMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            return possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item);
        }

        private void RecordFunction(Func<Tuple<T1, T2, T3, T4>, IList<Tuple<T1, T2, T3, T4>>, bool> test,
                                    IList<Tuple<T1, T2, T3, T4>> values,
                                    Func<Tuple<T1, T2, T3, T4>, TResult> function)
        {
            _functionSelector.AddTestAndAction(test, values, null, function);
        }

        private void RecordFunction(Func<Tuple<T1, T2, T3, T4>, bool> test, Func<Tuple<T1, T2, T3, T4>, TResult> function) =>
            _functionSelector.AddTestAndAction(null, null, test, function);

        private static Func<Tuple<T1, T2, T3, T4>, TResult> ActionToFunc(Action<T1, T2, T3, T4> action) =>
            x =>
            {
                action(x.Item1, x.Item2, x.Item3, x.Item4);
                return default(TResult);
            };

        IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>
            IActionWithHandler<IActionMatcher<T1, T2, T3, T4>, T1, T2, T3, T4>.Or(
                T1 value1, T2 value2, T3 value3, T4 value4)
        {
            _withValues.Add(Tuple.Create(value1, value2, value3, value4));
            return this;
        }
    }
}