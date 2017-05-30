using System;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncSingleHandler<T, TResult>
    {
        IConsFuncMatcher<T, TResult> Do(TResult value);

        IConsFuncSingleWhereHandler<T, TResult> Where(Func<T, bool> testFunc);
    }
}