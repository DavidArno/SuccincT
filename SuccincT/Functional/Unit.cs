using System;

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

        public static bool operator ==(Unit u1, Unit u2) => true;

        public static bool operator !=(Unit u1, Unit u2) => false;

        public static Unit unit { get; } = new Unit();

        public static void Ignore(object anything) { }
    }
}