namespace SuccincT.Options
{
    public class Some<T>
    {
        public static bool operator is(Option<T> option, out T value)
        {
            value = option.HasValue ? option.Value : default(T);
            return option.HasValue;
        }
    }
}
