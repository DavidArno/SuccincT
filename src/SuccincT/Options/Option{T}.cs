using SuccincT.Functional;
using SuccincT.Unions;
using System;
using static SuccincT.Unions.None;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides an optional value of type T. Modelled on F# options. Either contains a T value, or None.
    /// </summary>
    public readonly struct Option<T>
    {
        private readonly T _value;

        public Option(T value) => (HasValue, _value) = (value is {}, value);

        /// <summary>
        /// Creates an instance of an option with no value.
        /// </summary>
        public static Option<T> None() => default;

        /// <summary>
        /// Creates an instance of option with the specified value.
        /// </summary>
        public static Option<T> Some(T value) => value is {} valid
            ? new Option<T>(valid)
            : throw new ArgumentNullException(
                "null cannot be used with Option<T>.Some() as null is equivalent to None.\n" +
                "Please use Option<T>.None() or new Option<T>(null) instead.");

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IOptionFuncMatcher<T, TResult> Match<TResult>()
            => new OptionMatcher<T, TResult>(CreateUnion(), this);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IOptionActionMatcher<T> Match() => new OptionMatcher<T, Unit>(CreateUnion(), this);

        /// <summary>
        /// True if created via Some(), false if via None().
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// The value held (if created by Some()). Will throw an InvalidOperationException if created via None().
        /// </summary>
        public T Value => HasValue
            ? _value
            : throw new InvalidOperationException("Option contains no value.");

        public T ValueOrDefault => HasValue ? _value : default;

        public override bool Equals(object obj)
            => obj is Option<T> option ? EqualsOption(option) : obj is null && !HasValue;

        internal bool EqualsOption(Option<T> other)
            => HasValue && other.HasValue && Value is {} value && value.Equals(other.Value) ||
               !(HasValue || other.HasValue);

        public override int GetHashCode() => HasValue && _value is {} value ? value.GetHashCode() : 0;

        public static bool operator ==(Option<T> a, Option<T> b) => a.EqualsOption(b);

        public static bool operator !=(Option<T> a, Option<T> b) => !a.EqualsOption(b);

        public static implicit operator Option<T>(T value) => new Option<T>(value);

        public void Deconstruct(out bool hasValue, out T value) =>
            (hasValue, value) = (HasValue, HasValue ? _value : default);

        private Union<T, None> CreateUnion() => HasValue ? new Union<T, None>(_value) : new Union<T, None>(none);
    }
}