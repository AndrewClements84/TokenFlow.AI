using BenchmarkDotNet.Attributes;
using TokenFlow.AI.Costing;
using TokenFlow.Core.Models;

namespace TokenFlow.Tools.Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class CostEstimatorBenchmarks
    {
        private readonly CostEstimator _estimator = new CostEstimator();
        private readonly ModelSpec _model = new ModelSpec("gpt-4o", "openai", "approx", 128000, 4096, 0.01m, 0.03m);

        [Params(100, 1000, 10000)]
        public int TokenCount { get; set; }

        [Benchmark]
        public decimal EstimateInputCost()
        {
            return _estimator.EstimateInputCost(TokenCount, _model);
        }

        [Benchmark]
        public decimal EstimateTotalCost()
        {
            var result = new TokenCountResult(TokenCount, TokenCount / 2, TokenCount + TokenCount / 2);
            return _estimator.EstimateTotalCost(result, _model);
        }

        [Benchmark]
        public decimal EstimateOutputCost()
        {
            return _estimator.EstimateOutputCost(TokenCount / 2, _model);
        }
    }
}

