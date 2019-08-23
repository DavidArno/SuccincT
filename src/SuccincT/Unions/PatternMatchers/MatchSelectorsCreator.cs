using SuccincT.Functional;

namespace SuccincT.Unions.PatternMatchers
{
    internal static class MatchSelectorsCreator
    {
        internal static MatchSelectorsForCases<T1, T2, Unit, Unit, TResult> CreateSelectors<T1, T2, TResult>()
            => new MatchSelectorsForCases<T1, T2, Unit, Unit, TResult>();

        internal static MatchSelectorsForCases<T1, T2, T3, Unit, TResult> CreateSelectors<T1, T2, T3, TResult>()
            =>            new MatchSelectorsForCases<T1, T2, T3, Unit, TResult>();

        internal static MatchSelectorsForCases<T1, T2, T3, T4, TResult> CreateSelectors<T1, T2, T3, T4, TResult>()
            => new MatchSelectorsForCases<T1, T2, T3, T4, TResult>();
    }
}