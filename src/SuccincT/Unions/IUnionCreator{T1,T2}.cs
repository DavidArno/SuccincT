namespace SuccincT.Unions
{
    public interface IUnionCreator<T1, T2>
    {
        Union<T1, T2> Create(T1 value);
        Union<T1, T2> Create(T2 value);
    }
}