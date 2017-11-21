using BenchmarkDotNet.Attributes;
using SuccincT.Unions;
using System.Collections.Generic;

namespace SuccincTPerformanceTests
{
    [MemoryDiagnoser]
    public class UnionT1T2Benchmarks
    {
        private const int Count = 1_000;

        [Benchmark]
        public void UnionT1T2Benchmark()
        {
            var unions = new List<Union<int, string>>();

            for (var i = 0; i < Count; ++i)
            {
                unions.Add(new Union<int, string>(1));
            }

            _ = unions;
        }
    }
}
