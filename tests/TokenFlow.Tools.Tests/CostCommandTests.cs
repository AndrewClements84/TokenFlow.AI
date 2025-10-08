using Moq;
using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;
using TokenFlow.Tools.Commands;
using TokenFlow.Tools.Tests.Helpers;

namespace TokenFlow.Tools.Tests
{
    public class CostCommandTests
    {
        [Fact]
        public void Run_ShouldReturnSuccess_AndPrintCost()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("TokenFlow.AI makes cost estimation easy.");
                Assert.Equal(0, result);
            });

            Assert.Contains("ModelId", output);
            Assert.Contains("InputRatePer1K", output);
            Assert.Contains("OutputRatePer1K", output);
            Assert.Contains("TotalCost", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForEmptyInput()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForWhitespace()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("   ");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldUseInjectedRegistry()
        {
            var registry = new ModelRegistry();
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("Hello Flow", registry);
                Assert.Equal(0, result);
            });

            Assert.Contains("[TokenFlow.AI] Using model registry source:", output);
        }

        [Fact]
        public void Run_ShouldPrintCostBreakdown_WhenRegistryModelUsed()
        {
            var registry = new ModelRegistry();
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("Flow.AI cost estimation test.", "gpt-4o", registry);
                Assert.Equal(0, result);
            });

            Assert.Contains("ModelId", output);
            Assert.Contains("InputRatePer1K", output);
            Assert.Contains("OutputRatePer1K", output);
        }

        [Fact]
        public void Run_ShouldFallbackToApproxModel_WhenModelMissing()
        {
            var mockRegistry = new Mock<IModelRegistry>();
            mockRegistry.Setup(r => r.GetById(It.IsAny<string>())).Returns((ModelSpec)null);

            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CostCommand.Run("Fallback test input", "missing-model", mockRegistry.Object);
                Assert.Equal(0, result);
            });

            Assert.Contains("[TokenFlow.AI] Using model registry source", output);
        }

    }
}


