using SuccincT.Exceptions;

namespace SuccincT.SuccessTypes
{
    internal class Success<T> : ISuccess<T>
    {
        private const string NoValueWhenNotSuccessful = "Operation wasn't successful, so no value exists.";
        private const string NoMessageWhenSuccessful = "Operation was successful, so no failure message exists.";

        private T _value;
        private string _failureReason;

        public bool Successful { get; internal set; }

        public T Value
        {
            get
            {
                if (!Successful) { throw new InvalidSuccessOperationException(NoValueWhenNotSuccessful); }
                return _value;
            }
            internal set { _value = value; }
        }

        public string FailureReason
        {
            get
            {
                if (Successful) { throw new InvalidSuccessOperationException(NoMessageWhenSuccessful); }
                return _failureReason;
            }
            internal set { _failureReason = value; }
        }
    }
}
