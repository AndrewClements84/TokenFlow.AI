using TokenFlow.AI.Tracking;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Tracking
{
    public class TokenUsageTrackerTests
    {
        private readonly ModelSpec _model = new ModelSpec("gpt-4o", "openai", "approx", 128000, 4096, 0.01m, 0.03m);

        [Fact]
        public void Record_ShouldAdd_AnalysisResult()
        {
            var tracker = new TokenUsageTracker(_model);
            var result = new TokenAnalysisResult(10, 0.05m, "gpt-4o");

            tracker.Record(result);
            var summary = tracker.GetSummary();

            Assert.Equal(1, summary.AnalysisCount);
            Assert.Equal(10, summary.TotalTokens);
            Assert.Equal(0.05m, summary.TotalCost);
        }

        [Fact]
        public void Record_ShouldIgnore_NullResult()
        {
            var tracker = new TokenUsageTracker(_model);
            tracker.Record(null);

            var summary = tracker.GetSummary();

            Assert.Equal(0, summary.AnalysisCount);
            Assert.Equal(0, summary.TotalTokens);
            Assert.Equal(0m, summary.TotalCost);
        }

        [Fact]
        public void GetSummary_ShouldAggregate_MultipleResults()
        {
            var tracker = new TokenUsageTracker(_model);
            tracker.Record(new TokenAnalysisResult(5, 0.02m, "gpt-4o"));
            tracker.Record(new TokenAnalysisResult(7, 0.03m, "gpt-4o"));

            var summary = tracker.GetSummary();

            Assert.Equal(2, summary.AnalysisCount);
            Assert.Equal(12, summary.TotalTokens);
            Assert.Equal(0.05m, summary.TotalCost);
        }

        [Fact]
        public void Reset_ShouldClear_Records()
        {
            var tracker = new TokenUsageTracker(_model);
            tracker.Record(new TokenAnalysisResult(8, 0.04m, "gpt-4o"));
            tracker.Reset();

            var summary = tracker.GetSummary();

            Assert.Equal(0, summary.AnalysisCount);
            Assert.Equal(0, summary.TotalTokens);
            Assert.Equal(0m, summary.TotalCost);
        }

        [Fact]
        public void GetSummary_ShouldRetain_ModelId()
        {
            var tracker = new TokenUsageTracker(_model);
            tracker.Record(new TokenAnalysisResult(3, 0.01m, "gpt-4o"));
            var summary = tracker.GetSummary();

            Assert.Equal("gpt-4o", summary.ModelId);
        }
    }
}

