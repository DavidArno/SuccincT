using System;
using SuccincT.Unions;

namespace SuccincT.Exceptions
{
    public class InvalidCaseException : Exception
    {
        public InvalidCaseException(Variant invalidCase, Variant validCase) :
            base(string.Format("Cannot access union case {0} when case {1} is selected case.", invalidCase, validCase))
        { }
    }
}
