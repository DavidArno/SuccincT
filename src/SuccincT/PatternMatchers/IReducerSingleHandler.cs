using System;

namespace SuccincT.PatternMatchers
{
    public interface IReducerSingleHandler<T, TResult>
    {
        IReducerMatcher<T, TResult> Do(TResult doValue);
        IReducerMatcher<T, TResult> Do(Func<T,TResult> doFunc);
    }
}