using System;
using System.Collections.Generic;

namespace SuccincT.Unions
{
    internal class UnionMatcher<T1, T2, T3, TReturn>
    {
        private sealed class Case<T>
        {
            public Func<T, T, bool> Comparision { get; set; }
            public Func<T, TReturn> MatchAction { get; set; }
        }

        private readonly List<Union<Case<T1>, Case<T2>, Case<T3>>> _cases =
            new List<Union<Case<T1>, Case<T2>, Case<T3>>>();
 
        public void AddAnyMatch(Func<T1, TReturn> func)
        {
            _cases.Add(CreateCase(func));
        }

        public void AddAnyMatch(Func<T2, TReturn> func)
        {
            _cases.Add(CreateCase(func));
        }

        public void AddAnyMatch(Func<T3, TReturn> func)
        {
            _cases.Add(CreateCase(func));
        }

        private static Union<Case<T1>, Case<T2>, Case<T3>> CreateCase(Func<T1, TReturn> func)
        {
            return new Union<Case<T1>, Case<T2>, Case<T3>>(new Case<T1>
            {
                Comparision = (x, y) => true,
                MatchAction = func
            });
        }

        private static Union<Case<T1>, Case<T2>, Case<T3>> CreateCase(Func<T2, TReturn> func)
        {
            return new Union<Case<T1>, Case<T2>, Case<T3>>(new Case<T2>
            {
                Comparision = (x, y) => true,
                MatchAction = func
            });
        }

        private static Union<Case<T1>, Case<T2>, Case<T3>> CreateCase(Func<T3, TReturn> func)
        {
            return new Union<Case<T1>, Case<T2>, Case<T3>>(new Case<T3>
            {
                Comparision = (x, y) => true,
                MatchAction = func
            });
        }
    }
}
