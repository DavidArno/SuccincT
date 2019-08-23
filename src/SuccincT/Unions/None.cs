using SuccincT.Functional;
using static SuccincT.Functional.Unit;

namespace SuccincT.Unions
{
    /// <summary>
    /// Singleton value class that encapsulates the None literal used by various Succinct classes.
    /// </summary>
    public sealed class None
    {
        private static readonly None TheOnlyNone = new None(unit);

        private None(Unit _) { }

        /// <summary>
        /// The sole implemented value of None. If used with the the Option{T} type, this value
        /// need never be explicitly accessed. However, it is exposed for third-party use, should
        /// the need arise
        /// </summary>
        public static None none { get; } = TheOnlyNone;

        public override string ToString() => "!none!";
    }
}