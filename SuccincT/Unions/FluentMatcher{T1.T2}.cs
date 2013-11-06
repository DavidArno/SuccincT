using System;
using System.Collections.Generic;
using SuccincT.Exceptions;

namespace SuccincT.Unions
{
    internal class FluentMatcher<T1, T2, TResult> : IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult>
    {
        private readonly Union<T1, T2> _union;
        private readonly List<MatcherFunctions<T1, T2, TResult>> _matchCases;

        public FluentMatcher(Union<T1, T2> union)
        {
            _union = union;
            _matchCases = new List<MatcherFunctions<T1, T2, TResult>>();
        }

        public IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult> Case(Func<T1, bool> test, TResult resultIfTestPasses)
        {
            _matchCases.Add(MatcherFunctions.Create(new Union<Func<T1, bool>, Func<T2, bool>>(test), resultIfTestPasses));
            return this;
        }

        public IFluentMatcherWithoutElseClauseDefined<T1, T2, TResult> Case(Func<T2, bool> test, TResult resultIfTestPasses)
        {
            _matchCases.Add(MatcherFunctions.Create(new Union<Func<T1, bool>, Func<T2, bool>>(test), resultIfTestPasses));
            return this;
        }

        public IFluentMatcherWithElseDefined<T1, T2, TResult> Else(TResult elseResult)
        {
            return new FluentMatcherWithElse<T1, T2, TResult>(_union, _matchCases, elseResult);
        }

        public TResult Result()
        {
            var resolverResult = FluentMatchResolver.Resolve(_union, _matchCases);
            return resolverResult.MatchAndReturn(result => result, none => { throw new NoMatchFoundException(); });
        }
    }
}
