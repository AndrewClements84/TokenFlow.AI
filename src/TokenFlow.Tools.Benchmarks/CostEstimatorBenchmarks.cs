using BenchmarkDotNet.Attributes;
using TokenFlow.AI.Costing;
using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    [MarkdownExporter, HtmlExporter]
    public class CostEstimatorBenchmarks
    {
        private readonly CostEstimator _estimator = new CostEstimator();
        private readonly ModelSpec _model = new ModelSpec(
            "gpt-4o",
            "openai",
            "approx",
            128000,
            4096,
            0.01m,
            0.03m);

        [Params(100, 1000, 10000)]
        public int TokenCount { get; set; }

        [Benchmark(Description = "Estimate input token cost")]
        public decimal EstimateInputCost()
        {
            return _estimator.EstimateInputCost(TokenCount, _model);
        }

        [Benchmark(Description = "Estimate total combined cost")]
        public decimal EstimateTotalCost()
        {
            var result = new TokenCountResult(TokenCount, TokenCount / 2, TokenCount + TokenCount / 2);
            return _estimator.EstimateTotalCost(result, _model);
        }

        [Benchmark(Description = "Estimate output token cost")]
        public decimal EstimateOutputCost()
        {
            return _estimator.EstimateOutputCost(TokenCount / 2, _model);
        }

        [Benchmark(Description = "Estimate cost from registry (CLI mode)")]
        public decimal EstimateCost_FromRegistry()
        {
            var registry = new ModelRegistry();
            var model = registry.GetById("gpt-4o");
            var result = new TokenCountResult(1000, 500, 1500);
            var estimator = new CostEstimator(registry);
            return estimator.EstimateTotalCost(result, model);
        }
    }
}
