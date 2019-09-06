using System;

namespace SuccincT.Options
{
    public static class Success
    {
        public static Success<T> CreateFailure<T>(T error) 
            => error is {} ? new Success<T>(error) : throw new ArgumentNullException(nameof(error));
    }
}
