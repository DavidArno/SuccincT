﻿using System;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;
using static SuccincT.Utilities.NrtSupport;

namespace SuccincT.Unions.PatternMatchers
{
    internal sealed class UnionPatternMatcher<T1, T2, TResult> : IUnionFuncPatternMatcher<T1, T2, TResult>,
                                                                 IUnionActionPatternMatcher<T1, T2>,
                                                                 IUnionFuncPatternMatcherAfterElse<TResult>,
                                                                 IUnionActionPatternMatcherAfterElse
    {
        private readonly Union<T1, T2> _union;

        private readonly MatchSelectorsForCases<T1, T2, Unit, Unit, TResult> _selector =
            MatchSelectorsCreator.CreateSelectors<T1, T2, TResult>();

        internal UnionPatternMatcher(Union<T1, T2> union) => _union = union;

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T1, TResult>
            IUnionFuncPatternMatcher<T1, T2, TResult>.Case1() =>
            new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T1, TResult>(_selector.RecordAction,
                                                                                                this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T2, TResult>
            IUnionFuncPatternMatcher<T1, T2, TResult>.Case2() =>
            new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T2, TResult>(_selector.RecordAction,
                                                                                                this);

        IUnionFuncPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T, TResult>
            IUnionFuncPatternMatcher<T1, T2, TResult>.CaseOf<T>()
        {
            if (TypesAreSame<T, T1>() || TypesAreSame<T, T2>())
            {
                return new UnionPatternCaseHandler<IUnionFuncPatternMatcher<T1, T2, TResult>, T, TResult>(
                    _selector.RecordAction,
                    this);
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, TResult>.Else(
            Func<Union<T1, T2>, TResult> elseFunc)
        {
            _selector.RecordElseFunction(elseFunc);
            return this;
        }

        IUnionFuncPatternMatcherAfterElse<TResult> IUnionFuncPatternMatcher<T1, T2, TResult>.Else(TResult elseValue)
        {
            _selector.RecordElseFunction(Func((Union<T1, T2> _) => elseValue));
            return this;
        }

        TResult IUnionFuncPatternMatcher<T1, T2, TResult>.Result() => _selector.ResultNoElse(_union);

        TResult IUnionFuncPatternMatcherAfterElse<TResult>.Result() => _selector.ResultUsingElse(_union);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T1> IUnionActionPatternMatcher<T1, T2>.Case1()
            =>
                new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T1, Unit>(_selector.RecordAction, this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T2> IUnionActionPatternMatcher<T1, T2>.Case2() => 
            new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T2, Unit>(_selector.RecordAction, this);

        IUnionActionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T>
            IUnionActionPatternMatcher<T1, T2>.CaseOf<T>()
        {
            if (TypesAreSame<T, T1>() || TypesAreSame<T, T2>())
            {
                return new UnionPatternCaseHandler<IUnionActionPatternMatcher<T1, T2>, T, Unit>(
                    _selector.RecordAction,
                    this);
            }

            throw new InvalidCaseOfTypeException(typeof(T));
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2>.Else(Action<Union<T1, T2>> elseAction)
        {
            _selector.RecordElseAction(elseAction);
            return this;
        }

        IUnionActionPatternMatcherAfterElse IUnionActionPatternMatcher<T1, T2>.IgnoreElse()
        {
            _selector.RecordElseAction(Action((Union<T1, T2> _) => {}));
            return this;
        }

        void IUnionActionPatternMatcher<T1, T2>.Exec() => _selector.ExecNoElse(_union);

        void IUnionActionPatternMatcherAfterElse.Exec() => _selector.ExecUsingElse(_union);
    }
}