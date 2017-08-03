using System.Collections.Generic;

namespace SuccincT.PatternMatchers
{
    public interface IReducerMatcher<T, TResult>
    {
        IReducerNoneHandler<T, TResult> Empty();

        IReducerSingleHandler<T, TResult> Single();

        //IReducerRecursiveConsHandler<T, TResult> RecursiveCons();

        IEnumerable<TResult> Result();
    }
}