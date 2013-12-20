namespace SuccincT.Options
{
    /// <summary>
    /// Static class that provides two helper methods for creating instances of the Option{T} class.
    /// </summary>
    public static class Option
    {
        public static Option<T> None<T>()
        {
            return new Option<T>();
        }

        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value);
        }
    }
}
