using System;
using TokenFlow.AI.Client;
using TokenFlow.AI.Registry;

namespace TokenFlow.Tools.Commands
{
    public static class CompareCommand
    {
        public static int Run(string text, string[] models, IModelRegistry registry = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to compare models with.");
                return 1;
            }

            if (models == null || models.Length == 0)
            {
                Console.WriteLine("Please specify at least one model (comma-separated).");
                return 1;
            }

            registry ??= new ModelRegistry();

            Console.WriteLine($"[TokenFlow.AI] Comparing {models.Length} models...");
            Console.WriteLine("------------------------------------------------");

            foreach (var model in models)
            {
                try
                {
                    var client = new TokenFlowClient(model.Trim());
                    var result = client.AnalyzeText(text);
                    Console.WriteLine($"{model.Trim(),-15} | Tokens: {result.TokenCount,6} | Cost: £{result.EstimatedCost,8:F4}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{model.Trim(),-15} | ERROR: {ex.Message}");
                }
            }

            Console.WriteLine("------------------------------------------------");
            return 0;
        }
    }
}
