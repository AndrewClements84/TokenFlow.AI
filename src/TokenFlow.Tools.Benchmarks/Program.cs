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
            // Always write results into the repo workspace, not a transient runner path
            var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "benchmark-results", "results");
            Directory.CreateDirectory(outputDir);

            Console.WriteLine($"[TokenFlow.Tools.Benchmarks] Output directory: {outputDir}");

            var config = DefaultConfig.Instance
                // Export full JSON reports for CI comparison
                .AddExporter(JsonExporter.Full)
                // Nice readable markdown and HTML summaries (optional)
                .AddExporter(MarkdownExporter.GitHub)
                .AddExporter(HtmlExporter.Default)
                // Log progress to console
                .AddLogger(ConsoleLogger.Default)
                // Force consistent artifact output path
                .WithArtifactsPath(outputDir);

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}




