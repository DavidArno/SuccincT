namespace SuccincT.Options
{
    public class None<T>
    {
        public static bool operator is(Option<T> option) => option == null || !option.HasValue;
    }
}
