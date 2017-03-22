namespace SuccincT.PatternMatchers
{
    public interface IFuncMatcherAfterElse<out TResult>
    {
        TResult Result();
    }
}