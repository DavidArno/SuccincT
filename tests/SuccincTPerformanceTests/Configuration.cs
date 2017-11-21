using System;
using System.Collections.Generic;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

namespace SuccincTPerformanceTests
{
    internal class Configuration : IConfig
    {
        private readonly IConfig _defaultConfig = DefaultConfig.Instance;

        public IEnumerable<IColumnProvider> GetColumnProviders() => _defaultConfig.GetColumnProviders();

        public IEnumerable<IExporter> GetExporters() { yield return AsciiDocExporter.Default; }

        public IEnumerable<ILogger> GetLoggers() { yield return new Logger(); }

        public IEnumerable<IDiagnoser> GetDiagnosers() => _defaultConfig.GetDiagnosers();

        public IEnumerable<IAnalyser> GetAnalysers() => _defaultConfig.GetAnalysers();

        public IEnumerable<Job> GetJobs() => _defaultConfig.GetJobs();

        public IEnumerable<IValidator> GetValidators() => _defaultConfig.GetValidators();

        public IEnumerable<HardwareCounter> GetHardwareCounters() => _defaultConfig.GetHardwareCounters();

        public IEnumerable<IFilter> GetFilters() => _defaultConfig.GetFilters();

        public IOrderProvider GetOrderProvider() => _defaultConfig.GetOrderProvider();

        public ISummaryStyle GetSummaryStyle() => _defaultConfig.GetSummaryStyle();

        public ConfigUnionRule UnionRule => _defaultConfig.UnionRule;

        public bool KeepBenchmarkFiles => true;

        private class Logger : ILogger
        {
            private readonly ILogger _logger = new ConsoleLogger(CreateColorScheme());

            public void Write(LogKind logKind, string text) => _logger.Write(logKind, $"{logKind.ToString()} - {text}");
            public void WriteLine() => _logger.WriteLine();
            public void WriteLine(LogKind logKind, string text) => _logger.WriteLine(logKind, 
                                                                                     $"{logKind.ToString()} - {text}");
        }

        private static Dictionary<LogKind, ConsoleColor> CreateColorScheme() =>
            new Dictionary<LogKind, ConsoleColor>
            {
                { LogKind.Default, ConsoleColor.Cyan },
                { LogKind.Help, ConsoleColor.Cyan },
                { LogKind.Header, ConsoleColor.Cyan },
                { LogKind.Result, ConsoleColor.Cyan },
                { LogKind.Statistic, ConsoleColor.Cyan },
                { LogKind.Info, ConsoleColor.Cyan },
                { LogKind.Error, ConsoleColor.Cyan },
                { LogKind.Hint, ConsoleColor.Cyan }
            };
    }
}
