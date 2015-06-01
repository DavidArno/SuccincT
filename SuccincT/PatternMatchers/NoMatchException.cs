using System;

namespace SuccincT.PatternMatchers
{
    [Serializable]
    public class NoMatchException : Exception
    {
        public NoMatchException(string message) : base(message) { }
    }
}