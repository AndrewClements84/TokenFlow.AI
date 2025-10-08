using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;

namespace TokenFlow.Tools.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .AddExporter(JsonExporter.Full)
                .AddExporter(MarkdownExporter.GitHub)
                .AddExporter(HtmlExporter.Default)
                .AddLogger(ConsoleLogger.Default)
                .WithArtifactsPath("benchmark-results/results");

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}



