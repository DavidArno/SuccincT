// ReSharper disable InconsistentNaming - "none" seems a better fit name than "Value". 

using System;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Unions
{
    /// <summary>
    /// Singleton value class that encapsulates the None literal used by various Succinct classes.
    /// </summary>
    public sealed class None
    {
        private None() { }

        /// <summary>
        /// The sole implemented value of None. If used with the the Option{T} type, this value need never be explicitly
        /// accessed. However, it is exposed for third-party use, should the need arise
        /// </summary>
        [SuppressMessage("Microsoft.Naming","CA1709:IdentifiersShouldBeCasedCorrectly")]
        public static None none { get; } = new None();

        [Obsolete("None.Value has been replaced with None.none and will be removed in v2.1.")]
        // ReSharper disable once UnusedMember.Global - Obsolete
        public static None Value => none;

        public override string ToString() => "!none!";
    }
}