namespace TokenFlow.AI
{
    /// <summary>
    /// Represents the result of a token and cost analysis.
    /// </summary>
    public class TokenAnalysisResult
    {
        public int TokenCount { get; private set; }
        public decimal EstimatedCost { get; private set; }
        public string ModelId { get; private set; }

        public TokenAnalysisResult(int tokenCount, decimal estimatedCost, string modelId)
        {
            TokenCount = tokenCount;
            EstimatedCost = estimatedCost;
            ModelId = modelId;
        }
    }
}

