using System;
using System.Diagnostics.CodeAnalysis;

//
// This file is largely copied from https://github.com/247Entertainment/E247.Fun/blob/master/E247.Fun/Unit.cs & those
// copied parts are copyright (c) 2016 247Entertainment.
//

// ReSharper disable UnusedParameter.Global - required to suppress complaints on == and != (params needed, but not used)
// ReSharper disable InconsistentNaming - "unit" feels better than "Value", especially when static imports are used.

namespace SuccincT.Functional
{
    public readonly struct Unit : IEquatable<Unit>
    {
        public override int GetHashCode() => 0;

        public override bool Equals(object? obj) => obj is Unit;

        public override string ToString() => "()";

        public bool Equals(Unit other) => true;

        [SuppressMessage("Style", "IDE0079:Remove unnecessary suppresion", Justification = "Shuts up an anlyzer bug")]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "")]
        public static bool operator ==(Unit u1, Unit u2) => true;

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "")]
        public static bool operator !=(Unit u1, Unit u2) => false;

        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public static Unit unit { get; } = new Unit();
    }
}