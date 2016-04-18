namespace SuccincT.Unions
{
    /// <summary>
    /// Factory class created by Union{T1,T2}.Creator(). Whilst this is a public class (as the user needs access
    /// to Create()), it has an internal constructor as it's intended for union creation internal usage only.
    /// </summary>
    public sealed class UnionCreator<T1, T2>
    {
        internal UnionCreator() { }

        public Union<T1, T2> Create(T1 value) => new Union<T1, T2>(value);

        public Union<T1, T2> Create(T2 value) => new Union<T1, T2>(value);
    }
}