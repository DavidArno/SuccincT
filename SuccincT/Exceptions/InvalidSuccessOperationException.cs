using System;

namespace SuccincT.Exceptions
{
    /// <summary>
    /// Defines the exception type that implementation of ISuccess should use. See 
    /// <code>ISuccess</code> for more details.
    /// </summary>
    public class InvalidSuccessOperationException : Exception
    {
        public InvalidSuccessOperationException(string message) : base(message) { }
    }
}
