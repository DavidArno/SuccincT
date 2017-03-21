using SuccincT.Options;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Functional
{
    public static class EnumerableExtensions
    {
        public static IConsEnumerable<T> ToConsEnumerable<T>(this IEnumerable<T> collection) =>
            new ConsEnumerable<T>(collection);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, T head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection,
                                                 IEnumerable<T> head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IEnumerable<(int index, T item)> Indexed<T>(this IEnumerable<T> enumeration)
        {
            var index = 0;
            foreach (var item in enumeration)
            {
                yield return (index++, item);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        public static void Deconstruct<T>(this IEnumerable<T> enumeration,
                                          out Option<T> head,
                                          out IConsEnumerable<T> tail)
        {
            var consEnumeration = enumeration is ConsEnumerable<T> alreadyCons
                ? alreadyCons
                : new ConsEnumerable<T>(enumeration);

            (head, tail) = consEnumeration.TupleCons();
        }

    }
}
