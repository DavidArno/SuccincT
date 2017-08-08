using System.Collections.Generic;

namespace SuccincT.Functional
{
    public static class EnumerableExtensions
    {
        public static IConsEnumerable<T> ToConsEnumerable<T>(this IEnumerable<T> collection) =>
            new ConsEnumerable<T>(collection);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, T head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, IEnumerable<T> head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IEnumerable<(int index, T item)> Indexed<T>(this IEnumerable<T> enumeration)
        {
            var index = 0;
            foreach (var item in enumeration)
            {
                yield return (index++, item);
            }
        }

        public static void Deconstruct<T>(this IEnumerable<T> enumeration,
                                          out T head,
                                          out IConsEnumerable<T> tail) =>
            (head, tail) = enumeration is ConsEnumerable<T> alreadyCons
                ? alreadyCons.TupleCons()
                : new ConsEnumerable<T>(enumeration).TupleCons();

        public static T Head<T>(this IConsEnumerable<T> collection)
        {
            var (head, _) = collection;
            return head;
        }

        public static IConsEnumerable<T> ReplaceHead<T>(this IConsEnumerable<T> collection, T newHead)
        {
            var (_, tail) = collection;
            return tail.Cons(newHead);
        }
    }
}
