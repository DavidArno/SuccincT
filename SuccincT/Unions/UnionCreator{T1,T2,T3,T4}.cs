namespace SuccincT.Unions
{
    /// <summary>
    /// Factory class created by Union{T1,T2,T3,T4}.Creator(). Whilst this is a public class (as the user needs access
    /// to Create()), it has an internal constructor as it's intended for union creation internal usage only.
    /// </summary>
    public sealed class UnionCreator<T1, T2, T3, T4>
    {
        internal UnionCreator() { }

        public Union<T1, T2, T3, T4> Create(T1 value)
        {
            return new Union<T1, T2, T3, T4>(value);
        }

        public Union<T1, T2, T3, T4> Create(T2 value)
        {
            return new Union<T1, T2, T3, T4>(value);
        }

        public Union<T1, T2, T3, T4> Create(T3 value)
        {
            return new Union<T1, T2, T3, T4>(value);
        }

        public Union<T1, T2, T3, T4> Create(T4 value)
        {
            return new Union<T1, T2, T3, T4>(value);
        }
    }
}