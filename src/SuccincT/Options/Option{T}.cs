using System;
using System.Diagnostics.CodeAnalysis;
using SuccincT.Functional;
using SuccincT.Unions;
using static SuccincT.Functional.Unit;
using static SuccincT.Unions.None;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides an optional value of type T. Modelled on F# options. Either contains a T value, or None.
    /// </summary>
    public sealed class Option<T>
    {
        private static readonly Option<T> NoneInstance = new Option<T>(unit);
        private readonly Union<T, None> _union;

        // ReSharper disable once UnusedParameter.Local - unit param used to
        // prevent JSON serializer from using this constructor to create an invalid union.
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "_")]
        private Option(Unit _) => _union = new Union<T, None>(none);

        private Option(T value) => _union = new Union<T, None>(value);

        /// <summary>
        /// Creates an instance of an option with no value.
        /// </summary>
        public static Option<T> None() => NoneInstance;

        /// <summary>
        /// Creates an instance of option with the specified value.
        /// </summary>
        public static Option<T> Some(T value) => new Option<T>(value);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IOptionFuncMatcher<T, TResult> Match<TResult>() => 
            new OptionMatcher<T, TResult>(_union, this);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IOptionActionMatcher<T> Match() => new OptionMatcher<T, Unit>(_union, this);

        /// <summary>
        /// True if created via Some(), false if via None().
        /// </summary>
        public bool HasValue => _union.Case == Variant.Case1;

        /// <summary>
        /// The value held (if created by Some()). Will throw an InvalidOperationException if created via None().
        /// </summary>
        public T Value => HasValue 
            ? _union.Case1 
            : throw new InvalidOperationException("Option contains no value.");

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override bool Equals(object obj)
        {
            if (obj is Option<T> option) return EqualsOption(option);
            if (obj is Maybe<T> maybe) return EqualsMaybe(maybe);
            return false;
        }

        internal bool EqualsOption(Option<T> other) =>
            !ReferenceEquals(other, null) && (other.HasValue && HasValue && Value.Equals(other.Value) || !(HasValue || other.HasValue));

        internal bool EqualsMaybe(Maybe<T> other) =>
            other.HasValue && HasValue && Value.Equals(other.Value) || !(HasValue || !other.CorrectlyLoad || other.HasValue);

        public override int GetHashCode() => _union.GetHashCode();

        public static bool operator ==(Option<T> a, Option<T> b) => ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.EqualsOption(b);

        public static bool operator !=(Option<T> a, Option<T> b) => a == null ? b != null : !a.EqualsOption(b);

        public static implicit operator Option<T>(T value) => new Option<T>(value);
        public static implicit operator Option<T>(Maybe<T> maybe) => maybe.Option;
    }
}