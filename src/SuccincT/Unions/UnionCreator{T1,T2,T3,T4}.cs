namespace SuccincT.Unions
{
    /// <summary>
    /// Factory class created by Union.UnionCreator{T1,T2,T3,T4}(). 
    /// </summary>
    internal sealed class UnionCreator<T1, T2, T3, T4> : IUnionCreator<T1, T2, T3, T4>
    {
        public Union<T1, T2, T3, T4> Create(T1 value) => new Union<T1, T2, T3, T4>(value);

        public Union<T1, T2, T3, T4> Create(T2 value) => new Union<T1, T2, T3, T4>(value);

        public Union<T1, T2, T3, T4> Create(T3 value) => new Union<T1, T2, T3, T4>(value);

        public Union<T1, T2, T3, T4> Create(T4 value) => new Union<T1, T2, T3, T4>(value);
    }
}