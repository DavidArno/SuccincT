using System;
using SuccincT.Functional;
using SuccincT.Unions;
using static SuccincT.Unions.None;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides an optional value of type T. Modelled on F# options. Either contains a T value, or None.
    /// </summary>
    public sealed class Option<T>
    {
        private readonly Union<T, None> _union;

        private Option()
        {
            _union = new Union<T, None>(none);
        }

        private Option(T value)
        {
            _union = new Union<T, None>(value);
        }

        /// <summary>
        /// Creates an instance of an option with no value.
        /// </summary>
        public static Option<T> None() => new Option<T>();

        /// <summary>
        /// Creates an instance of option with the specified value.
        /// </summary>
        public static Option<T> Some(T value) => new Option<T>(value);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IOptionFuncMatcher<T, TResult> Match<TResult>() => new OptionMatcher<T, TResult>(_union, this);

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
        public T Value
        {
            get
            {
                if (!HasValue) { throw new InvalidOperationException("Option contains no value."); }
                return _union.Case1;
            }
        }

        public override bool Equals(object obj)
        {
            var testObject = obj as Option<T>;
            return obj is Option<T> && testObject._union.Equals(_union);
        }

        public override int GetHashCode() => _union.GetHashCode();

        public static bool operator ==(Option<T> a, Option<T> b)
        {
            var aObj = (object)a;
            var bObj = (object)b;
            return (aObj == null && bObj == null) || (aObj != null && a.Equals(b));
        }

        public static bool operator !=(Option<T> a, Option<T> b) => !(a == b);
    }
}