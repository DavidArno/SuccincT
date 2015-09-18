namespace SuccincT.Unions
{
    public static class Union
    {
        public static UnionCreator<T1, T2> UnionCreator<T1, T2>() => new UnionCreator<T1, T2>();

        public static UnionCreator<T1, T2, T3> UnionCreator<T1, T2, T3>() => new UnionCreator<T1, T2, T3>();

        public static UnionCreator<T1, T2, T3, T4> UnionCreator<T1,T2,T3,T4>() => new UnionCreator<T1, T2, T3, T4>();
    }
}