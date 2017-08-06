using System;

namespace SuccincT.PatternMatchers
{
    public interface IMapperSingleHandler<T, TResult>
    {
        IMapperMatcher<T, TResult> Do(TResult doValue);
        IMapperMatcher<T, TResult> Do(Func<T,TResult> doFunc);
    }
}