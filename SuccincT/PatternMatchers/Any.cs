using System.Diagnostics.CodeAnalysis;

namespace SuccincT.PatternMatchers
{
    public struct Any
    {
        private static readonly Any AnAny = new Any();

        public override int GetHashCode() => 0;

        public override bool Equals(object obj) => obj is Any;

        public override string ToString() => "_";

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "other")]
        public bool Equals(Any other) => true;

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        public static bool operator ==(Any any1, Any any2) => true;

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        public static bool operator !=(Any any1, Any any2) => false;

        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        // ReSharper disable once ConvertToAutoProperty
        public static Any _ => AnAny;
    }
}
