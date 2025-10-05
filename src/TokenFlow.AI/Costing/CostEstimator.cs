using TokenFlow.Core.Interfaces;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Costing
{
    /// <summary>
    /// Provides basic cost estimation for LLM token usage.
    /// </summary>
    public class CostEstimator : ICostEstimator
    {
        public decimal EstimateInputCost(int inputTokens, ModelSpec model)
        {
            return (inputTokens / 1000m) * model.InputPricePer1K;
        }

        public decimal EstimateOutputCost(int outputTokens, ModelSpec model)
        {
            return (outputTokens / 1000m) * model.OutputPricePer1K;
        }

        public decimal EstimateTotalCost(TokenCountResult counts, ModelSpec model)
        {
            return EstimateInputCost(counts.PromptTokens, model)
                 + EstimateOutputCost(counts.CompletionTokens, model);
        }
    }
}

