using System;

namespace SuccincT.Options
{
    public interface ISuccessFuncMatchHandler<T, TResult>
    {
        ISuccessFuncMatcher<T, TResult> Do(Func<TResult> function);

        ISuccessFuncMatcher<T, TResult> Do(TResult value);
    }
}