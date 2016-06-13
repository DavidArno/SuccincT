using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedParameter.Global - required to suppress complains on == and != (params needed, but not used)
// ReSharper disable InconsistentNaming - "unit" feels better than "Value", especially when static imports are used.
namespace SuccincT.Functional
{
    public struct Unit : IEquatable<Unit>
    {
        public override int GetHashCode() => 0;

        public override bool Equals(object obj) => obj is Unit;

        public override string ToString() => "()";

        public bool Equals(Unit other) => true;

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        public static bool operator ==(Unit u1, Unit u2) => true;

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        public static bool operator !=(Unit u1, Unit u2) => false;

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "unit")]
        public static Unit unit { get; } = new Unit();

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        public static void Ignore<T>(T anything) { }
    }
}