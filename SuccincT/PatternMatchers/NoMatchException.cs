using System;

namespace SuccincT.PatternMatchers
{
    public class NoMatchException<T> : Exception
    {
        public NoMatchException(object item)
        {
            throw new NotImplementedException();
        }
    }
}