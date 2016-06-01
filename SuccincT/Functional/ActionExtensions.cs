using System;
using static SuccincT.Functional.Unit;

namespace SuccincT.Functional
{
    public static class ActionUnitFuncConversionExtensions
    {
        public static Func<Unit> ToUnitFunc(this Action action) =>
            () =>
            {
                action();
                return unit;
            };

        public static Func<T1, Unit> ToUnitFunc<T1>(this Action<T1> action) =>
            x =>
            {
                action(x);
                return unit;
            };

        public static Func<T1, T2, Unit> ToUnitFunc<T1, T2>(this Action<T1, T2> action) =>
            (x, y) =>
            {
                action(x, y);
                return unit;
            };

        public static Func<T1, T2, T3, Unit> ToUnitFunc<T1, T2, T3>(this Action<T1, T2, T3> action) =>
            (x, y, z) =>
            {
                action(x, y, z);
                return unit;
            };

        public static Func<T1, T2, T3, T4, Unit> ToUnitFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action) =>
            (w, x, y, z) =>
            {
                action(w, x, y, z);
                return unit;
            };

        public static Action ToAction(this Func<Unit> func) => () => Ignore(func);

        public static Action<T1> ToAction<T1>(this Func<T1, Unit> func) => x => Ignore(func(x));

        public static Action<T1, T2> ToAction<T1, T2>(this Func<T1, T2, Unit> func) => (x, y) => Ignore(func(x, y));

        public static Action<T1, T2, T3> ToAction<T1, T2, T3>(this Func<T1, T2, T3, Unit> func) =>
            (x, y, z) => Ignore(func(x, y, z));

        public static Action<T1, T2, T3, T4> ToAction<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Unit> func) =>
            (w, x, y, z) => Ignore(func(w, x, y, z));
    }
}
