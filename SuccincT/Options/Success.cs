using System;

namespace SuccincT.Options
{
    public static class Success
    {
        public static Success<T> CreateFailure<T>(T error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));

            return new Success<T>(error);
        }
    }
}
