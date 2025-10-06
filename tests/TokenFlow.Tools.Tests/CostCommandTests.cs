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

            Assert.Contains("Tokens:", output);
            Assert.Contains("Estimated cost", output);
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
    }
}


