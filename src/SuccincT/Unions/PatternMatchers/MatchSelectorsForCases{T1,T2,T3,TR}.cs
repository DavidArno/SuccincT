using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.Variant;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class MatchSelectorsForCases<T1, T2, T3, TResult>
    {
        private readonly MatchFunctionSelector<T1, T1, TResult> _case1Selector;
        private readonly MatchFunctionSelector<T2, T2, TResult> _case2Selector;
        private readonly MatchFunctionSelector<T3, T3, TResult> _case3Selector;
        private Func<Union<T1, T2, T3>, TResult> _elseFunction;

        internal MatchSelectorsForCases()
        {
            _case1Selector =
                new MatchFunctionSelector<T1, T1, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case1 value"));
            _case2Selector =
                new MatchFunctionSelector<T2, T2, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case2 value"));
            _case3Selector =
                new MatchFunctionSelector<T3, T3, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case3 value"));
        }

        private static TResult NewMethod<T>(T _) 
            => throw new NoMatchException("No match action defined for union with Case1 value");

        internal void RecordAction<T>(Func<T, IList<T>, bool> withTest,
                                      Func<T, bool> whereTest,
                                      IList<T> withData,
                                      Func<T, TResult> action) 
            => Selector<T>().AddTestAndAction(withTest, withData, whereTest, action);

        internal void RecordAction<T>(Func<T, IList<T>, bool> withTest,
                                      Func<T, bool> whereTest,
                                      IList<T> withData,
                                      Func<T, Unit> action) 
            => Selector<T>().AddTestAndAction(withTest, withData, whereTest, action as Func<T, TResult>);

        internal void RecordElseFunction(Func<Union<T1, T2, T3>, TResult> elseFunction) 
            => _elseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2, T3>> elseAction) 
            => _elseFunction = elseAction.ToUnitFunc() as Func<Union<T1, T2, T3>, TResult>;

        internal TResult ResultNoElse(Union<T1, T2, T3> union) 
            => DetermineResultUsingDefaultIfRequired(union);

        internal TResult ResultUsingElse(Union<T1, T2, T3> union)
        {
            var possibleResult = DetermineResult(union);
            return possibleResult.HasValue ? possibleResult.Value : ElseFunction(union);
        }

        internal void ExecNoElse(Union<T1, T2, T3> union) => DetermineResultUsingDefaultIfRequired(union);

        internal void ExecUsingElse(Union<T1, T2, T3> union)
        {
            var possibleResult = DetermineResult(union);
            Ignore(possibleResult.HasValue ? possibleResult.Value : ElseFunction(union));
        }

        private MatchFunctionSelector<T, T, TResult> Selector<T>()
        {
            switch(typeof(T))
            {
                case var t when t == typeof(T1): return _case1Selector as MatchFunctionSelector<T, T, TResult>;
                case var t when t == typeof(T2): return _case2Selector as MatchFunctionSelector<T, T, TResult>;
                default: return _case3Selector as MatchFunctionSelector<T, T, TResult>;
            }
        }

        private TResult DetermineResultUsingDefaultIfRequired(Union<T1, T2, T3> union)
        {
            switch (union.Case)
            {
                case Case1: return _case1Selector.DetermineResultUsingDefaultIfRequired(union.Case1);
                case Case2: return _case2Selector.DetermineResultUsingDefaultIfRequired(union.Case2);
                default: return _case3Selector.DetermineResultUsingDefaultIfRequired(union.Case3);
            }
        }

        private Option<TResult> DetermineResult(Union<T1, T2, T3> union)
        {
            switch (union.Case)
            {
                case Case1: return _case1Selector.DetermineResult(union.Case1);
                case Case2: return _case2Selector.DetermineResult(union.Case2);
                default: return _case3Selector.DetermineResult(union.Case3);
            }
        }

        private TResult ElseFunction(Union<T1, T2, T3> union) => _elseFunction(union);
    }
}