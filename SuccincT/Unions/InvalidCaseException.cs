using System;

namespace SuccincT.Unions
{
    [Serializable]
    public class InvalidCaseException : InvalidOperationException
    {
        public InvalidCaseException(Variant invalidCase, Variant validCase) :
            base(string.Format("Cannot access union case {0} when case {1} is selected case.", invalidCase, validCase))
        { }
    }
}