using System;

namespace SuccincT.PatternMatchers
{
    public interface IMapperSingleHandler<T, TResult>
    {
        IMapperSingleWhereHandler<T, TResult> Where(Func<T, bool> whereTest);

        IMapperMatcher<T, TResult> Do(TResult doValue);
        IMapperMatcher<T, TResult> Do(Func<T,TResult> doFunc);
    }
}