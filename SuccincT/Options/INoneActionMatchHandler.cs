using System;

namespace SuccincT.Options
{
    public interface INoneActionMatchHandler<T>
    {
        IOptionActionMatcher<T> Do(Action action);
    }
}