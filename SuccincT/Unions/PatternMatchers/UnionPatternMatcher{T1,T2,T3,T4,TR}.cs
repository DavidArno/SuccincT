using System;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincT.Unions.PatternMatchers
{
    /// <summary>
    /// Fluent class created by Union{T1,T2,T3,T4}.Match{TResult}(). Whilst this is a public
    /// class (as the user needs access to Case1-4(), CaseOf(), Else() and Result()), it has an 
    /// internal constructor as it is intended for pattern matching internal usage only.
    /// </summary>
    internal sealed class UnionPatternMatcher<T1, T2, T3, T4, TResult> : IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>,
                                                                         IUnionActionPatternMatcher<T1, T2, T3, T4>,
                                                                         IUnionActionPatternMatcherAfterElse,
                                                                         IUnionFuncPatternMatcherAfterElse<TResult>
    {
        private readonly Union<T1, T2, T3, T4> _union;

        private readonly MatchSelectorsForCases<T1, T2, T3, T4, TResult> _selector =
            MatchSelectorsCreator.CreateSelectors<T1, T2, T3, T4, TResult>();

        internal UnionPatternMatcher(Union<T1, T2, T3, T4> union) => _union = union;

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T1, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Case1() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T1, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T2, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Case2() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T2, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T3, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Case3() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T3, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T4, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Case4() =>
                new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T4, TResult>(
                    _selector.RecordAction,
                    this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T, TResult>
            IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.CaseOf<T>()
        {
            if (typeof(T) == typeof(T1))
            {
                return
                    new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T1, TResult>(
                        _selector.RecordAction,
                        this)
                        as IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T, TResult>;
            }

            if (typeof(T) == typeof(T2))
            {
                return
                    new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T2, TResult>(
                        _selector.RecordAction,
                        this)
                        as IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T, TResult>;
            }

            if (typeof(T) == typeof(T3))
            {
                return
                    new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T3, TResult>(
                        _selector.RecordAction,
                        this)
                        as IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T, TResult>;
            }

            if (typeof(T) == typeof(T4))
            {
                return
                    new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T4, TResult>(
                        _selector.RecordAction,
                        this)
                        as IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>, T, TResult>;
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Else(
            Func<Union<T1, T2, T3, T4>, TResult> elseFunc)
        {
            _selector.RecordElseFunction(elseFunc);
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Else(TResult elseValue)
        {
            _selector.RecordElseFunction(Func((Union < T1, T2, T3, T4 > _) => elseValue));
            return this;
        }

        TResult IUnionFuncPatternMatcher<T1, T2, T3, T4, TResult>.Result() => _selector.ResultNoElse(_union);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result() => _selector.ResultUsingElse(_union);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T1>
            IUnionActionPatternMatcher<T1, T2, T3, T4>.Case1() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T1, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T2>
            IUnionActionPatternMatcher<T1, T2, T3, T4>.Case2() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T2, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T3>
            IUnionActionPatternMatcher<T1, T2, T3, T4>.Case3() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T3, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T4>
            IUnionActionPatternMatcher<T1, T2, T3, T4>.Case4() =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T4, Unit>(_selector.RecordAction,
                                                                                              this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T>
            IUnionActionPatternMatcher<T1, T2, T3, T4>.CaseOf<T>()
        {
            if (typeof(T) == typeof(T1))
            {
                return
                    new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T1, Unit>(
                        _selector.RecordAction,
                        this)
                        as IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T>;
            }

            if (typeof(T) == typeof(T2))
            {
                return
                    new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T2, Unit>(
                        _selector.RecordAction,
                        this)
                        as IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T>;
            }

            if (typeof(T) == typeof(T3))
            {
                return
                    new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T3, Unit>(
                        _selector.RecordAction,
                        this)
                        as IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T>;
            }

            if (typeof(T) == typeof(T4))
            {
                return
                    new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T4, Unit>(
                        _selector.RecordAction,
                        this)
                        as IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2, T3, T4>, T>;
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2, T3, T4>.Else(
            Action<Union<T1, T2, T3, T4>> elseAction)
        {
            _selector.RecordElseAction(elseAction);
            return this;
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2, T3, T4>.IgnoreElse()
        {
            _selector.RecordElseAction(Action((Union<T1, T2, T3, T4> _) => { }));
            return this;
        }

        void IUnionActionPatternMatcher<T1, T2, T3, T4>.Exec() => _selector.ExecNoElse(_union);

        void IUnionActionPatternMatcherAfterElse.Exec() => _selector.ExecUsingElse(_union);
    }
}