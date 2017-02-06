using SuccincT.Options;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Functional
{
    public static class IEnumerableExtensions
    {
        public static IConsEnumerable<T> ToConsEnumerable<T>(this IEnumerable<T> collection) =>
            new ConsEnumerable<T>(collection);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, T head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, 
                                                 IEnumerable<T> head) =>
            collection.ToConsEnumerable().Cons(head);

        public static ConsResult<T> Cons<T>(this IEnumerable<T> collection) => 
            collection.ToConsEnumerable().Cons();

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
            var consEnumeration = enumeration is IConsEnumerable<T> alreadyCons
                ? alreadyCons
                : enumeration.ToConsEnumerable();

            var splitResult = consEnumeration.Cons();
            head = splitResult.Head;
            tail = splitResult.Tail;
        }

    }
}
