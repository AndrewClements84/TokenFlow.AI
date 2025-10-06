using System;
using TokenFlow.AI.Client;
using TokenFlow.AI.Registry;

namespace TokenFlow.Tools.Commands
{
    public static class CostCommand
    {
        public static int Run(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to estimate cost for.");
                return 1;
            }

            var registry = new ModelRegistry();
            Console.WriteLine($"[TokenFlow.AI] Using model registry source: {registry.LoadSource}");

            var client = new TokenFlowClient("gpt-4o");
            var result = client.AnalyzeText(text);

            Console.WriteLine($"Tokens: {result.TokenCount}");
            Console.WriteLine($"Estimated cost: £{result.EstimatedCost:F4}");
            return 0;
        }
    }
}


