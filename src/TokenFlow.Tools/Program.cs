using System;
using TokenFlow.Tools.Commands;

namespace TokenFlow.Tools
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: tokenflow <command> [options]");
                Console.WriteLine("Commands: count, chunk, cost");
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var input = args.Length > 1 ? string.Join(" ", args, 1, args.Length - 1) : string.Empty;

            switch (command)
            {
                case "count":
                    return CountCommand.Run(input);
                case "chunk":
                    return ChunkCommand.Run(input);
                case "cost":
                    return CostCommand.Run(input);
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    return 1;
            }
        }
    }
}
