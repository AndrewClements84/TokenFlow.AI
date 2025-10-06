namespace TokenFlow.AI.Tracking
{
    /// <summary>
    /// Represents an immutable summary of total token usage and cost.
    /// </summary>
    public class UsageSummary
    {
        public int AnalysisCount { get; private set; }
        public int TotalTokens { get; private set; }
        public decimal TotalCost { get; private set; }
        public string ModelId { get; private set; }

        public UsageSummary(int analysisCount, int totalTokens, decimal totalCost, string modelId)
        {
            AnalysisCount = analysisCount;
            TotalTokens = totalTokens;
            TotalCost = totalCost;
            ModelId = modelId;
        }
    }
}

