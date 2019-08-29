using SuccincT.Functional;
using System;

namespace SuccincT.Utilities
{
    internal static class NRTSupport
    {
        internal static int GetItemHashCode<T>(T item) => item is {} notNullItem ? notNullItem.GetHashCode() : 0;

        internal static T ItemAs<T>(object? obj)
            => obj is T cast ? cast : throw new InvalidOperationException($"Failed to cast item to type {typeof(T)}");

        internal static bool SameType<T1, T2>() => typeof(T1) == typeof(T2);

        internal static bool ComparePossibleNullValues<T>(T a, T b)
            => a is { } value ? value.Equals(b) : b is null;

        public static Func<T, TResult> ToFuncOf<T, TResult>(this Func<T, Unit> func)
            => x => func(x) is TResult result
                ? result
                : throw new InvalidCastException($"Cannot convert Unit to type {typeof(TResult)}");
    }
}
