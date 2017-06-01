namespace SuccincT.PatternMatchers
{
    public interface IConsFuncMatcher<T, TResult>
    {
        IConsFuncNoneHandler<T, TResult> Empty();

        IConsFuncSingleHandler<T, TResult> Single();

        IConsFuncConsHandler<T, TResult> Cons();

        TResult Result();
    }
}