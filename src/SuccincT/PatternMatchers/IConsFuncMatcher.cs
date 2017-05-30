namespace SuccincT.PatternMatchers
{
    public interface IConsFuncMatcher<T, TResult>
    {
        IConsFuncNoneHandler<T, TResult> Empty();

        IConsFuncSingleHandler<T, TResult> Single();

        TResult Result();
    }
}