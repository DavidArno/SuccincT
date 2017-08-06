using System;
using SuccincT.Functional;

namespace SuccincT.PatternMatchers
{
    public interface IMapperRecursiveConsHandler<T, TResult>
    {
        IMapperMatcher<T, TResult> Do(Func<T, T, IConsEnumerable<TResult>, IConsEnumerable<TResult>> doFunc);
    }
}