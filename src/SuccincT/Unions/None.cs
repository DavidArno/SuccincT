using SuccincT.Functional;
using static SuccincT.Functional.Unit;

namespace SuccincT.Unions
{
    /// <summary>
    /// valueless struct that encapsulates the "none" literal used by various Succinct types.
    /// </summary>
    public readonly struct None
    {
        public static None none { get; } = default;

        public override string ToString() => "!none!";
    }
}