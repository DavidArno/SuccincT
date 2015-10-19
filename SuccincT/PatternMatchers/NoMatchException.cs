using System;

namespace SuccincT.PatternMatchers
{
    public sealed class NoMatchException : Exception
    {
        public NoMatchException(string message) : base(message) { }
    }
}