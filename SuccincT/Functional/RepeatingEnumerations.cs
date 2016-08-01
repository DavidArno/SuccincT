using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Functional
{
    public static class RepeatingEnumerations
    {
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> collection)
        {
            //var list = collection as IList<T>;
            //IList<T> cache = new List<T>();
            //if (list == null)
            //{
            //    using (var enumerator = collection.GetEnumerator())
            //    {
            //        while (enumerator.MoveNext())
            //        {
            //            cache.Add(enumerator.Current);
            //            yield return enumerator.Current;
            //        }
            //    }
            //}
            //else
            //{
            //    cache = list;
            //}

            //if (cache.Count == 0) yield break;

            var enumerationContainsItems = false;
            var consEnumeration = collection.ToConsEnumerable();
            while (true)
            {
                foreach (var item in consEnumeration)
                {
                    yield return item;
                    enumerationContainsItems = true;
                }

                if (!enumerationContainsItems) yield break;
            }
        }

        [SuppressMessage("ReSharper", "IteratorNeverReturns")]
        public static IEnumerable<T> Cycle<T>(params T[] collection)
        {
            while (true)
            {
                foreach (var item in collection)
                {
                    yield return item;
                }
            }
        }
    }
}
