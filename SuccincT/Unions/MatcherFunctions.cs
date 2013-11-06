using System;

namespace SuccincT.Unions
{
    internal static class MatcherFunctions
    {
        public static MatcherFunctions<T1, T2, T3> Create<T1, T2, T3>(Union<Func<T1, bool>, Func<T2, bool>> functions, T3 result)
        {
            return new MatcherFunctions<T1, T2, T3> { Functions = functions, Result = result };
        }
    }
}
