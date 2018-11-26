using System;
using SuccincT.Functional;
using SuccincT.Unions;

namespace SuccincT.Options
{
    public readonly struct Success<T>
    {
        private readonly T _error;

        internal Success(T error) => (IsFailure, _error) = (true, error);

        public bool IsFailure { get; }

        public T Failure 
            => IsFailure
                ? _error
                : throw new InvalidOperationException("Cannot fetch a Failure for an error-free Success<T> value.");

        public ISuccessFuncMatcher<T, TResult> Match<TResult>() => new SuccessMatcher<T, TResult>(CreateUnion(), this);

        public ISuccessActionMatcher<T> Match() => new SuccessMatcher<T, Unit>(CreateUnion(), this);

        public override bool Equals(object obj)
            => obj is Success<T> other &&
               other.IsFailure == IsFailure &&
               (IsFailure && other.Failure.Equals(_error) || !IsFailure);

        public override int GetHashCode() => IsFailure ? _error.GetHashCode() : 1;

        public static bool operator ==(Success<T> a, object b) => a.Equals(b);

        public static bool operator !=(Success<T> a, object b) => !a.Equals(b);

        public static implicit operator bool(Success<T> success) => !success.IsFailure;
        public static implicit operator Success<T>(T value) => Success.CreateFailure(value);

        public void Deconstruct(out bool isSuccess, out T error) 
            => (isSuccess, error) = (!IsFailure, IsFailure ? _error : default);

        private Union<T, bool> CreateUnion() => IsFailure ? new Union<T, bool>(_error) : new Union<T, bool>(true);
    }

}
