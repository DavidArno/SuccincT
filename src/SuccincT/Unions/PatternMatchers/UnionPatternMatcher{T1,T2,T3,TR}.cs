using System;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;
using static SuccincT.Utilities.NrtSupport;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class UnionFuncPatternMatcher<T1, T2, T3, TResult> : IUnionFuncPatternMatcher<T1, T2, T3, TResult>,
                                                                         IUnionActionPatternMatcher<T1, T2, T3>,
                                                                         IUnionActionPatternMatcherAfterElse,
                                                                         IUnionFuncPatternMatcherAfterElse<TResult>
    {
        private readonly Union<T1, T2, T3> _union;

        private readonly MatchSelectorsForCases<T1, T2, T3, Unit, TResult> _selector =
            MatchSelectorsCreator.CreateSelectors<T1, T2, T3, TResult>();

        internal UnionFuncPatternMatcher(Union<T1, T2, T3> union) => _union = union;

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T1, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Case1() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T1, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T2, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Case2() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T2, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T3, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Case3() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T3, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, TResult>.CaseOf<T>()
        {
            if (TypesAreSame<T, T1>() || TypesAreSame<T, T2>() || TypesAreSame<T, T3>())
            {
                return new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, TResult>, T, TResult>(
                    _selector.RecordAction,
                    this);
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Else(
            Func<Union<T1, T2, T3>, TResult> elseFunc)
        {
            _selector.RecordElseFunction(elseFunc);
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Else(TResult elseValue)
        {
            _selector.RecordElseFunction(Func((Union<T1, T2, T3> _) => elseValue));
            return this;
        }

        TResult IUnionFuncPatternMatcher<T1, T2, T3, TResult>.Result() => _selector.ResultNoElse(_union);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result() => _selector.ResultUsingElse(_union);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T1>
            IUnionActionPatternMatcher<T1, T2, T3>.Case1() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T1, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T2>
            IUnionActionPatternMatcher<T1, T2, T3>.Case2() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T2, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T3>
            IUnionActionPatternMatcher<T1, T2, T3>.Case3() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T3, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T>
            IUnionActionPatternMatcher<T1, T2, T3>.CaseOf<T>()
        {
            if (TypesAreSame<T, T1>() || TypesAreSame<T, T2>() || TypesAreSame<T, T3>())
            {
                return new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3>, T, Unit>(
                    _selector.RecordAction,
                    this);
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2, T3>.Else(
            Action<Union<T1, T2, T3>> elseAction)
        {
            _selector.RecordElseAction(elseAction);
            return this;
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2, T3>.IgnoreElse()
        {
            _selector.RecordElseAction(Action((Union<T1, T2, T3> _) => {}));
            return this;
        }

        void IUnionActionPatternMatcher<T1, T2, T3>.Exec() => _selector.ExecNoElse(_union);

        void IUnionActionPatternMatcherAfterElse.Exec() => _selector.ExecUsingElse(_union);
    }
}