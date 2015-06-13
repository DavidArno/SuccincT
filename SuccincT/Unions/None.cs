namespace SuccincT.Unions
{
    /// <summary>
    /// Singleton value class that encapsulates the None literal used by various Succinct classes.
    /// </summary>
    public sealed class None
    {
        private static readonly None TheNone = new None();

        private None() { }

        /// <summary>
        /// The sole implemented value of None. f used with the the Option{T} type, this value need never be explicitly
        /// accessed. However, it is exposed for third-party use, should the need arise
        /// </summary>
        public static None Value { get { return TheNone; } }
    }
}