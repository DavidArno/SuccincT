namespace SuccincT.Unions
{
    internal interface IUnion<out T1, out T2, out T3, out T4>
    {
        Variant Case { get; }
        T1 Case1 { get; }
        T2 Case2 { get; }
        T3 Case3 { get; }
        T4 Case4 { get; }
    }
}