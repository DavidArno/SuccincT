namespace SuccincT.Unions
{
    /// <summary>
    /// Factory class created by Union.UnionCreator{T1,T2,T3}().
    /// </summary>
    internal sealed class UnionCreator<T1, T2, T3> : IUnionCreator<T1, T2, T3>
    {
        public Union<T1, T2, T3> Create(T1 value) => new Union<T1, T2, T3>(value);

        public Union<T1, T2, T3> Create(T2 value) => new Union<T1, T2, T3>(value);

        public Union<T1, T2, T3> Create(T3 value) => new Union<T1, T2, T3>(value);
    }
}