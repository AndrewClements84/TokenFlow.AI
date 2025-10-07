using TokenFlow.Tools.Commands;
using TokenFlow.AI.Registry;
using System;

namespace TokenFlow.Tools
{
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

            IModelRegistry registry = CreateRegistry(registryArg);

            switch (command)
            {
                case "count": return CountCommand.Run(text, registry);
                case "cost": return CostCommand.Run(text, registry);
                case "chunk": return ChunkCommand.Run(text, registry);
                case "analyze": return AnalyzeCommand.Run(text, registry, format, model);
                case "compare": return CompareCommand.Run(text, GetArg(args, "--models", "gpt-4o").Split(','), registry);
                case "list-models": return ListModelsCommand.Run(registry);
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    PrintUsage();
                    return 1;
            }
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
            Console.WriteLine("  compare        Compare token cost across multiple models");
            Console.WriteLine("  list-models    List available models from registry");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --model <id>        Specify model ID (default: gpt-4o)");
            Console.WriteLine("  --models <ids>      Comma-separated list for compare (default: gpt-4o)");
            Console.WriteLine("  --format <type>     Output format (table/json)");
            Console.WriteLine("  --registry <src>    Load registry (embedded/file/url)");
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
            if (System.IO.File.Exists(arg))
                return new ModelRegistry(arg);
            return new ModelRegistry(); // embedded fallback
        }
    }
}
