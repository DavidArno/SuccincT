using System;

namespace SuccincT.Unions
{
    internal class MatcherFunctions<T1, T2, T3>
    {
        public Union<Func<T1, bool>, Func<T2, bool>> Functions { get; set; }
        public T3 Result { get; set; }
    }
}
