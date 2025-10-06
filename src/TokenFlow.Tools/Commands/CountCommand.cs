using System;
using TokenFlow.AI.Client;
using TokenFlow.AI.Registry;

namespace TokenFlow.Tools.Commands
{
    public static class CountCommand
    {
        public static int Run(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to count tokens for.");
                return 1;
            }

            var registry = new ModelRegistry(); // Loads embedded by default
            Console.WriteLine($"[TokenFlow.AI] Using model registry source: {registry.LoadSource}");

            var client = new TokenFlowClient("gpt-4o");
            var result = client.AnalyzeText(text);

            Console.WriteLine($"Tokens: {result.TokenCount}");
            return 0;
        }
    }
}


