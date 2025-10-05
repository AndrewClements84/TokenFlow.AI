using TokenFlow.AI.Costing;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Costing
{
    public class CostEstimatorTests
    {
        [Fact]
        public void EstimateInputCost_ShouldReturnExpectedValue()
        {
            var estimator = new CostEstimator();
            var model = new ModelSpec("test", "mock", "approx", 1000, null, 0.02m, 0.04m);

            var cost = estimator.EstimateInputCost(1000, model);
            Assert.Equal(0.02m, cost);
        }

        [Fact]
        public void EstimateOutputCost_ShouldReturnExpectedValue()
        {
            var estimator = new CostEstimator();
            var model = new ModelSpec("test", "mock", "approx", 1000, null, 0.02m, 0.04m);

            var cost = estimator.EstimateOutputCost(2000, model);
            Assert.Equal(0.08m, cost);
        }

        [Fact]
        public void EstimateTotalCost_ShouldAddInputAndOutputCosts()
        {
            var estimator = new CostEstimator();
            var model = new ModelSpec("test", "mock", "approx", 1000, null, 0.02m, 0.04m);
            var counts = new TokenCountResult(1000, 500, 1500);

            var total = estimator.EstimateTotalCost(counts, model);
            Assert.Equal(0.02m + 0.02m, total);
        }
    }
}

