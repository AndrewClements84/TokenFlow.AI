using System;
using System.IO;
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
            var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "benchmark-results", "results");
            Directory.CreateDirectory(outputDir);

            Console.WriteLine($"[TokenFlow.Tools.Benchmarks] Writing results to: {outputDir}");

            var config = DefaultConfig.Instance
                .AddExporter(JsonExporter.Full)
                .AddExporter(MarkdownExporter.GitHub)
                .AddExporter(HtmlExporter.Default)
                .AddLogger(ConsoleLogger.Default)
                .WithArtifactsPath(outputDir);

            // ✅ Run all benchmarks explicitly to ensure discovery and export
            BenchmarkRunner.Run<TokenizerBenchmarks>(config);
            BenchmarkRunner.Run<ChunkerBenchmarks>(config);
            BenchmarkRunner.Run<CostEstimatorBenchmarks>(config);

            Console.WriteLine("[TokenFlow.Tools.Benchmarks] ✅ Benchmarks completed successfully.");
        }
    }
}
