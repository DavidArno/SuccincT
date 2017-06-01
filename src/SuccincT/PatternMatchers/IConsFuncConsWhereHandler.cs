using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncConsWhereHandler<T, TResult>
    {
        IConsFuncMatcher<T, TResult> Do(TResult value);
        IConsFuncMatcher<T, TResult> Do(Func<T, IEnumerable<T>, TResult> doFunc);
    }
}