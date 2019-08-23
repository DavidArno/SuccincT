namespace SuccincT.PatternMatchers
{
    public interface IConsActionMatcher<T>

    {
        IConsFuncMatcher<T, TResult> To<TResult>();
        IMapperMatcher<T, TResult> MapTo<TResult>();
    }
}