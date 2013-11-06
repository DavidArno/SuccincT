namespace SuccincT.Options
{
    public static class Option
    {
        public static Option<T> None<T>()
        {
            return new Option<T>(Unions.None.Value);
        }

        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value);
        }
    }
}
