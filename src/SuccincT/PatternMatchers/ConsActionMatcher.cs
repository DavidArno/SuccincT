using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    internal class ConsActionMatcher<T> : IConsActionMatcher<T>
    {
        private readonly IEnumerable<T> _collection;

        public ConsActionMatcher(IEnumerable<T> collection) => _collection = collection;

        public IConsFuncMatcher<T, TResult> To<TResult>() => new ConsFuncMatcher<T, TResult>(_collection);
        public IReducerMatcher<T, TResult> ReduceTo<TResult>() => new ReducerMatcher<T, TResult>(_collection);
    }
}