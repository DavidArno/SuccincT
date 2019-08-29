using System;
using static SuccincT.Functional.Unit;

namespace SuccincT.Functional
{
    public static class ActionUnitFuncConversionExtensions
    {
        public static Func<Unit> ToUnitFunc(this Action action) => () => { action(); return unit; };

        public static Func<T1, Unit> ToUnitFunc<T1>(this Action<T1> action) => x => { action(x); return unit; };

        public static Func<T1, T2, Unit> ToUnitFunc<T1, T2>(this Action<T1, T2> action) =>
            (x, y) => { action(x, y); return unit; };

        public static Func<T1, T2, T3, Unit> ToUnitFunc<T1, T2, T3>(this Action<T1, T2, T3> action) =>
            (x, y, z) => { action(x, y, z); return unit; };

        public static Func<T1, T2, T3, T4, Unit> ToUnitFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action) =>
            (w, x, y, z) => { action(w, x, y, z); return unit; };

        internal static Func<TResult> ToUnitFuncCastAs<TResult>(this Action action)
            => () =>
            {
                action();
                return unit is TResult result
                    ? result
                    : throw new InvalidCastException($"Cannot cast unit to {typeof(TResult)}");
            };

        internal static Func<T, TResult> ToUnitFuncCastAs<T, TResult>(this Action<T> action)
            => x =>
            {
                action(x);
                return unit is TResult result
                    ? result
                    : throw new InvalidCastException($"Cannot cast unit to {typeof(TResult)}");
            };

    }
}
