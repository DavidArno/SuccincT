using System.Collections.Generic;

namespace SuccincT.Unions
{
    internal static class FluentMatchResolver
    {
        public static Union<T3, None> Resolve<T1, T2, T3>(Union<T1, T2> union, IEnumerable<MatcherFunctions<T1, T2, T3>> matchCases)
        {
            foreach (var matchCase in matchCases)
            {
                if (Case1FunctionPassesTest(union, matchCase) || Case2FunctionPassesTest(union, matchCase))
                {
                    return new Union<T3, None>(matchCase.Result);
                }
            }

            return new Union<T3, None>(None.Value);
        }

        private static bool Case1FunctionPassesTest<T1, T2, T3>(Union<T1, T2> union, MatcherFunctions<T1, T2, T3> matchCase)
        {
            return union.Case == Variant.Case1 &&
                   matchCase.Functions.Case == Variant.Case1 &&
                   matchCase.Functions.Case1(union.Case1);
        }

        private static bool Case2FunctionPassesTest<T1, T2, T3>(Union<T1, T2> union, MatcherFunctions<T1, T2, T3> matchCase)
        {
            return union.Case == Variant.Case2 &&
                   matchCase.Functions.Case == Variant.Case2 &&
                   matchCase.Functions.Case2(union.Case2);
        }
    }
}
