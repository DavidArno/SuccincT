using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Unions
{
    /// <summary>
    /// valueless struct that encapsulates the "none" literal used by various Succinct types.
    /// </summary>
    public readonly struct None
    {
        // ReSharper disable once InconsistentNaming
        public static None none { get; } = default;

        public override string ToString() => "!none!";

        public override bool Equals(object? obj) => obj is None;

        public override int GetHashCode() => 0;

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "")]
        public static bool operator ==(None left, None right) => true;

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "")]
        public static bool operator !=(None left, None right) => false;
    }
}