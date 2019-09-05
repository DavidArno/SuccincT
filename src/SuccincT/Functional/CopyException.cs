using System;

namespace SuccincT.Functional
{
    public sealed class CopyException : Exception
    {
        public CopyException(string message) : base(message) { }

        public CopyException(string message, Exception innerException) : base(message, innerException) { }
    }
}