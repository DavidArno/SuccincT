namespace SuccincT.PatternMatchers
{
    internal sealed class TypeMatcher<T> : ITypeMatcher<T>
    {
        private readonly T _item;

        public TypeMatcher(T item) { _item = item; }

        public ITypeMatcher<T, TResult> To<TResult>() => new TypeMatcher<T, TResult>(_item);
    }
}
