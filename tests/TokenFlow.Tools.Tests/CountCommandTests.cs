using TokenFlow.Tools.Commands;
using TokenFlow.Tools.Tests.Helpers;

namespace TokenFlow.Tools.Tests
{
    public class CountCommandTests
    {
        [Fact]
        public void Run_ShouldReturnSuccess_AndPrintTokenCount()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CountCommand.Run("Hello world");
                Assert.Equal(0, result);
            });

            Assert.Contains("Tokens:", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForEmptyInput()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CountCommand.Run("");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForWhitespace()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = CountCommand.Run("   ");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldPrintRegistrySource()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = CountCommand.Run("Hello world!");
                Assert.Equal(0, result);
            });

            Assert.Contains("[TokenFlow.AI] Using model registry source:", output);
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
