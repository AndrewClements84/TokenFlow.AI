using System;
using System.Text.Json;
using TokenFlow.AI.Client;
using TokenFlow.AI.Registry;

namespace TokenFlow.Tools.Commands
{
    public static class AnalyzeCommand
    {
        public static int Run(string text, IModelRegistry registry = null, string format = "table", string modelId = "gpt-4o")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to analyze.");
                return 1;
            }

            registry ??= new ModelRegistry();
            var client = new TokenFlowClient(modelId);
            var result = client.AnalyzeText(text);

            if (format.Equals("json", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
                return 0;
            }

            Console.WriteLine($"Model: {result.ModelId}");
            Console.WriteLine($"Tokens: {result.TokenCount}");
            Console.WriteLine($"Estimated cost: £{result.EstimatedCost:F4}");
            return 0;
        }
    }
}

