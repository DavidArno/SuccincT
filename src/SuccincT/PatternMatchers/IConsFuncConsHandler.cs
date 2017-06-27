using System;
using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncConsHandler<T, TResult>
    {
        IConsFuncConsWhereHandler<T, TResult> Where(Func<T, IEnumerable<T>, bool> testFunc);
        IConsFuncMatcher<T, TResult> Do(TResult value);
        IConsFuncMatcher<T, TResult> Do(Func<T, IEnumerable<T>, TResult> doFunc);
    }
}