using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.Options
{
    public static class FirstOrNoneExtensions
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> collection) => collection.FirstOrNone(_ => true);

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> collection, Func<T, bool> matchElement)
        {
            try
            {
                return Option<T>.Some(collection.First(matchElement));
            }
            catch (Exception ex) when (ex.GetType() == typeof(ArgumentNullException) ||
                                       ex.GetType() == typeof(InvalidOperationException))
            {
                return Option<T>.None();
            }
        }
    }
}