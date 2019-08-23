using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.Variant;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class MatchSelectorsForCases<T1, T2, T3, T4, TResult>
    {
        private readonly MatchFunctionSelector<T1, T1, TResult> _case1Selector;
        private readonly MatchFunctionSelector<T2, T2, TResult> _case2Selector;
        private readonly MatchFunctionSelector<T3, T3, TResult> _case3Selector;
        private readonly MatchFunctionSelector<T4, T4, TResult> _case4Selector;
        private Func<Union<T1, T2>, TResult> _u2ElseFunction;
        private Func<Union<T1, T2, T3>, TResult> _u3ElseFunction;
        private Func<Union<T1, T2, T3, T4>, TResult> _u4ElseFunction;

        internal MatchSelectorsForCases()
        {
            _u2ElseFunction = null!;
            _u3ElseFunction = null!;
            _u4ElseFunction = null!;

            _case1Selector =
                new MatchFunctionSelector<T1, T1, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case1 value"));
            _case2Selector =
                new MatchFunctionSelector<T2, T2, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case2 value"));
            _case3Selector = typeof(T3) == typeof(Unit)
                ? null!
                : new MatchFunctionSelector<T3, T3, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case3 value"));
            _case4Selector = typeof(T4) == typeof(Unit)
                ? null!
                : new MatchFunctionSelector<T4, T4, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case4 value"));
        }

        internal void RecordAction<T>(Func<T, IList<T>, bool> withTest,
                                      Func<T, bool> whereTest,
                                      IList<T> withData,
                                      Func<T, TResult> action) => 
            Selector<T>().AddTestAndAction(withTest, withData, whereTest, action);

        internal void RecordAction<T>(Func<T, IList<T>, bool> withTest,
                                      Func<T, bool> whereTest,
                                      IList<T> withData,
                                      Func<T, Unit> action) => 
            Selector<T>().AddTestAndAction(withTest, withData, whereTest, (action as Func<T, TResult>)!);

        internal void RecordElseFunction(Func<Union<T1, T2>, TResult> elseFunction) =>
            _u2ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2>> elseAction) =>
            _u2ElseFunction = (elseAction.ToUnitFunc() as Func<Union<T1, T2>, TResult>)!;

        internal void RecordElseFunction(Func<Union<T1, T2, T3>, TResult> elseFunction) =>
            _u3ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2, T3>> elseAction) =>
            _u3ElseFunction = (elseAction.ToUnitFunc() as Func<Union<T1, T2, T3>, TResult>)!;

        internal void RecordElseFunction(Func<Union<T1, T2, T3, T4>, TResult> elseFunction) =>
            _u4ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2, T3, T4>> elseAction) =>
            _u4ElseFunction = (elseAction.ToUnitFunc() as Func<Union<T1, T2, T3, T4>, TResult>)!;

        internal TResult ResultNoElse(IUnion<T1, T2, T3, T4> union) =>
            DetermineResultUsingDefaultIfRequired(union);

        internal TResult ResultUsingElse(IUnion<T1, T2, T3, T4> union)
        {
            var possibleResult = DetermineResult(union);
            return possibleResult.HasValue ? possibleResult.Value : ElseFunction(union);
        }

        internal void ExecNoElse(IUnion<T1, T2, T3, T4> union) =>
            DetermineResultUsingDefaultIfRequired(union);

        internal void ExecUsingElse(IUnion<T1, T2, T3, T4> union)
        {
            var possibleResult = DetermineResult(union);
            _ = possibleResult.HasValue ? possibleResult.Value : ElseFunction(union);
        }

        private MatchFunctionSelector<T, T, TResult> Selector<T>()
        {
            switch(typeof(T))
            {
                case var t when t == typeof(T1): return (_case1Selector as MatchFunctionSelector<T, T, TResult>)!;
                case var t when t == typeof(T2): return (_case2Selector as MatchFunctionSelector<T, T, TResult>)!;
                case var t when t == typeof(T3): return (_case3Selector as MatchFunctionSelector<T, T, TResult>)!;
                default: return (_case4Selector as MatchFunctionSelector<T, T, TResult>)!;
            }
        }

        private TResult DetermineResultUsingDefaultIfRequired(IUnion<T1, T2, T3, T4> union)
        {
            switch (union.Case)
            {
                case Case1: return _case1Selector.DetermineResultUsingDefaultIfRequired(union.Case1);
                case Case2: return _case2Selector.DetermineResultUsingDefaultIfRequired(union.Case2);
                case Case3: return _case3Selector.DetermineResultUsingDefaultIfRequired(union.Case3);
                default: return _case4Selector.DetermineResultUsingDefaultIfRequired(union.Case4);
            }
        }

        private Option<TResult> DetermineResult(IUnion<T1, T2, T3, T4> union)
            => union.Case switch
            {
                Case1 => _case1Selector.DetermineResult(union.Case1),
                Case2 => _case2Selector.DetermineResult(union.Case2),
                Case3 => _case3Selector.DetermineResult(union.Case3),
                _ => _case4Selector.DetermineResult(union.Case4),
            };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private TResult ElseFunction(IUnion<T1, T2, T3, T4> union)
        {
            switch (union)
            {
                case Union<T1, T2> _: return _u2ElseFunction((union as Union<T1, T2>)!);
                case Union<T1, T2, T3> _: return _u3ElseFunction((union as Union<T1, T2, T3>)!);
                default: return _u4ElseFunction((union as Union<T1, T2, T3, T4>)!);
            }
        }
    }
}