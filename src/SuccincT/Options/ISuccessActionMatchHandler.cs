using System;

namespace SuccincT.Options
{
    public interface ISuccessActionMatchHandler<T>
    {
        ISuccessActionMatcher<T> Do(Action action);
    }
}