using System;

namespace SuccincT.Unions
{
    public sealed class InvalidCaseException : InvalidOperationException
    {
        public InvalidCaseException(Variant invalidCase, Variant validCase) :
            base($"Cannot access union case {invalidCase} when case {validCase} is selected case.") { }
    }
}