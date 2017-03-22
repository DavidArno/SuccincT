namespace SuccincT.Unions
{
    public interface IUnionCreator<T1, T2, T3>
    {
        Union<T1, T2, T3> Create(T1 value);
        Union<T1, T2, T3> Create(T2 value);
        Union<T1, T2, T3> Create(T3 value);
    }
}