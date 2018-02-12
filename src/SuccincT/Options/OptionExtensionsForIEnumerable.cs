using System;
using System.Collections.Generic;

namespace SuccincT.Options
{
    //
    // These methods are all based on the XxxOrDefault methods in
    // http://referencesource.microsoft.com/#System.Core/System/Linq/Enumerable.cs
    // I have followed the logic structure of each method there, on the assumption that it gives the fastest possible
    // result in each case, rather than write more readable/functional code.
    //
    public static class OptionExtensionsForIEnumerable
    {
        public static Option<T> TryFirst<T>(this IEnumerable<T> collection)
        {
            switch (collection)
            {
                case null: return Option<T>.None();
                case IList<T> list when list.Count > 0: return Option<T>.Some(list[0]);
            }

            using (var enumerator = collection.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return Option<T>.Some(enumerator.Current);
                }
            }

            return Option<T>.None();
        }

        public static Option<T> TryFirst<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            if (collection == null) return Option<T>.None();

            foreach (var element in collection)
            {
                if (predicate(element)) { return Option<T>.Some(element); }
            }

            return Option<T>.None();
        }

        public static Option<T> TryLast<T>(this IEnumerable<T> collection)
        {
            switch (collection)
            {
                case null: return Option<T>.None();
                case IList<T> list when list.Count > 0: return Option<T>.Some(list[list.Count - 1]);
            }

            using (var e = collection.GetEnumerator())
            {
                if (!e.MoveNext()) return Option<T>.None();

                T result;
                do
                {
                    result = e.Current;
                } while (e.MoveNext());
                return Option<T>.Some(result);
            }
        }

        public static Option<T> TryLast<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            var result = Option<T>.None();
            if (collection == null) return result;

            foreach (var element in collection)
            {
                if (predicate(element)) { result = Option<T>.Some(element); }
            }

            return result;
        }

        public static Option<T> TrySingle<T>(this IEnumerable<T> collection)
        {
            switch (collection)
            {
                case null: return Option<T>.None();
                case IList<T> list: return list.Count == 1 ? Option<T>.Some(list[0]) : Option<T>.None();
            }

            using (var enumerator = collection.GetEnumerator())
            {
                if (!enumerator.MoveNext()) return Option<T>.None();

                var result = enumerator.Current;
                return !enumerator.MoveNext() ? Option<T>.Some(result) : Option<T>.None();
            }
        }

        public static Option<T> TrySingle<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var result = Option<T>.None();
            var count = 0;
            if (collection == null) return result;

            foreach (var element in collection)
            {
                if (!predicate(element)) continue;
                if (++count > 1) break;

                result = Option<T>.Some(element);
            }

            return count == 1 ? result : Option<T>.None();
        }

        public static Option<T> TryElementAt<T>(this IEnumerable<T> collection, int index)
        {
            if (collection == null || index < 0) return Option<T>.None();

            if (collection is IList<T> list)
            {
                return index < list.Count ? Option<T>.Some(list[index]) : Option<T>.None();
            }

            using (var enumerator = collection.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext()) break;
                    if (index-- == 0) { return Option<T>.Some(enumerator.Current); }
                }
            }

            return Option<T>.None();
        }
    }
}