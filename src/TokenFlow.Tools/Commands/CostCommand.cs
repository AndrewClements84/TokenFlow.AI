using System;
using TokenFlow.AI.Costing;
using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;
using TokenFlow.Tools.Utilities;

namespace TokenFlow.Tools.Commands
{
    public static class CostCommand
    {
        // === Backward-compatible overload ===
        public static int Run(string text, IModelRegistry registry)
            => Run(text, "gpt-4o", registry);

        // === New extended overload with modelId ===
        public static int Run(string text, string modelId = "gpt-4o", IModelRegistry registry = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to estimate cost for.");
                return 1;
            }

            registry ??= new ModelRegistry();
            Console.WriteLine($"[TokenFlow.AI] Using model registry source: {registry.LoadSource}");

            var model = registry.GetById(modelId)
                ?? new ModelSpec(modelId, "approx", "approx", 128000, 4096, 0.01m, 0.03m);

            var tokenizer = new TokenFlow.Tokenizers.Factory.TokenizerFactory().Create(model.TokenizerName);
            int tokenCount = tokenizer.CountTokens(text);

            var estimator = new CostEstimator(registry);
            var result = new TokenCountResult(tokenCount, tokenCount / 2, tokenCount + tokenCount / 2);
            var breakdown = estimator.EstimateDetailedCost(result, model.Id);

            Console.WriteLine();
            OutputFormatter.Write(breakdown, "table");
            Console.WriteLine();
            return 0;
        }
    }
}
