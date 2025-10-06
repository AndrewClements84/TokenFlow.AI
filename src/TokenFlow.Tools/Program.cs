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
                Console.WriteLine("Usage: tokenflow <command> <text> [--registry <source>]");
                Console.WriteLine("Commands: count, cost, chunk");
                return 1;
            }

            string command = args[0].ToLowerInvariant();
            string text = args.Length > 1 ? args[1] : string.Empty;

            // default registry setup etc.
            string registryArg = "embedded";
            int registryIndex = Array.IndexOf(args, "--registry");
            if (registryIndex >= 0 && registryIndex + 1 < args.Length)
                registryArg = args[registryIndex + 1];

            IModelRegistry registry = CreateRegistry(registryArg);

            switch (command)
            {
                case "count":
                    return CountCommand.Run(text, registry);
                case "cost":
                    return CostCommand.Run(text, registry);
                case "chunk":
                    return ChunkCommand.Run(text, registry);
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    return 1;
            }
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
