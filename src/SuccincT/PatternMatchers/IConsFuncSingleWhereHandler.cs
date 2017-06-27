using System;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncSingleWhereHandler<T, TResult>
    {
        IConsFuncMatcher<T, TResult> Do(TResult value);
        IConsFuncMatcher<T, TResult> Do(Func<T, TResult> doFunc);
    }
}