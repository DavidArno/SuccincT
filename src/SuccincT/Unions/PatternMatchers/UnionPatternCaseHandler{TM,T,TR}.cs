using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Functional;
using SuccincT.PatternMatchers;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class UnionPatternCaseHandler<TMatcher, T, TResult> :
        IUnionFuncPatternCaseHandler<TMatcher, T, TResult>,
        IUnionActionPatternCaseHandler<TMatcher, T>,
        IFuncWithHandler<TMatcher, T, TResult>,
        IFuncWhereHandler<TMatcher, T, TResult>,
        IActionWithHandler<TMatcher, T>,
        IActionWhereHandler<TMatcher, T>
    {
        private readonly Action<Func<T, IList<T>, bool>, Func<T, bool>, IList<T>, Func<T, TResult>> _recorder;
        private readonly TMatcher _matcher;
        private List<T> _ofValues;
        private Func<T, bool> _whereExpression;

        internal UnionPatternCaseHandler(
            Action<Func<T, IList<T>, bool>, Func<T, bool>, IList<T>, Func<T, TResult>> recorder,
            TMatcher matcher)
        {
            _recorder = recorder;
            _matcher = matcher;
        }

        IFuncWithHandler<TMatcher, T, TResult> IUnionFuncPatternCaseHandler<TMatcher, T, TResult>.Of(T value)
        {
            _ofValues = new List<T> {value};
            return this;
        }

        IFuncWhereHandler<TMatcher, T, TResult> IUnionFuncPatternCaseHandler<TMatcher, T, TResult>.Where(
            Func<T, bool> expression)
        {
            _whereExpression = expression;
            return this;
        }

        TMatcher IUnionFuncPatternCaseHandler<TMatcher, T, TResult>.Do(Func<T, TResult> action)
        {
            _recorder((x, y) => true, null, null, action);
            return _matcher;
        }

        TMatcher IUnionFuncPatternCaseHandler<TMatcher, T, TResult>.Do(TResult value)
        {
            _recorder((x, y) => true, null, null, x => value);
            return _matcher;
        }

        IActionWithHandler<TMatcher, T> IUnionActionPatternCaseHandler<TMatcher, T>.Of(T value)
        {
            _ofValues = new List<T> {value};
            return this;
        }

        IActionWhereHandler<TMatcher, T> IUnionActionPatternCaseHandler<TMatcher, T>.Where(Func<T, bool> expression)
        {
            _whereExpression = expression;
            return this;
        }

        TMatcher IUnionActionPatternCaseHandler<TMatcher, T>.Do(Action<T> action)
        {
            _recorder((x, y) => true, null, null, action.ToUnitFunc() as Func<T, TResult>);
            return _matcher;
        }

        IFuncWithHandler<TMatcher, T, TResult> IFuncWithHandler<TMatcher, T, TResult>.Or(T value)
        {
            _ofValues.Add(value);
            return this;
        }

        TMatcher IFuncWithHandler<TMatcher, T, TResult>.Do(Func<T, TResult> action)
        {
            _recorder((x, y) => y.Any(value => EqualityComparer<T>.Default.Equals(x, value)), null, _ofValues, action);
            return _matcher;
        }

        TMatcher IFuncWithHandler<TMatcher, T, TResult>.Do(TResult value)
        {
            _recorder((x, y) => y.Any(v => EqualityComparer<T>.Default.Equals(x, v)), null, _ofValues, x => value);
            return _matcher;
        }

        TMatcher IFuncWhereHandler<TMatcher, T, TResult>.Do(Func<T, TResult> action)
        {
            _recorder(null, _whereExpression, null, action);
            return _matcher;
        }

        TMatcher IFuncWhereHandler<TMatcher, T, TResult>.Do(TResult value)
        {
            _recorder(null, _whereExpression, null, x => value);
            return _matcher;
        }

        IActionWithHandler<TMatcher, T> IActionWithHandler<TMatcher, T>.Or(T value)
        {
            _ofValues.Add(value);
            return this;
        }

        TMatcher IActionWithHandler<TMatcher, T>.Do(Action<T> action)
        {
            _recorder((x, y) => y.Any(value => EqualityComparer<T>.Default.Equals(x, value)),
                      null,
                      _ofValues,
                      action.ToUnitFunc() as Func<T, TResult>);
            return _matcher;
        }

        TMatcher IActionWhereHandler<TMatcher, T>.Do(Action<T> action)
        {
            _recorder(null, _whereExpression, null, action.ToUnitFunc() as Func<T, TResult>);
            return _matcher;
        }
    }
}