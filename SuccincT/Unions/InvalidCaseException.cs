using System;

namespace SuccincT.Unions
{
    [Serializable]
    public class InvalidCaseException : InvalidOperationException
    {
        public InvalidCaseException(Variant invalidCase, Variant validCase) :
            base($"Cannot access union case {invalidCase} when case {validCase} is selected case.")
        { }
    }
}