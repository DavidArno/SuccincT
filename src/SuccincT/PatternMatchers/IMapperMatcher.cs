using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    public interface IMapperMatcher<T, TResult>

    {
        IMapperNoneHandler<T, TResult> Empty();

        IMapperSingleHandler<T, TResult> Single();

        IMapperRecursiveConsHandler<T, TResult> RecursiveCons();

        IEnumerable<TResult> Result();
    }
}