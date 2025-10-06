using TokenFlow.Tools.Commands;

namespace TokenFlow.Tools.Tests
{
    public class CostCommandTests
    {
        [Fact]
        public void Run_ShouldReturnSuccess_AndPrintCost()
        {
            var output = CaptureConsoleOut(() =>
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
            var output = CaptureConsoleOut(() =>
            {
                int result = CostCommand.Run("");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForWhitespace()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = CostCommand.Run("   ");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        private static string CaptureConsoleOut(Action action)
        {
            var original = Console.Out;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                action();
                Console.SetOut(original);
                return sw.ToString();
            }
        }
    }
}


