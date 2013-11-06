using System;

namespace SuccincT.Unions
{
    public static class MatchAndActionExtensions
    {
        public static void MatchAndAction<T1, T2>(this Union<T1, T2> union, 
                                                  Action<T1> case1Action, 
                                                  Action<T2> case2Action)
        {
            switch (union.Case)
            {
                case Variant.Case1: { case1Action(union.Case1); break; }
                case Variant.Case2: { case2Action(union.Case2); break; }
            }
        }

        public static void MatchAndAction<T1, T2, T3>(this Union<T1, T2, T3> union,
                                                      Action<T1> case1Action,
                                                      Action<T2> case2Action,
                                                      Action<T3> case3Action)
        {
            switch (union.Case)
            {
                case Variant.Case1: { case1Action(union.Case1); break; }
                case Variant.Case2: { case2Action(union.Case2); break; }
                case Variant.Case3: { case3Action(union.Case3); break; }
            }
        }

        public static void MatchAndAction<T1, T2, T3, T4>(this Union<T1, T2, T3, T4> union, 
                                                          Action<T1> case1Action, 
                                                          Action<T2> case2Action, 
                                                          Action<T3> case3Action, 
                                                          Action<T4> case4Action)
        {
            switch (union.Case)
            {
                case Variant.Case1: { case1Action(union.Case1); break; }
                case Variant.Case2: { case2Action(union.Case2); break; }
                case Variant.Case3: { case3Action(union.Case3); break; }
                case Variant.Case4: { case4Action(union.Case4); break; }
            }
        }
    }
}
