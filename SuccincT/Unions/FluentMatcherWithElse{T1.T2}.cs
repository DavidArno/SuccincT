using System;
using System.Collections.Generic;

namespace SuccincT.Unions
{
    internal class FluentMatcherWithElse<T1, T2, TResult> : IFluentMatcherWithElseDefined<T1, T2, TResult>
    {
        private readonly Union<T1, T2> _union;
        private readonly List<MatcherFunctions<T1, T2, TResult>> _matchCases;
        private readonly TResult _elseValue;

        internal FluentMatcherWithElse(Union<T1, T2> union,
                                       List<MatcherFunctions<T1, T2, TResult>> matchCases,
                                       TResult elseValue)
        {
            _union = union;
            _matchCases = matchCases;
            _elseValue = elseValue;
        }

        public IFluentMatcherWithElseDefined<T1, T2, TResult> Case(Func<T1, bool> test, TResult resultIfTestPasses)
        {
            _matchCases.Add(MatcherFunctions.Create(new Union<Func<T1, bool>, Func<T2, bool>>(test), resultIfTestPasses));
            return this;
        }

        public IFluentMatcherWithElseDefined<T1, T2, TResult> Case(Func<T2, bool> test, TResult resultIfTestPasses)
        {
            _matchCases.Add(MatcherFunctions.Create(new Union<Func<T1, bool>, Func<T2, bool>>(test), resultIfTestPasses));
            return this;
        }

        public TResult Result()
        {
            var resolverResult = FluentMatchResolver.Resolve(_union, _matchCases);
            return resolverResult.MatchAndReturn(result => result, none => _elseValue);
        }
    }
}
