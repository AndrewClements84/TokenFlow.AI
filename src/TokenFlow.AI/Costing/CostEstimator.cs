using TokenFlow.AI.Registry;
using TokenFlow.Core.Interfaces;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Costing
{
    /// <summary>
    /// Provides cost estimation for language models, dynamically using registry data
    /// from Flow.AI.Core or embedded model definitions.
    /// </summary>
    public class CostEstimator : ICostEstimator
    {
        private readonly IModelRegistry _registry;

        public CostEstimator(IModelRegistry registry = null)
        {
            // Fallback to embedded local registry if none provided
            _registry = registry ?? new ModelRegistry();
        }

        public decimal EstimateInputCost(int inputTokens, ModelSpec model)
        {
            if (model == null) model = ResolveModel("gpt-4o");
            return CalculateCost(inputTokens, model.InputPricePer1K);
        }

        public decimal EstimateOutputCost(int outputTokens, ModelSpec model)
        {
            if (model == null) model = ResolveModel("gpt-4o");
            return CalculateCost(outputTokens, model.OutputPricePer1K);
        }

        public decimal EstimateTotalCost(TokenCountResult result, ModelSpec model)
        {
            if (result == null) return 0;
            if (model == null) model = ResolveModel("gpt-4o");

            var inputCost = EstimateInputCost(result.PromptTokens, model);
            var outputCost = EstimateOutputCost(result.CompletionTokens, model);
            return inputCost + outputCost;
        }

        /// <summary>
        /// Returns a detailed cost breakdown with input/output rates.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public CostBreakdownResult EstimateDetailedCost(TokenCountResult result, string modelId)
        {
            
            if (result == null)
                return new CostBreakdownResult { ModelId = modelId };

            var model = _registry.GetById(modelId) ?? CreateApproxModel(modelId);

            var inputCost = CalculateCost(result.PromptTokens, model.InputPricePer1K);
            var outputCost = CalculateCost(result.CompletionTokens, model.OutputPricePer1K);

            return new CostBreakdownResult
            {
                ModelId = model.Id,
                InputRatePer1K = model.InputPricePer1K,
                OutputRatePer1K = model.OutputPricePer1K,
                InputCost = inputCost,
                OutputCost = outputCost
            };
        }

        // === Helper methods ===

        private static decimal CalculateCost(int tokens, decimal ratePer1K)
        {
            if (tokens <= 0 || ratePer1K <= 0) return 0;
            return (tokens / 1000m) * ratePer1K;
        }

        private ModelSpec ResolveModel(string modelId)
        {
            return _registry.GetById(modelId) ?? CreateApproxModel(modelId);
        }

        private static ModelSpec CreateApproxModel(string id)
        {
            // Basic default fallback rates if model not found
            return new ModelSpec(id, "approx", "approx", 128000, 4096, 0.01m, 0.03m);
        }
    }
}
