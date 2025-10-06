using System.Collections.Generic;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tracking
{
    /// <summary>
    /// Tracks cumulative token and cost usage across multiple analyses.
    /// </summary>
    public class TokenUsageTracker
    {
        private readonly ModelSpec _model;
        private readonly List<TokenAnalysisResult> _records;

        public TokenUsageTracker(ModelSpec model)
        {
            _model = model;
            _records = new List<TokenAnalysisResult>();
        }

        /// <summary>
        /// Records a single analysis result into the tracker.
        /// </summary>
        public void Record(TokenAnalysisResult result)
        {
            if (result == null)
                return;

            _records.Add(result);
        }

        /// <summary>
        /// Returns an aggregated summary of all recorded analyses.
        /// </summary>
        public UsageSummary GetSummary()
        {
            int totalTokens = 0;
            decimal totalCost = 0m;

            for (int i = 0; i < _records.Count; i++)
            {
                var r = _records[i];
                totalTokens += r.TokenCount;
                totalCost += r.EstimatedCost;
            }

            return new UsageSummary(_records.Count, totalTokens, totalCost, _model.Id);
        }

        /// <summary>
        /// Clears all tracked analysis records.
        /// </summary>
        public void Reset()
        {
            _records.Clear();
        }
    }
}

