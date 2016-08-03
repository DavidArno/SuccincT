using System;

namespace SuccincT.Functional
{
    public static class PipeExtensions
    {
        public static TR PipeTo<T, TR>(this T value, Func<T, TR > func) => func(value);
    }
}
