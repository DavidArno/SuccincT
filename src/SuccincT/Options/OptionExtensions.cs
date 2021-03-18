using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Functional;

namespace SuccincT.Options
{
    public static class OptionExtensions
    {
        public static Option<TOutput> Map<TInput, TOutput>(this Option<TInput> input, Func<TInput, TOutput> f)
            => input switch {
                (Option.Some, var value) => f(value).Into(Option<TOutput>.Some),
                _ => Option<TOutput>.None()
            };

        public static Option<T> Or<T>(this Option<T> option, Option<T> anotherOption)
            => option.HasValue ? option : anotherOption;

        public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> lazyAnotherOption)
            => option.HasValue ? option : lazyAnotherOption();

        public static Option<T> Flatten<T>(this Option<Option<T>> option)
            => option switch {
                (Option.Some, var value) => value,
                _ => Option<T>.None()
            };

        public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> options)
            => options.Where(x => x.HasValue).Select(x => x.Value);

        public static IEnumerable<TU> Choose<T, TU>(this IEnumerable<T> items, Func<T, Option<TU>> optionSelector)
            => items.Select(optionSelector).Choose();

        public static Option<T> Some<T>(this T value) => value.Into(Option<T>.Some);
    }
}