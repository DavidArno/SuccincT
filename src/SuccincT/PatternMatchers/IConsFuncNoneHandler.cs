namespace SuccincT.PatternMatchers
{
    public interface IConsFuncNoneHandler<T, TResult>
    {
        IConsFuncMatcher<T, TResult> Do(TResult value);
    }
}