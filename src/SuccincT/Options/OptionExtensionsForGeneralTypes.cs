namespace SuccincT.Options
{
    public static class OptionExtensionsForGeneralTypes
    {
        public static Option<T> ToOption<T>(this T? obj) where T : class => obj ?? Option<T>.None();

        public static Option<T> ToOption<T>(this T? obj) where T : struct => obj ?? Option<T>.None();

        public static T? AsNullable<T>(this Option<T> option) where T : struct
            => option.HasValue ? (T?)option.Value : null;

        public static Option<T> TryCast<T>(this object? value) where T : class => (value as T).ToOption();
    }
}
