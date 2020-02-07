using System;

namespace SuccincT.Unions
{
    public sealed class InvalidCaseOfTypeException : InvalidOperationException
    {
        internal InvalidCaseOfTypeException(Type caseOfType) :
            base($"Union doesn't have a case of type {caseOfType}.") {}
    }
}