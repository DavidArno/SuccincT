namespace SuccincT.SuccessTypes
{
    /// <summary>
    /// Defines a return type from a method that might fail for non-exceptional reasons and
    /// thus doesn't warrant exception throwing.
    /// </summary>
    /// <remarks>
    /// The expected behaviour of any implementation is that, if Successful is true, Value should
    /// be readable and FailureReason should throw an InvalidSuccessOperationException if
    /// read. If Successful is false, Value should throw the InvalidSuccessOperationException
    /// and FailureReason should contain text of some sort.
    /// </remarks>
    public interface ISuccess<out T>
    {
        /// <summary>
        /// True if the function was successful in determining a value; false otherwise.
        /// </summary>
        bool Successful { get; }

        /// <summary>
        /// The value, if successfully determined. Otherwise should throw an 
        /// InvalidSuccessOperationException.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// If no value was determined, an explanation as to why. Otherwise should throw an 
        /// InvalidSuccessOperationException.
        /// </summary>
        string FailureReason { get; }
    }
}
