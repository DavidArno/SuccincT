using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelectorData<T1, T2, TR>
    {
        public MatchFunctionSelectorData(Func<T1, IList<T2>, bool>? withTestFunc,
                                         Func<T1, bool>? whereTestFunc,
                                         IList<T2>? withList,
                                         Func<T1, TR> actionFunc)
        {
            WithTestFunc = withTestFunc;
            WhereTestFunc = whereTestFunc;
            WithList = withList;
            ActionFunc = actionFunc;
        }

        internal Func<T1, IList<T2>, bool>? WithTestFunc { get; }
        internal IList<T2>? WithList { get; }
        internal Func<T1, bool>? WhereTestFunc { get; }
        internal Func<T1, TR> ActionFunc { get; }
    }
}