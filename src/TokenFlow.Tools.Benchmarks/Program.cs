using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Loggers;

namespace TokenFlow.Tools.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                // Always export machine-readable JSON for CI comparisons
                .AddExporter(JsonExporter.Full)
                // Nice human-readable reports (optional, harmless if also set via attributes)
                .AddExporter(MarkdownExporter.GitHub)
                .AddExporter(HtmlExporter.Default)
                // CI-friendly console logs
                .AddLogger(ConsoleLogger.Default)
                // Put all artifacts in a predictable folder for CI
                .WithArtifactsPath("benchmark-results/results");

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}


