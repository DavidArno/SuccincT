using System.Collections.Generic;

namespace SuccincT.Functional
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(int index, T item)> Indexed<T>(this IEnumerable<T> enumeration)
        {
            var index = 0;
            foreach (var item in enumeration)
            {
                yield return (index++, item);
            }
        }
    }
}
