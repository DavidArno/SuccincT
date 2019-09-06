using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal sealed class ConsActionMatcher<T> : IConsActionMatcher<T>
    {
        private readonly IEnumerable<T> _collection;

        public ConsActionMatcher(IEnumerable<T> collection) => _collection = collection;

        public IConsFuncMatcher<T, TResult> To<TResult>() => new ConsFuncMatcher<T, TResult>(_collection);

        public IMapperMatcher<T, TResult> MapTo<TResult>()  => new MapperMatcher<T, TResult>(_collection);
    }
}