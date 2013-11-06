using System;

namespace SuccincT.Unions
{
    public static class MatchAndReturnExtensions
    {
        public static TResult MatchAndReturn<T1, T2, TResult>(this Union<T1, T2> union, 
                                                              Func<T1, TResult> case1Func,
                                                              Func<T2, TResult> case2Func)
        {
            switch (union.Case)
            {
                case Variant.Case1: { return case1Func(union.Case1); }
                default: { return case2Func(union.Case2); }
            }
        }

        public static TResult MatchAndReturn<T1, T2, T3, TResult>(this Union<T1, T2, T3> union,
                                                                  Func<T1, TResult> case1Func,
                                                                  Func<T2, TResult> case2Func,
                                                                  Func<T3, TResult> case3Func)
        {
            switch (union.Case)
            {
                case Variant.Case1: { return case1Func(union.Case1); }
                case Variant.Case2: { return case2Func(union.Case2); }
                default: { return case3Func(union.Case3); }
            }
        }

        public static TResult MatchAndReturn<T1, T2, T3, T4, TResult>(this Union<T1, T2, T3, T4> union,
                                                                      Func<T1, TResult> case1Func,
                                                                      Func<T2, TResult> case2Func,
                                                                      Func<T3, TResult> case3Func,
                                                                      Func<T4, TResult> case4Func)
        {
            switch (union.Case)
            {
                case Variant.Case1: { return case1Func(union.Case1); }
                case Variant.Case2: { return case2Func(union.Case2); }
                case Variant.Case3: { return case3Func(union.Case3); }
                default: { return case4Func(union.Case4); }
            }
        }
    }
}
