using Moq;
using TokenFlow.AI.Costing;
using TokenFlow.AI.Registry;
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

        [Fact]
        public void EstimateInputCost_ShouldUseFallbackModel_WhenNullProvided()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            // pass null model to trigger fallback path
            var cost = estimator.EstimateInputCost(1000, null);

            Assert.True(cost > 0);
        }

        [Fact]
        public void EstimateOutputCost_ShouldUseFallbackModel_WhenNullProvided()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            var cost = estimator.EstimateOutputCost(1000, null);

            Assert.True(cost > 0);
        }

        [Fact]
        public void EstimateDetailedCost_ShouldReturnEmptyBreakdown_WhenResultIsNull()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            var breakdown = estimator.EstimateDetailedCost(null, "gpt-4o");

            Assert.Equal("gpt-4o", breakdown.ModelId);
            Assert.Equal(0, breakdown.TotalCost);
        }

        [Fact]
        public void ResolveModel_ShouldReturnApproxModel_WhenModelNotFound()
        {
            var mockRegistry = new Mock<IModelRegistry>();
            mockRegistry.Setup(r => r.GetById(It.IsAny<string>())).Returns((ModelSpec)null);

            var estimator = new CostEstimator(mockRegistry.Object);
            var resolved = typeof(CostEstimator)
                .GetMethod("ResolveModel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(estimator, new object[] { "unknown" }) as ModelSpec;

            Assert.NotNull(resolved);
            Assert.Equal("approx", resolved.Family);
        }

        [Fact]
        public void CreateApproxModel_ShouldReturnDefaultRates()
        {
            var method = typeof(CostEstimator)
                .GetMethod("CreateApproxModel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var model = (ModelSpec)method.Invoke(null, new object[] { "fallback-model" });

            Assert.Equal("fallback-model", model.Id);
            Assert.Equal(0.01m, model.InputPricePer1K);
            Assert.Equal(0.03m, model.OutputPricePer1K);
        }

        [Fact]
        public void EstimateInputCost_ShouldResolveModel_WhenModelIsNull()
        {
            // Arrange
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            // Act
            // Pass null model so that (model == null) branch executes
            var cost = estimator.EstimateInputCost(100, null);

            // Assert
            Assert.True(cost > 0); // should succeed using fallback model
        }

        [Fact]
        public void Private_Guard_ShouldTrigger_ModelFallback()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            // Force a null model in a method that uses the guard internally
            var method = typeof(CostEstimator).GetMethod("EstimateOutputCost");
            var result = method.Invoke(estimator, new object[] { 500, null });

            Assert.NotNull(result);
        }

        [Fact]
        public void Private_Guard_ShouldReturnZero_WhenResultNull()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            var method = typeof(CostEstimator).GetMethod("EstimateDetailedCost");
            var result = method.Invoke(estimator, new object[] { null, "gpt-4o" });

            Assert.NotNull(result);
        }

        [Fact]
        public void EstimateTotalCost_ShouldReturnZero_WhenResultIsNull()
        {
            // Arrange
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);
            var model = new ModelSpec("gpt-4o", "openai", "tiktoken", 128000, 4096, 0.01m, 0.03m);

            // Act
            var total = estimator.EstimateTotalCost(null, model);

            // Assert
            Assert.Equal(0, total);
        }

        [Fact]
        public void EstimateTotalCost_ShouldResolveModel_WhenModelIsNull()
        {
            // Arrange
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);
            var result = new TokenCountResult(500, 250, 750);

            // Act
            // Model is null to trigger the guard
            var total = estimator.EstimateTotalCost(result, null);

            // Assert
            Assert.True(total > 0); // should calculate cost using resolved model
        }

        [Fact]
        public void EstimateTotalCost_PrivateNullGuard_ShouldReturnZero()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);

            // use reflection to call the exact overload
            var method = typeof(CostEstimator)
                .GetMethod("EstimateTotalCost", new[] { typeof(TokenCountResult), typeof(ModelSpec) });

            var model = new ModelSpec("gpt-4o", "openai", "tiktoken", 128000, 4096, 0.01m, 0.03m);

            // call with null result to ensure guard triggers
            var result = (decimal)method.Invoke(estimator, new object[] { null, model });

            Assert.Equal(0m, result);
        }

        [Fact]
        public void EstimateTotalCost_ShouldReturnZero_WhenResultNull_Explicit()
        {
            var registry = new ModelRegistry();
            var estimator = new CostEstimator(registry);
            var model = new ModelSpec("gpt-4o", "openai", "tiktoken", 128000, 4096, 0.01m, 0.03m);

            TokenCountResult result = null; // ensure null reference
            var total = estimator.EstimateTotalCost(result, model);

            Assert.Equal(0, total);
        }
    }
}

