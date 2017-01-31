using SuccincT.Functional;

namespace SuccincT.Options
{
    public static class OptionExtensionsForGeneralTypes
    {
        public static Option<T> ToOption<T>(this T obj) => 
            obj != null ? obj.Into(Option<T>.Some) : Option<T>.None();

        public static T? AsNullable<T>(this Option<T> option) where T : struct => 
            option.HasValue ? option.Value : (T?) null;
    }
}
