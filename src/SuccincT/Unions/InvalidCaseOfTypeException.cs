using System;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Unions
{
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public sealed class InvalidCaseOfTypeException : InvalidOperationException
    {
        internal InvalidCaseOfTypeException(Type caseOfType) :
            base($"Union doesn't have a case of type {caseOfType}.") { }
    }
}