using System;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncSingleHandler<T, TResult>
    {
        IConsFuncSingleWhereHandler<T, TResult> Where(Func<T, bool> testFunc);
        IConsFuncMatcher<T, TResult> Do(TResult value);
        IConsFuncMatcher<T, TResult> Do(Func<T, TResult> doFunc);
    }
}