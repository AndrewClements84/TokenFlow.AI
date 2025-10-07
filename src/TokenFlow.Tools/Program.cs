using TokenFlow.Tools.Commands;
using TokenFlow.AI.Registry;
using System;
using System.IO;

namespace TokenFlow.Tools
{
    /// <summary>
    /// Entry point for TokenFlow.Tools CLI.
    /// Supports commands: count, cost, chunk, analyze, compare, list-models.
    /// Adds --format, --input, and --output options for structured automation.
    /// </summary>
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            string command = args[0].ToLowerInvariant();
            string text = args.Length > 1 ? args[1] : string.Empty;

            string registryArg = GetArg(args, "--registry", "embedded");
            string format = GetArg(args, "--format", "table");
            string model = GetArg(args, "--model", "gpt-4o");
            string inputPath = GetArg(args, "--input", null);
            string outputPath = GetArg(args, "--output", null);

            // 🧩 Quiet mode toggle before anything else
            bool isQuiet = format.Equals("quiet", StringComparison.OrdinalIgnoreCase);
            if (isQuiet)
                Environment.SetEnvironmentVariable("TOKENFLOW_SILENT", "1");

            try
            {
                // === Input Handling ===
                if (!string.IsNullOrWhiteSpace(inputPath))
                {
                    if (!File.Exists(inputPath))
                    {
                        Console.WriteLine($"[TokenFlow.AI] Failed to read input file: File not found ({inputPath})");
                        return 1;
                    }

                    try
                    {
                        text = File.ReadAllText(inputPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TokenFlow.AI] Failed to read input file: {ex.Message}");
                        return 1;
                    }
                }

                // === Registry Creation ===
                IModelRegistry registry = CreateRegistry(registryArg);

                // === Command Dispatch ===
                return command switch
                {
                    "count" => CountCommand.Run(text, registry),
                    "cost" => CostCommand.Run(text, registry),
                    "chunk" => ChunkCommand.Run(text, registry),
                    "analyze" => AnalyzeCommand.Run(text, registry, format, model, outputPath),
                    "compare" => CompareCommand.Run(text, GetArg(args, "--models", "gpt-4o").Split(','), registry),
                    "list-models" => ListModelsCommand.Run(registry),
                    "help" => ShowHelp(),
                    _ => UnknownCommand(command)
                };
            }
            finally
            {
                // Always clear the quiet-mode flag
                if (isQuiet)
                    Environment.SetEnvironmentVariable("TOKENFLOW_SILENT", null);
            }
        }

        // === Helpers ===

        private static int ShowHelp()
        {
            PrintUsage();
            return 0;
        }

        private static int UnknownCommand(string cmd)
        {
            Console.WriteLine($"Unknown command: {cmd}");
            PrintUsage();
            return 1;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: tokenflow <command> <text> [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  count          Count tokens in text");
            Console.WriteLine("  cost           Estimate cost for text");
            Console.WriteLine("  chunk          Split text into token-sized chunks");
            Console.WriteLine("  analyze        Full analysis (tokens + cost)");
            Console.WriteLine("  compare        Compare token cost across models");
            Console.WriteLine("  list-models    List available models from registry");
            Console.WriteLine("  help           Show this usage information");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --model <id>        Specify model ID (default: gpt-4o)");
            Console.WriteLine("  --models <ids>      Comma-separated list for compare");
            Console.WriteLine("  --format <type>     Output format (table/json/csv/quiet)");
            Console.WriteLine("  --input <file>      Read input text from a file");
            Console.WriteLine("  --output <file>     Write results to a file");
            Console.WriteLine("  --registry <src>    Load registry (embedded/file/url)");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  tokenflow analyze \"Hello world\"");
            Console.WriteLine("  tokenflow analyze --input text.txt --format json --output result.json");
            Console.WriteLine("  tokenflow compare \"Sample\" --models gpt-4o,claude-3-opus");
            Console.WriteLine("  tokenflow list-models");
            Console.WriteLine("  tokenflow analyze \"Hello\" --format quiet  (suppress logs)");
        }

        private static string GetArg(string[] args, string name, string defaultValue)
        {
            int index = Array.IndexOf(args, name);
            return (index >= 0 && index + 1 < args.Length) ? args[index + 1] : defaultValue;
        }

        private static IModelRegistry CreateRegistry(string arg)
        {
            if (arg.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return new ModelRegistry(new Uri(arg), null, true);
            if (File.Exists(arg))
                return new ModelRegistry(arg);
            return new ModelRegistry(); // embedded fallback
        }
    }
}
