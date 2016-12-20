using System;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Unions
{
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public sealed class InvalidCaseException : InvalidOperationException
    {
        public InvalidCaseException(Variant invalidCase, Variant validCase) :
            base($"Cannot access union case {invalidCase} when case {validCase} is selected one.") { }
    }
}