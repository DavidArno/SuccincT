namespace SuccincT.PatternMatchers
{
    public interface IConsFuncSingleWhereHandler<T, TResult>
    {
        IConsFuncMatcher<T, TResult> Do(TResult value);
    }
}