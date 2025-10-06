namespace TokenFlow.Tools.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_ShouldReturnError_WhenNoArguments()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = Program.Main(Array.Empty<string>());
                Assert.Equal(1, result);
            });

            Assert.Contains("Usage: tokenflow", output);
            Assert.Contains("Commands:", output);
        }

        [Fact]
        public void Main_ShouldReturnError_ForUnknownCommand()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = Program.Main(new[] { "unknown" });
                Assert.Equal(1, result);
            });

            Assert.Contains("Unknown command", output);
        }

        [Fact]
        public void Main_ShouldInvokeCountCommand()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = Program.Main(new[] { "count", "Hello world" });
                Assert.Equal(0, result);
            });

            Assert.Contains("Token count", output);
        }

        [Fact]
        public void Main_ShouldInvokeChunkCommand()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = Program.Main(new[] { "chunk", "TokenFlow is modular" });
                Assert.Equal(0, result);
            });

            Assert.Contains("Chunk 1:", output);
            Assert.Contains("Total chunks", output);
        }

        [Fact]
        public void Main_ShouldInvokeCostCommand()
        {
            var output = CaptureConsoleOut(() =>
            {
                int result = Program.Main(new[] { "cost", "Estimate this" });
                Assert.Equal(0, result);
            });

            Assert.Contains("Tokens:", output);
            Assert.Contains("Estimated cost", output);
        }

        /// <summary>
        /// Captures all console output during the execution of the given action.
        /// </summary>
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
