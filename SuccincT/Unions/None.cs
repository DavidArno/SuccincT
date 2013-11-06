namespace SuccincT.Unions
{
    public sealed class None
    {
        private static readonly None TheNone = new None();
        private None() { }

        public static None Value { get { return TheNone; } }
    }
}
