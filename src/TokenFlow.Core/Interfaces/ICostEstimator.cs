using TokenFlow.Core.Models;

namespace TokenFlow.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for estimating token-based usage costs.
    /// </summary>
    public interface ICostEstimator
    {
        /// <summary>
        /// Estimates the cost of input tokens for a given model.
        /// </summary>
        decimal EstimateInputCost(int inputTokens, ModelSpec model);

        /// <summary>
        /// Estimates the cost of output tokens for a given model.
        /// </summary>
        decimal EstimateOutputCost(int outputTokens, ModelSpec model);

        /// <summary>
        /// Estimates total cost based on token usage.
        /// </summary>
        decimal EstimateTotalCost(TokenCountResult counts, ModelSpec model);
    }
}
