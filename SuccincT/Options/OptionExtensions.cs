using System;
using System.Collections.Generic;
using System.Linq;
using SuccincT.Functional;

namespace SuccincT.Options
{
    public static class OptionExtensions
    {
        public static Option<TOutput> Map<TInput, TOutput>(this Option<TInput> input, Func<TInput, TOutput> f) =>
            input.Match<Option<TOutput>>()
                 .Some().Do(x => f(x).Into(Option<TOutput>.Some))
                 .None().Do(Option<TOutput>.None())
                 .Result();

        public static Option<T> Or<T>(this Option<T> option, Option<T> anotherOption) => 
            option.HasValue ? option : anotherOption;

        public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> lazyAnotherOption) => 
            option.HasValue ? option : lazyAnotherOption();

        public static Option<T> Flatten<T>(this Option<Option<T>> option) =>
            option.Match<Option<T>>()
                  .Some().Do(x => x)
                  .None().Do(Option<T>.None())
                  .Result();

        public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> options) => 
            options.Where(x => x.HasValue).Select(x => x.Value);
    }
}