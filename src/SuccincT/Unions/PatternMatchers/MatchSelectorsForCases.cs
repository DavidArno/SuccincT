using System;
using System.Collections.Generic;
using SuccincT.Functional;
using SuccincT.Options;
using SuccincT.PatternMatchers;
using static SuccincT.Unions.Variant;
using static SuccincT.Utilities.NRTSupport;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class MatchSelectorsForCases<T1, T2, T3, T4, TResult>
    {
        private readonly MatchFunctionSelector<T1, T1, TResult> _case1Selector;
        private readonly MatchFunctionSelector<T2, T2, TResult> _case2Selector;
        private readonly MatchFunctionSelector<T3, T3, TResult>? _case3Selector;
        private readonly MatchFunctionSelector<T4, T4, TResult>? _case4Selector;
        private Func<Union<T1, T2>, TResult>? _unionOf2ElseFunction;
        private Func<Union<T1, T2, T3>, TResult>? _unionOf3ElseFunction;
        private Func<Union<T1, T2, T3, T4>, TResult>? _unionOf4ElseFunction;

        internal MatchSelectorsForCases()
        {
            _unionOf2ElseFunction = null;
            _unionOf3ElseFunction = null;
            _unionOf4ElseFunction = null;

            _case1Selector =
                new MatchFunctionSelector<T1, T1, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case1 value"));
            _case2Selector =
                new MatchFunctionSelector<T2, T2, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case2 value"));
            _case3Selector = typeof(T3) == typeof(Unit)
                ? null
                : new MatchFunctionSelector<T3, T3, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case3 value"));
            _case4Selector = typeof(T4) == typeof(Unit)
                ? null
                : new MatchFunctionSelector<T4, T4, TResult>(
                    x => throw new NoMatchException("No match action defined for union with Case4 value"));
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

        internal void RecordElseFunction(Func<Union<T1, T2>, TResult> elseFunction) =>
            _unionOf2ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2>> elseAction) =>
            _unionOf2ElseFunction = elseAction.ToUnitFunc() as Func<Union<T1, T2>, TResult>;

        internal void RecordElseFunction(Func<Union<T1, T2, T3>, TResult> elseFunction) =>
            _unionOf3ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2, T3>> elseAction) =>
            _unionOf3ElseFunction = elseAction.ToUnitFunc() as Func<Union<T1, T2, T3>, TResult>;

        internal void RecordElseFunction(Func<Union<T1, T2, T3, T4>, TResult> elseFunction) =>
            _unionOf4ElseFunction = elseFunction;

        internal void RecordElseAction(Action<Union<T1, T2, T3, T4>> elseAction) =>
            _unionOf4ElseFunction = elseAction.ToUnitFunc() as Func<Union<T1, T2, T3, T4>, TResult>;

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
            => typeof(T) switch
            {
                var _ when TypesAreSame<T, T1>() => (_case1Selector as MatchFunctionSelector<T, T, TResult>)!,
                var _ when TypesAreSame<T, T2>() => (_case2Selector as MatchFunctionSelector<T, T, TResult>)!,
                var _ when TypesAreSame<T, T3>() => (_case3Selector as MatchFunctionSelector<T, T, TResult>)!,
                _ => (_case4Selector as MatchFunctionSelector<T, T, TResult>)!
            };

        private TResult DetermineResultUsingDefaultIfRequired(IUnion<T1, T2, T3, T4> union)
        {
            return union.Case switch
            {
                Case1 => _case1Selector.DetermineResultUsingDefaultIfRequired(union.Case1),
                Case2 => _case2Selector.DetermineResultUsingDefaultIfRequired(union.Case2),
                Case3 => _case3Selector!.DetermineResultUsingDefaultIfRequired(union.Case3),
                _ => _case4Selector!.DetermineResultUsingDefaultIfRequired(union.Case4),
            };
        }

        private Option<TResult> DetermineResult(IUnion<T1, T2, T3, T4> union)
            => union.Case switch
            {
                Case1 => _case1Selector.DetermineResult(union.Case1),
                Case2 => _case2Selector.DetermineResult(union.Case2),
                Case3 => _case3Selector!.DetermineResult(union.Case3),
                _ => _case4Selector!.DetermineResult(union.Case4),
            };

        private TResult ElseFunction(IUnion<T1, T2, T3, T4> union) 
            => union switch
            {
                Union<T1, T2> u2 => _unionOf2ElseFunction!(u2),
                Union<T1, T2, T3> u3 => _unionOf3ElseFunction!(u3),
                Union<T1, T2, T3, T4> u4 => _unionOf4ElseFunction!(u4),
                _ => throw new InvalidOperationException()
            };
    }
}