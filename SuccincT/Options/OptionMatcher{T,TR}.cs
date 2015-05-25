using System;
using SuccincT.PatternMatchers;
using SuccincT.Unions;

namespace SuccincT.Options
{
    public sealed class OptionMatcher<T, TReturn>
    {
        private readonly Union<T, None> _union;
        private readonly Option<T> _option;

        private readonly UnionCaseActionSelector<T, TReturn> _case1ActionSelector =
            new UnionCaseActionSelector<T, TReturn>(
                x => { throw new InvalidOperationException("No match action defined for Option with value"); });

        private readonly UnionCaseActionSelector<None, TReturn> _case2ActionSelector =
            new UnionCaseActionSelector<None, TReturn>(
                x => { throw new InvalidOperationException("No match action defined for Option with no value"); });

        internal OptionMatcher(Union<T, None> union, Option<T> option)
        {
            _union = union;
            _option = option;
        }

        public UnionPatternCaseHandler<OptionMatcher<T, TReturn>, T, TReturn> Some()
        {
            return new UnionPatternCaseHandler<OptionMatcher<T, TReturn>, T, TReturn>(RecordAction, this);
        }

        public NoneMatchHandler<T, TReturn> None()
        {
            return new NoneMatchHandler<T, TReturn>(RecordAction, this);
        }

        public UnionPatternMatcherAfterElse<Union<T, None>, T, None, TReturn> Else(Func<Option<T>, TReturn> elseAction)
        {
            return new UnionPatternMatcherAfterElse<Union<T, None>, T, None, TReturn>(_union,
                                                                                      _case1ActionSelector,
                                                                                      _case2ActionSelector,
                                                                                      x => elseAction(_option));
        }

        public TReturn Result()
        {
            return _union.Case == Variant.Case1 
                ? _case1ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case1) 
                : _case2ActionSelector.DetermineResultUsingDefaultIfRequired(_union.Case2);
        }

        private void RecordAction(Func<T, bool> test, Func<T, TReturn> action)
        {
            _case1ActionSelector.AddTestAndAction(test, action);
        }

        private void RecordAction(Func<TReturn> action)
        {
            _case2ActionSelector.AddTestAndAction(x => true, x => action());
        }
    }
}
