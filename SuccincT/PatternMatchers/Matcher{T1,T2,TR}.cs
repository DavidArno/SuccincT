using System;
using System.Collections.Generic;
using System.Linq;
using static SuccincT.Functional.Unit;

namespace SuccincT.PatternMatchers
{
    internal sealed class Matcher<T1, T2, TResult> :
        IMatcher<T1, T2>,
        IActionMatcher<T1, T2>,
        IFuncMatcher<T1, T2, TResult>,
        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2>,
        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2>,
        IActionMatcherAfterElse,
        IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>,
        IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>,
        IFuncMatcherAfterElse<TResult>
    {
        private readonly MatchFunctionSelector<Tuple<T1, T2>, TResult> _functionSelector;
        private readonly Tuple<T1, T2> _item;
        private IList<Tuple<T1, T2>> _withValues;
        private Func<Tuple<T1, T2>, bool> _whereExpression;
        private Func<Tuple<T1, T2>, TResult> _elseFunction;

        internal Matcher(Tuple<T1, T2> item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<Tuple<T1, T2>, TResult>(x =>
            {
                throw new NoMatchException($"No match action exists for value of ({_item.Item1}, {_item.Item2}");
            });
        }

        IFuncMatcher<T1, T2, TR> IMatcher<T1, T2>.To<TR>() => new Matcher<T1, T2, TR>(_item);

        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> IActionMatcher<T1, T2>.With(T1 value1, T2 value2)
        {
            _withValues = new List<Tuple<T1, T2>> {Tuple.Create(value1, value2)};
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> IFuncMatcher<T1, T2, TResult>.With(T1 value1,
                                                                                                            T2 value2)
        {
            _withValues = new List<Tuple<T1, T2>> {Tuple.Create(value1, value2)};
            return this;
        }

        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> IMatcher<T1, T2>.With(T1 value1, T2 value2)
        {
            _withValues = new List<Tuple<T1, T2>> {Tuple.Create(value1, value2)};
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2> IMatcher<T1, T2>.Where(Func<T1, T2, bool> function)
        {
            _whereExpression = x => function(x.Item1, x.Item2);
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2> IActionMatcher<T1, T2>.Where(Func<T1, T2, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2);
            return this;
        }

        IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult> IFuncMatcher<T1, T2, TResult>.Where(
            Func<T1, T2, bool> expression)
        {
            _whereExpression = x => expression(x.Item1, x.Item2);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2>.Else(Action<T1, T2> action)
        {
            _elseFunction = ActionToFunc(action);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1, T2>.IgnoreElse()
        {
            _elseFunction = x => default(TResult);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, TResult>.Else(Func<T1, T2, TResult> function)
        {
            _elseFunction = x => function(x.Item1, x.Item2);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, T2, TResult>.Else(TResult result)
        {
            _elseFunction = x => result;
            return this;
        }

        IActionWithHandler<IActionMatcher<T1, T2>, T1, T2> IActionWithHandler<IActionMatcher<T1, T2>, T1, T2>.Or(
            T1 value1,
            T2 value2)
        {
            _withValues.Add(Tuple.Create(value1, value2));
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>
            IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>.Or(T1 value1, T2 value2)
        {
            _withValues.Add(Tuple.Create(value1, value2));
            return this;
        }

        IActionMatcher<T1, T2> IActionWithHandler<IActionMatcher<T1, T2>, T1, T2>.Do(Action<T1, T2> action)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2>>.Default.Equals(x, v)),
                           _withValues,
                           ActionToFunc(action));
            return this;
        }

        IActionMatcher<T1, T2> IActionWhereHandler<IActionMatcher<T1, T2>, T1, T2>.Do(Action<T1, T2> action)
        {
            RecordFunction(_whereExpression, ActionToFunc(action));
            return this;
        }

        IFuncMatcher<T1, T2, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>.Do(
            Func<T1, T2, TResult> function)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2>>.Default.Equals(x, v)),
                           _withValues,
                           x => function(x.Item1, x.Item2));
            return this;
        }

        IFuncMatcher<T1, T2, TResult> IFuncWithHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>.Do(
            TResult value)
        {
            RecordFunction((x, y) => y.Any(v => EqualityComparer<Tuple<T1, T2>>.Default.Equals(x, v)),
                           _withValues,
                           x => value);
            return this;
        }

        IFuncMatcher<T1, T2, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>.Do(
            Func<T1, T2, TResult> function)
        {
            RecordFunction(_whereExpression, x => function(x.Item1, x.Item2));
            return this;
        }

        IFuncMatcher<T1, T2, TResult> IFuncWhereHandler<IFuncMatcher<T1, T2, TResult>, T1, T2, TResult>.Do(
            TResult value)
        {
            RecordFunction(_whereExpression, x => value);
            return this;
        }

        void IActionMatcher<T1, T2>.Exec() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        void IActionMatcherAfterElse.Exec()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            Ignore(possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item));
        }

        TResult IFuncMatcher<T1, T2, TResult>.Result() =>
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        TResult IFuncMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            return possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item);
        }

        private void RecordFunction(Func<Tuple<T1, T2>, IList<Tuple<T1, T2>>, bool> test,
                                    IList<Tuple<T1, T2>> values,
                                    Func<Tuple<T1, T2>, TResult> function)
        {
            _functionSelector.AddTestAndAction(test, values, null, function);
        }

        private void RecordFunction(Func<Tuple<T1, T2>, bool> test, Func<Tuple<T1, T2>, TResult> function) =>
            _functionSelector.AddTestAndAction(null, null, test, function);

        private static Func<Tuple<T1, T2>, TResult> ActionToFunc(Action<T1, T2> action) =>
            x =>
            {
                action(x.Item1, x.Item2);
                return default(TResult);
            };
    }
}