using BenchmarkDotNet.Running;
using System;
using System.IO;

namespace SuccincTPerformanceTests
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<UnionT1T2Benchmarks>(new Configuration());
//            File.AppendAllText("benchmark.log", BenchmarkRunner.Run<OptionBenchmarks>().ToString());
        }
    }
}
