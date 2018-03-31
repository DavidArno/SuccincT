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
        // ReSharper disable once UnusedParameter.Local - unit param used to
        // prevent JSON serializer from using this constructor to create an invalid maybe.
        private Maybe(None _)
        {
            Option = Option<T>.None();
            CorrectlyLoad = true;
        }

        private Maybe(T value)
        {
            Option = Option<T>.Some(value);
            CorrectlyLoad = true;
        }

        private Maybe(Option<T> option)
        {
            Option = option;
            CorrectlyLoad = (object)option != null;
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
        public IOptionFuncMatcher<T, TResult> Match<TResult>() => Option.Match<TResult>();

        /// <summary>
        /// Provides a fluent matcher that ultimately (upon Exec() being called) invokes the Action
        /// associated with the match.
        /// </summary>
        public IOptionActionMatcher<T> Match() => Option.Match();

        /// <summary>
        /// True if created via Some(), false if via None().
        /// </summary>
        public bool HasValue => (object)Option != null && Option.HasValue;

        internal bool CorrectlyLoad { get; }

        /// <summary>
        /// The value held (if created by Some()). Will throw an InvalidOperationException if created via None().
        /// </summary>
        public T Value => Option.Value;

        public T ValueOrDefault => HasValue ? Option.Value : default;

        // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Maybe<T> maybe: return Option.EqualsMaybe(maybe);
                case Option<T> option: return Option.EqualsOption(option);
                default: return false;
            }
        }

        public override int GetHashCode() => Option.GetHashCode();

        public static bool operator ==(Maybe<T> a, Option<T> b) => a.Option == b;

        public static bool operator !=(Maybe<T> a, Option<T> b) => a.Option != b;

        internal Option<T> Option { get; }

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);
        public static implicit operator Maybe<T>(Option<T> option) => new Maybe<T>(option);

        public void Deconstruct(out bool hasValue, out T value) =>
            (hasValue, value) = (HasValue, HasValue ? Option.Value : default);
    }
}