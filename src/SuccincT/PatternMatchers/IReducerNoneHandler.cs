namespace SuccincT.PatternMatchers
{
    public interface IReducerNoneHandler<T, TResult>
    {
        IReducerMatcher<T, TResult> Do(TResult doValue);
    }
}