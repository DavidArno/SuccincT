namespace SuccincT.Unions
{
    /// <summary>
    /// Factory class created by Union.UnionCreator{T1,T2}().
    /// </summary>
    internal sealed class UnionCreator<T1, T2> : IUnionCreator<T1, T2>
    {
        public Union<T1, T2> Create(T1 value) => new Union<T1, T2>(value);

        public Union<T1, T2> Create(T2 value) => new Union<T1, T2>(value);
    }
}