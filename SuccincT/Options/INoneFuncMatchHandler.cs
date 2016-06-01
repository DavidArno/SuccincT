using System;

namespace SuccincT.Options
{
    public interface INoneFuncMatchHandler<T, TResult>
    {
        IOptionFuncMatcher<T, TResult> Do(Func<TResult> action);

        IOptionFuncMatcher<T, TResult> Do(TResult value);
    }
}