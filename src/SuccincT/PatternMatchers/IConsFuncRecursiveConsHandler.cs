using System;

namespace SuccincT.PatternMatchers
{
    public interface IConsFuncRecursiveConsHandler<T, TResult>
    {
        IConsFuncRecursiveConsWhereHandler<T, TResult> Where(Func<T, bool> testFunc);

        IConsFuncMatcher<T, TResult> Do(Func<T, TResult, TResult> doFunc);
    }
}