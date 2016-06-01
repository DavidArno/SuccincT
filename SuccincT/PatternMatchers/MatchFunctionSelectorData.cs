using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal sealed class MatchFunctionSelectorData<T, TR>
    {
        public MatchFunctionSelectorData(Func<T, IList<T>, bool> withTestFunc,
                                         Func<T, bool> whereTestFunc,
                                         IList<T> withList,
                                         Func<T, TR> actionFunc)
        {
            WithTestFunc = withTestFunc;
            WhereTestFunc = whereTestFunc;
            WithList = withList;
            ActionFunc = actionFunc;
        }

        internal Func<T, IList<T>, bool> WithTestFunc { get; }
        internal IList<T> WithList { get; }
        internal Func<T, bool> WhereTestFunc { get; }
        internal Func<T, TR> ActionFunc { get; }
    }
}