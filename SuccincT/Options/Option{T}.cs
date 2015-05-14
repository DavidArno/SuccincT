using System;
using SuccincT.Unions;

namespace SuccincT.Options
{
    /// <summary>
    /// Provides an optional value of type T. Modelled on F# options. Either contains a T value, or None.
    /// </summary>
    public class Option<T>
    {
        private readonly Union<T, None> _union;
 
        private Option()
        {
            _union = new Union<T, None>(Unions.None.Value);
        }

        private Option(T value)
        {
            _union = new Union<T, None>(value);
        }

        public static Option<T> None() { return new Option<T>(); }
        public static Option<T> Some(T value) { return new Option<T>(value); }

        public OptionMatcher<T, TResult> Match<TResult>()
        {
            return new OptionMatcher<T, TResult>(this);
        }

        public OptionMatcherUnit<T> MatchAndExec()
        {
            return new OptionMatcherUnit<T>(this);
        }

        public bool HasValue { get { return _union.Case == Variant.Case1; } }

        public T Value
        {
            get
            {
                if (!HasValue) { throw new InvalidOperationException("Option contains no value."); }
                return _union.Case1;
            }
        }
    }
}
