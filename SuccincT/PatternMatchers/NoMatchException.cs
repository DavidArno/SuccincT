using System;

namespace SuccincT.PatternMatchers
{
    [Serializable]
    public sealed class NoMatchException : Exception
    {
        public NoMatchException(string message) : base(message) { }
    }
}