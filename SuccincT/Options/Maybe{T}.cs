using SuccincT.Unions;
using static SuccincT.Unions.None;

namespace SuccincT.Options
{
    /// <summary>
    /// Maybe{T} is an alternative to Option{T}. It behaves in exactly the same way and is implcitly convertable from
    /// one to the other. It is provided simply as an alternative for those who prefer "Maybe" as a type name, or prefer
    /// the use of a struct, to avoid null problems.
    /// </summary>
    public struct Maybe<T>
    {
        private readonly Option<T> _option;
        private readonly bool _correctlyCreated;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        // ReSharper disable once UnusedParameter.Local - unused "_" parameter needed to satisfy "cannot declare 
        // constructor with no parameters in structs" C# language rule.
        private Maybe(None _)
        {
            _option = Option<T>.None();
            _correctlyCreated = true;
        }

        private Maybe(T value)
        {
            _option = Option<T>.Some(value);
            _correctlyCreated = true;
        }

        private Maybe(Option<T> option)
        {
            _option = option;
            _correctlyCreated = (object)option != null;
        }

        /// <summary>
        /// Creates an instance of a maybe with no value.
        /// </summary>
        public static Maybe<T> None() => new Maybe<T>(none);

        /// <summary>
        /// Creates an instance of a maybe with the specified value.
        /// </summary>
        public static Maybe<T> Some(T value) => new Maybe<T>(value);

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Result() being called) returns a TResult value
        /// by invoking the function associated with the match.
        /// </summary>
        public IOptionFuncMatcher<T, TResult> Match<TResult>() => _option.Match<TResult>();

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IOptionActionMatcher<T> Match() => _option.Match();

        /// <summary>
        /// True if created via Some(), false if via None().
        /// </summary>
        public bool HasValue => (object)_option != null && _option.HasValue;

        internal bool CorrectlyLoad => _correctlyCreated;

        /// <summary>
        /// The value held (if created by Some()). Will throw an InvalidOperationException if created via None().
        /// </summary>
        public T Value => _option.Value;

        // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
        public override bool Equals(object obj)
        {
            if (obj is Maybe<T>) return _option.EqualsMaybe((Maybe<T>)obj);
            var optionObj = (Option<T>)obj;
            return (object)optionObj != null && _option.EqualsOption(optionObj);
        }

        public override int GetHashCode() => _option.GetHashCode();

        public static bool operator ==(Maybe<T> a, Maybe<T> b) => a._option.EqualsOption(b._option);

        public static bool operator !=(Maybe<T> a, Maybe<T> b) => !a._option.EqualsOption(b._option);

        public static implicit operator Option<T>(Maybe<T> maybe) => maybe._option;
        public static implicit operator Maybe<T>(Option<T> option) => new Maybe<T>(option);
    }
}