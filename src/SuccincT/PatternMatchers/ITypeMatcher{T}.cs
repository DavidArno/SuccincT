namespace SuccincT.PatternMatchers
{
    public interface ITypeMatcher<T>
    {
        ITypeMatcher<T, TResult> To<TResult>();
    }
}