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

        public override bool Equals(object obj) => obj is None;

        public override int GetHashCode() => 0;

        public static bool operator ==(None left, None right) => true;

        public static bool operator !=(None left, None right) => false;
    }
}