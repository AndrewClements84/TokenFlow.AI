using TokenFlow.Tools.Commands;
using TokenFlow.Tools.Tests.Helpers;

namespace TokenFlow.Tools.Tests
{
    public class ChunkCommandTests
    {
        [Fact]
        public void Run_ShouldReturnSuccess_AndPrintChunks()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = ChunkCommand.Run("This is a sample text that will be chunked into parts.");
                Assert.Equal(0, result);
            });

            Assert.Contains("Chunk 1:", output);
            Assert.Contains("Total chunks", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForEmptyInput()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = ChunkCommand.Run("");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void Run_ShouldReturnError_ForWhitespace()
        {
            var output = TestConsoleHelper.CaptureOutput(() =>
            {
                int result = ChunkCommand.Run("   ");
                Assert.Equal(1, result);
            });

            Assert.Contains("Please provide text", output);
        }
    }
}


