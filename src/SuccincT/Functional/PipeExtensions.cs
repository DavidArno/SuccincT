using System;

namespace SuccincT.Functional
{
    public static class PipeExtensions
    {
        public static TR Into<T, TR>(this T value, Func<T, TR > func) => func(value);
    }
}
