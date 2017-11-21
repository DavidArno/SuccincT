using BenchmarkDotNet.Attributes;
using SuccincT.Options;
using System.Collections.Generic;

namespace SuccincTPerformanceTests
{
    [MemoryDiagnoser]
    public class OptionBenchmarks
    {
        private const int Count = 1_000;

        [Benchmark]
        public void OptionCreationBenchmark()
        {
            var options = new List<Option<int>>();

            for (var i = 0; i < Count; ++i)
            {
                options.Add(1);
            }

            _ = options;
        }

        [Benchmark]
        public void OptionCopyBenchmark()
        {
            Option<int> option = 0;
            for (var i = 0; i < Count; ++i)
            {
                var x = CreateOption();
                option = x;
            }

            _ = option;
        }

        private static Option<int> CreateOption() => 1;
    }
}
