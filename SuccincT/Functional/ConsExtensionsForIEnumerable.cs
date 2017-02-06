using System.Collections.Generic;

namespace SuccincT.Functional
{
    public static class ConsExtensionsForIEnumerable
    {
        public static IConsEnumerable<T> ToConsEnumerable<T>(this IEnumerable<T> collection) =>
            new ConsEnumerable<T>(collection);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, T head) =>
            collection.ToConsEnumerable().Cons(head);

        public static IConsEnumerable<T> Cons<T>(this IEnumerable<T> collection, IEnumerable<T> head) =>
            collection.ToConsEnumerable().Cons(head);

        public static ConsResult<T> Cons<T>(this IEnumerable<T> collection) => collection.ToConsEnumerable().Cons();
    }
}
