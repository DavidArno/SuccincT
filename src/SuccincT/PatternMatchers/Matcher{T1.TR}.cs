using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    internal sealed class Matcher<T1, TResult> : IMatcher<T1>,
                                                 IActionMatcher<T1>,
                                                 IFuncMatcher<T1, TResult>,
                                                 IActionWithHandler<IActionMatcher<T1>, T1>,
                                                 IActionWhereHandler<IActionMatcher<T1>, T1>,
                                                 IActionMatcherAfterElse,
                                                 IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult>,
                                                 IFuncWhereHandler<IFuncMatcher<T1, TResult>, T1, TResult>,
                                                 IFuncMatcherAfterElse<TResult>
    {
        private readonly MatchFunctionSelector<T1, T1, TResult> _functionSelector;
        private readonly T1 _item;
        private List<T1> _withValues;
        private Func<T1, bool> _whereExpression;
        private Func<T1, TResult> _elseFunction;

        internal Matcher(T1 item)
        {
            _item = item;
            _functionSelector = new MatchFunctionSelector<T1, T1, TResult>(
                x => { throw new NoMatchException($"No match action exists for value of {_item}"); });
        }

        IFuncMatcher<T1, TR> IMatcher<T1>.To<TR>() => new Matcher<T1, TR>(_item);

        IActionWithHandler<IActionMatcher<T1>, T1> IMatcher<T1>.With(T1 value)
        {
            _withValues = new List<T1> {value};
            return this;
        }

        IActionWithHandler<IActionMatcher<T1>, T1> IActionMatcher<T1>.With(T1 value)
        {
            _withValues = new List<T1> { value };
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult> IFuncMatcher<T1, TResult>.With(T1 value)
        {
            _withValues = new List<T1> { value };
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1>, T1> IMatcher<T1>.Where(Func<T1, bool> expression)
        {
            _whereExpression = expression;
            return this;
        }

        IActionWhereHandler<IActionMatcher<T1>, T1> IActionMatcher<T1>.Where(Func<T1, bool> expression)
        {
            _whereExpression = expression;
            return this;
        }

        IFuncWhereHandler<IFuncMatcher<T1, TResult>, T1, TResult> IFuncMatcher<T1, TResult>.Where(
            Func<T1, bool> expression)
        {
            _whereExpression = expression;
            return this;
        }

        IActionWithHandler<IActionMatcher<T1>, T1> IActionWithHandler<IActionMatcher<T1>, T1>.Or(T1 value)
        {
            _withValues.Add(value);
            return this;
        }

        IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult>
            IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult>.Or(T1 value)
        {
            _withValues.Add(value);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1>.Else(Action<T1> action)
        {
            _elseFunction = ActionToFunc(action);
            return this;
        }

        IActionMatcherAfterElse IActionMatcher<T1>.IgnoreElse()
        {
            _elseFunction = x => default(TResult);
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, TResult>.Else(Func<T1, TResult> function)
        {
            _elseFunction = function;
            return this;
        }

        IFuncMatcherAfterElse<TResult> IFuncMatcher<T1, TResult>.Else(TResult value)
        {
            _elseFunction = x => value;
            return this;
        }

        IActionMatcher<T1> IActionWithHandler<IActionMatcher<T1>, T1>.Do(Action<T1> action)
        {
            RecordFunction(_withValues, ActionToFunc(action));
            return this;
        }

        IActionMatcher<T1> IActionWhereHandler<IActionMatcher<T1>, T1>.Do(Action<T1> action)
        {
            var expression = _whereExpression;
            RecordFunction(expression, ActionToFunc(action));
            return this;
        }

        IFuncMatcher<T1, TResult> IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult>.Do(
            Func<T1, TResult> action)
        {
            var values = _withValues;
            RecordFunction(x => values.Any(y => EqualityComparer<T1>.Default.Equals(x, y)), action);
            return this;
        }

        IFuncMatcher<T1, TResult> IFuncWithHandler<IFuncMatcher<T1, TResult>, T1, TResult>.Do(TResult value)
        {
            RecordFunction(_withValues, v => value);
            return this;
        }

        IFuncMatcher<T1, TResult> IFuncWhereHandler<IFuncMatcher<T1, TResult>, T1, TResult>.Do(
            Func<T1, TResult> action)
        {
            RecordFunction(_whereExpression, action);
            return this;
        }

        IFuncMatcher<T1, TResult> IFuncWhereHandler<IFuncMatcher<T1, TResult>, T1, TResult>.Do(TResult value)
        {
            RecordFunction(_whereExpression, x => value);
            return this;
        }

        void IActionMatcher<T1>.Exec() => _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        void IActionMatcherAfterElse.Exec()
        {
            if (!_functionSelector.DetermineResult(_item).HasValue)
            {
                _elseFunction(_item);
            }
        }

        TResult IFuncMatcher<T1, TResult>.Result() =>
            _functionSelector.DetermineResultUsingDefaultIfRequired(_item);

        TResult IFuncMatcherAfterElse<TResult>.Result()
        {
            var possibleResult = _functionSelector.DetermineResult(_item);
            return possibleResult.HasValue ? possibleResult.Value : _elseFunction(_item);
        }

        private void RecordFunction(IList<T1> values, Func<T1, TResult> function) =>
            _functionSelector.AddTestAndAction((x, y) => y.Any(v => EqualityComparer<T1>.Default.Equals(x, v)),
                                               values,
                                               null,
                                               function);

        private void RecordFunction(Func<T1, bool> test, Func<T1, TResult> function) =>
            _functionSelector.AddTestAndAction(null, null, test, function);

        private static Func<T1, TResult> ActionToFunc(Action<T1> action) =>
            x =>
            {
                action(x);
                return default(TResult);
            };
    }
}