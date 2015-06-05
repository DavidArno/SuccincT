using System;
using SuccincT.PatternMatchers;
using SuccincT.Unions;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Options
{
    public sealed class OptionMatcher<T>
    {
        private readonly Union<T, None> _union;
        private readonly Option<T> _option;

        private readonly UnionCaseActionSelector<T> _case1ActionSelector =
            new UnionCaseActionSelector<T>(
                x => { throw new NoMatchException("No match action defined for Option with value"); });

        private readonly UnionCaseActionSelector<None> _case2ActionSelector =
            new UnionCaseActionSelector<None>(
                x => { throw new NoMatchException("No match action defined for Option with no value"); });

        internal OptionMatcher(Union<T, None> union, Option<T> option)
        {
            _union = union;
            _option = option;
        }

        public UnionPatternCaseHandler<OptionMatcher<T>, T> Some()
        {
            return new UnionPatternCaseHandler<OptionMatcher<T>, T>(RecordAction, this);
        }

        public NoneMatchHandler<T> None()
        {
            return new NoneMatchHandler<T>(RecordAction, this);
        }

        public UnionOfTwoPatternMatcherAfterElse<T, None> Else(Action<Option<T>> elseAction)
        {
            return new UnionOfTwoPatternMatcherAfterElse<T, None>(_union,
                                                                  _case1ActionSelector,
                                                                  _case2ActionSelector,
                                                                  x => elseAction(_option));
        }

        public void Exec()
        {
            if (_union.Case == Variant.Case1)
            {
                _case1ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case1);
            }
            else
            {
                _case2ActionSelector.InvokeMatchedActionUsingDefaultIfRequired(_union.Case2);
            }
        }

        private void RecordAction(Func<T, bool> test, Action<T> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Action action)
        {
            _case2ActionSelector.AddTestAndAction(x => true, x => action());
        }
    }
}