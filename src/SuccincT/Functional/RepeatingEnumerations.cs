using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SuccincT.Functional
{
    public static class RepeatingEnumerations
    {
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> collection)
        {
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
