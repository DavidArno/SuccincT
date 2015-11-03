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
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> collection)
        {
            if (collection != null)
            {
                var list = collection as IList<T>;
                if (list != null && list.Count > 0)
                {
                    return Option<T>.Some(list[0]);
                }

                using (var enumerator = collection.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return Option<T>.Some(enumerator.Current);
                    }
                }
            }

            return Option<T>.None();
        }

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            if (collection != null)
            {
                foreach (var element in collection)
                {
                    if (predicate(element)) { return Option<T>.Some(element); }
                }
            }

            return Option<T>.None();
        }

        public static Option<T> LastOrNone<T>(this IEnumerable<T> collection)
        {
            if (collection != null)
            {
                var list = collection as IList<T>;
                if (list != null && list.Count > 0)
                {
                    return Option<T>.Some(list[list.Count - 1]);
                }

                using (var e = collection.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        T result;
                        do
                        {
                            result = e.Current;
                        } while (e.MoveNext());
                        return Option<T>.Some(result);
                    }
                }
            }

            return Option<T>.None();
        }

        public static Option<T> LastOrNone<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            var result = Option<T>.None();
            if (collection != null)
            {
                foreach (var element in collection)
                {
                    if (predicate(element)) { result = Option<T>.Some(element); }
                }
            }

            return result;
        }

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> collection)
        {
            if (collection != null)
            {
                var list = collection as IList<T>;
                if (list != null)
                {
                    return list.Count == 1 ? Option<T>.Some(list[0]) : Option<T>.None();
                }

                using (var enumerator = collection.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var result = enumerator.Current;
                        return !enumerator.MoveNext() ? Option<T>.Some(result) : Option<T>.None();
                    }
                }
            }

            return Option<T>.None();
        }

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var result = Option<T>.None();
            var count = 0;
            if (collection != null)
            {
                foreach (var element in collection)
                {
                    if (predicate(element))
                    {
                        if (++count > 1) { break; }
                        result = Option<T>.Some(element);
                    }
                }
            }
            return count == 1 ? result : Option<T>.None();
        }
    }
}